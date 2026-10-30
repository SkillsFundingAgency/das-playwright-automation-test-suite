namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AppliationApprovedPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Application approved";
    protected override ILocator PageHeader => page.Locator(".govuk-panel--confirmation");

    public async Task<TransferPledgePage> ClickBackButton()
    {
        await page.Locator(".govuk-back-link, .back-link").ClickAsync();
        return await VerifyPageAsync(() => new TransferPledgePage(context));
    }
}
