namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class RejectingTheApprenticeshipApplicationsPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Rejecting the apprenticeship application(s)";

    public async Task<TransferPledgePage> BulkReject()
    {
        await page.Locator("#reject-application-reject").CheckAsync();
        await page.Locator("#reject_application").ClickAsync();
        return await VerifyPageAsync(() => new TransferPledgePage(context));
    }

    public async Task<TransferPledgePage> CancelBulkReject()
    {
        await page.Locator("#reject-application-cancel").CheckAsync();
        await page.Locator("#reject_application").ClickAsync();
        return await VerifyPageAsync(() => new TransferPledgePage(context));
    }
}
