using System.Text.RegularExpressions;

namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class TransfersPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Transfers", Exact = true, Level = 1 })).ToBeVisibleAsync();

    public async Task<ConnectWithReceivingEmployerPage> ConnectWithReceivingEmployer()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Connect to a receiving employer", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new ConnectWithReceivingEmployerPage(context));
    }

    public async Task<TransferConnectionRequestDetailsPage> ViewTransferConnectionRequestDetails(string sender)
    {
        var row = ConnectionRows("receive", sender).Filter(new() { HasText = "Pending" });
        await Assertions.Expect(row).ToHaveCountAsync(1);
        await row.GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
        return await VerifyPageAsync(() => new TransferConnectionRequestDetailsPage(context));
    }

    public async Task VerifyTransferConnectionStatus(string employer, string role)
    {
        var row = ConnectionRows(role, employer).Filter(new() { HasText = "Approved" });
        await Assertions.Expect(row).ToHaveCountAsync(1);
    }

    public async Task<TransferRequestDetailsPage> OpenPendingCohortRequestAsFundingEmployer(string receivingEmployer, string totalCost)
    {
        // The source selects the latest matching request when earlier requests have the same employer/cost.
        var row = page.Locator("#transfers-send tbody tr")
            .Filter(new() { HasTextRegex = new Regex(Regex.Escape(receivingEmployer), RegexOptions.IgnoreCase) })
            .Filter(new() { HasText = "Pending" })
            .Filter(new() { HasText = totalCost }).Last;
        await Assertions.Expect(row).ToBeVisibleAsync();
        await row.GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
        return await VerifyPageAsync(() => new TransferRequestDetailsPage(context));
    }

    private ILocator ConnectionRows(string role, string employer)
    {
        if (role is not ("send" or "receive"))
            throw new ArgumentOutOfRangeException(nameof(role), role, "Transfer role must be send or receive.");

        return page.Locator($"#connections-{role} tbody tr")
            .Filter(new() { HasTextRegex = new Regex(Regex.Escape(employer), RegexOptions.IgnoreCase) });
    }
}
