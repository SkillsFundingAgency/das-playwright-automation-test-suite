namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class TransferRequestDetailsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Transfer request details");

    public async Task<TransferRequestApprovedPage> ApproveTransferRequest()
    {
        await page.Locator("#ApprovalConfirmed").CheckAsync();
        await page.Locator("#submit-approve-transfer").ClickAsync();
        return await VerifyPageAsync(() => new TransferRequestApprovedPage(context));
    }

    public async Task<TransferRequestRejectedPage> RejectTransferRequest()
    {
        await page.Locator("#ApprovalConfirmed-no").CheckAsync();
        await page.Locator("#submit-approve-transfer").ClickAsync();
        return await VerifyPageAsync(() => new TransferRequestRejectedPage(context));
    }
}
