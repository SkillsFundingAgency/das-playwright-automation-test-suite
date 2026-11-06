namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class TransferConnectionRequestDetailsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Connection request details");

    public async Task<ConnectionConfirmedPage> AcceptTransferConnectionRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I want to approve the connection request", Exact = true }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new ConnectionConfirmedPage(context));
    }
}
