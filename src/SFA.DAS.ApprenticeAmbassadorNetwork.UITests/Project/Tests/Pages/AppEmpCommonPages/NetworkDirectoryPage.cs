namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class NetworkDirectoryPage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Network directory");
    }

    private static string SearchResultLink => (".das-search-results__link");

    public async Task<ApprenticeMessagePage> GoToApprenticeMessagePage(bool isRegionalChair)
    {
        if (isRegionalChair) await FilterByRole_Regionalchair();

        else await FilterByRole_Apprentice();

        await page.Locator(SearchResultLink).First.ClickAsync();

        return await VerifyPageAsync(() => new ApprenticeMessagePage(context, isRegionalChair));
    }

    public new async Task FilterEventByEventRegion_London()
    {
        await base.FilterEventByEventRegion_London();
    }
    public new async Task FilterByRole_Apprentice()
    {
        await base.FilterByRole_Apprentice();
    }
    public new async Task FilterByRole_Employer()
    {
        await base.FilterByRole_Employer();
    }
    public new async Task FilterByRole_Regionalchair()
    {
        await base.FilterByRole_Regionalchair();
    }

    public new async Task ClearAllFilters()
    {
        await base.ClearAllFilters();
    }

    public new async Task VerifyEventRegion_London_Filter()
    {
        await base.VerifyEventRegion_London_Filter();
    }

    public new async Task VerifyRole_Apprentice_Filter()
    {
        await base.VerifyRole_Apprentice_Filter();
    }

    public new async Task VerifyRole_Employer_Filter()
    {
        await base.VerifyRole_Employer_Filter();
    }

    public new async Task VerifyRole_Regionalchair_Filter()
    {
        await base.VerifyRole_Regionalchair_Filter();
    }
}