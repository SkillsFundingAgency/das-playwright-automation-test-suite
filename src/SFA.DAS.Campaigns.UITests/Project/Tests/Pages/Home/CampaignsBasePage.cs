using Microsoft.Playwright;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Home;

public abstract class CampaignsBasePage(ScenarioContext context) : BasePage(context)
{
    protected readonly CampaignsDataHelper campaignsDataHelper = context.Get<CampaignsDataHelper>();
    protected IPage page => context.Get<Driver>().Page;

    protected async Task<T> VerifyPageAsync<T>(Func<T> createPage) where T : BasePage
    {
        var pageObj = createPage();
        await pageObj.VerifyPage();
        return pageObj;
    }
}