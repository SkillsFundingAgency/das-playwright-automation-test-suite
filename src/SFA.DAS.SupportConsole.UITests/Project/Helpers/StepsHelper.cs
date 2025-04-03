using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;

namespace SFA.DAS.SupportConsole.UITests.Project.Helpers;

public class StepsHelper(ScenarioContext context)
{
    public async Task<SearchHomePage> Tier1LoginToSupportConsole() => await LoginToSupportTool(context.GetUser<SupportConsoleTier1User>());

    public async Task<SearchHomePage> Tier2LoginToSupportConsole() => await LoginToSupportTool(context.GetUser<SupportConsoleTier2User>());

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

    private async Task<SearchHomePage> LoginToSupportTool(DfeAdminUser loginUser)
    {
        await new DfeAdminLoginStepsHelper(context).LoginToSupportTool(loginUser);

        return await VerifyPageHelper.VerifyPageAsync(() => new SearchHomePage(context));
    }
}