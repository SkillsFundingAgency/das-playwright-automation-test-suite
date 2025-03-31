using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Helpers;

public class StepsHelper(ScenarioContext context)
{
    public async Task NavigateToSupportTools()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.SupportTools_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    public async Task<ToolSupportHomePage> ReLoginToSupportSCPTools()
    {
        await NavigateToSupportTools();

        if (!await new ToolSupportHomePage(context).IsPageDisplayed())
        {
            await new DfeAdminLoginStepsHelper(context).CheckAndLoginToSupportTool(context.GetUser<SupportToolScpUser>());
        }

        return await VerifyPageHelper.VerifyPageAsync(() => new ToolSupportHomePage(context));
    }

    public async Task<ToolSupportHomePage> ValidUserLogsinToSupportSCPTools() => await LoginToSupportTools(context.GetUser<SupportToolScpUser>());

    public async Task<ToolSupportHomePage> ValidUserLogsinToSupportSCSTools() => await LoginToSupportTools(context.GetUser<SupportToolScsUser>());

    private async Task<ToolSupportHomePage> LoginToSupportTools(DfeAdminUser loginUser)
    {
        await new DfeAdminLoginStepsHelper(context).LoginToSupportTool(loginUser);

        return await VerifyPageHelper.VerifyPageAsync(() => new ToolSupportHomePage(context));
    }
    
    public async Task<SearchHomePage> Tier1LoginToSupportConsole() => await LoginToSupportConsole(context.GetUser<SupportConsoleTier1User>());

    public async Task<SearchHomePage> Tier2LoginToSupportConsole() => await LoginToSupportConsole(context.GetUser<SupportConsoleTier2User>());

    public async Task<AccountOverviewPage> SearchAndViewAccount() => await new SearchHomePage(context).SearchByPublicAccountIdAndViewAccount();

    public async Task<UlnSearchResultsPage> SearchForUln(string uln)
    {
        var page = await new AccountOverviewPage(context).ClickCommitmentsMenuLink();

        return await page.SearchForULN(uln);
    }

    public async Task SearchWithInvalidUln(bool WithSpecialChars)
    {
        var page = await new AccountOverviewPage(context).ClickCommitmentsMenuLink();

        await page.SelectUlnSearchTypeRadioButton();

        if (WithSpecialChars)
            await page.SearchWithInvalidULNWithSpecialChars();
        else
            await page.SearchWithInvalidULN();
    }

    public async Task SearchWithInvalidCohort(bool WithSpecialChars)
    {
        var page = await new AccountOverviewPage(context).ClickCommitmentsMenuLink();

        await page.SelectCohortRefSearchTypeRadioButton();

        if (WithSpecialChars)
            await page.SearchWithInvalidCohortWithSpecialChars();
        else
            await page.SearchWithInvalidCohort();
    }

    public async Task SearchWithUnauthorisedCohortAccess()
    {
        var page = await new AccountOverviewPage(context).ClickCommitmentsMenuLink();

        await page.SelectCohortRefSearchTypeRadioButton();

        await page.SearchWithUnauthorisedCohortAccess();
    }

    public async Task<CohortSummaryPage> SearchForCohort(string cohortRef)
    {
        var page = await new AccountOverviewPage(context).ClickCommitmentsMenuLink();

        return await page.SearchCohort(cohortRef);
    }

    private async Task<SearchHomePage> LoginToSupportConsole(DfeAdminUser loginUser)
    {
        await new DfeAdminLoginStepsHelper(context).LoginToSupportConsole(loginUser);

        return await VerifyPageHelper.VerifyPageAsync(() => new SearchHomePage(context));
    }
}
