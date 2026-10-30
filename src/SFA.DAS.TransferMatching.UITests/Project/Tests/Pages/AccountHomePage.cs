using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class AccountHomePage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => objectContext.GetOrganisationName();

    public async Task<MyTransferPledgesPage> ClickTask()
    {
        await page.Locator("#tasks a[href*='pledges']").ClickAsync();
        return await VerifyPageAsync(() => new MyTransferPledgesPage(context));
    }
}
