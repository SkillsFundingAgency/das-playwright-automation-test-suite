using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AccountHomePage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => objectContext.GetOrganisationName();
    // The first h1 can be a notification banner heading, so target the organisation name heading as HomePage does.
    protected override ILocator PageHeader => page.Locator("h1.govuk-heading-xl");

    public async Task<MyTransferPledgesPage> ClickTask()
    {
        await page.Locator("#tasks a[href*='pledges']").ClickAsync();
        return await VerifyPageAsync(() => new MyTransferPledgesPage(context));
    }
}
