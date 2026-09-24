using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]
public class ApprenticeshipListSteps
{
    private readonly ScenarioContext _context;
    private readonly ProviderRestrictedCoursesPage _providerRestrictedCoursesPage;


public ApprenticeshipListSteps(ScenarioContext context)
{
    _context = context;
    _providerRestrictedCoursesPage = new ProviderRestrictedCoursesPage(context);
    
}

    private async Task<ProviderRestrictedCoursesPage>OpenManageProviderCoursePage()
    {
        var home = new SearchforATrainingProviderPage(_context);
        await home.ClickManageRestrictedCourses();
        return await new ProviderRestrictedCoursesPage(_context)
            .VerifyPageAsync( () => new ProviderRestrictedCoursesPage(_context));
    }

    [When(@"the user clicks on manage restricted courses")]

    public async Task WhenTheUserClicksOnManageRestrictedCourses()
    {
        var manageProviderCoursePage = await OpenManageProviderCoursePage();
    }

    [Then(@"the user uses the search and filter functionality and results are displayed as expected")]

    public async Task ThenTheUserUsesTheSearchAndFilterFunctionalityAndResultsAreDisplayedAsExpected()
    {
        await _providerRestrictedCoursesPage.SearchFunctionality("professional");
        await _providerRestrictedCoursesPage.VerifyResults("professional", "yes");
        await _providerRestrictedCoursesPage.SelectFilter("Last start date added");
        await _providerRestrictedCoursesPage.ApplyFilter();
        await _providerRestrictedCoursesPage.VerifySelectedFilter("Last start date added");
        await _providerRestrictedCoursesPage.ClearAllFilters();
        await _providerRestrictedCoursesPage.VerifyNoFiltersSelected();
    }
}