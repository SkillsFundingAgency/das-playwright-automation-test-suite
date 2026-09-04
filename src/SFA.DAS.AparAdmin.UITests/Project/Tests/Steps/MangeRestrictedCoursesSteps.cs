using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ManageRestrictedCourses;
using System.Collections.Generic;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]
public class MangeRestrictedCoursesSteps
{
    private readonly ScenarioContext _context;
    private readonly ViewMangeRestrictedCoursesPage _viewMangeRestrictedCoursesPage;

    public MangeRestrictedCoursesSteps(ScenarioContext context)
    {
        _context = context;
        _viewMangeRestrictedCoursesPage =
            new ViewMangeRestrictedCoursesPage(context);
    }

    [When(@"the user navigates to restricted courses")]
    public async Task WhenTheUserNavigatesToRestrictedCourses()
    {
        var manageTrainingProviderPage = await OpenManageTrainingProviderPage();
        await manageTrainingProviderPage.NavigateToRestrictedCourses();
        await _viewMangeRestrictedCoursesPage.VerifyPage();
    }

    [When(@"the user searches for ""(.*)""")]
    public async Task WhenTheUserSearchesFor(string courseName)
    {
        await _viewMangeRestrictedCoursesPage.SearchCourse(courseName);
    }

    [Then(@"the user is able to verify the restricted course results")]
    public async Task ThenTheUserIsAbleToVerifyTheRestrictedCourseResults()
    {
        await _viewMangeRestrictedCoursesPage.VerifyCourseResults();
    }

    [When(@"the user selects the ""(.*)"" training type filter")]
    public async Task WhenTheUserSelectsTheTrainingTypeFilter(string trainingType)
    {
        await _viewMangeRestrictedCoursesPage.SelectTrainingType(trainingType);
    }

    [When(@"the user selects the following training type filters:")]
    public async Task WhenTheUserSelectsTheFollowingTrainingTypeFilters(Table table)
    {
        foreach (var row in table.Rows)
        {
            await _viewMangeRestrictedCoursesPage.SelectTrainingType(row["Training Type"]);
        }
    }

    [When(@"the user applies the filter")]
    public async Task WhenTheUserAppliesTheFilter()
    {
        await _viewMangeRestrictedCoursesPage.ApplyFilter();
    }

    [Then(@"the user is able to verify the ""(.*)"" filter is selected")]
    public async Task ThenTheUserIsAbleToVerifyTheFilterIsSelected(string trainingType)
    {
        await _viewMangeRestrictedCoursesPage.VerifySelectedFilter(trainingType);
    }

    [Then(@"the user is able to verify the following filters are selected:")]
    public async Task ThenTheUserIsAbleToVerifyTheFollowingFiltersAreSelected(Table table)
    {
        foreach (var row in table.Rows)
        {
            await _viewMangeRestrictedCoursesPage.VerifySelectedFilter(row["Training Type"]);
        }
    }

    [When(@"the user clears the selected filter")]
    public async Task WhenTheUserClearsTheSelectedFilter()
    {
        await _viewMangeRestrictedCoursesPage.ClearAllFilters();
    }

    [When(@"the user clears all selected filters")]
    public async Task WhenTheUserClearsAllSelectedFilters()
    {
        await _viewMangeRestrictedCoursesPage.ClearAllFilters();
    }

    [Then(@"the user is able to verify that no filters are selected")]
    public async Task ThenTheUserIsAbleToVerifyThatNoFiltersAreSelected()
    {
        await _viewMangeRestrictedCoursesPage.VerifyNoFiltersSelected();
    }

    private async Task<ManageTrainingProviderInformationPage>OpenManageTrainingProviderPage()
    {
        var home = new AparAdminHomePage(_context);
        await home.ClickAddOrSearchForProvider();
        return await new ManageTrainingProviderInformationPage(_context)
            .VerifyPageAsync( () => new ManageTrainingProviderInformationPage(_context));
    }

    [Then(@"the user verifies pagination links are working as expected")]
        public async Task ThenTheUserVerifiesPaginationLinksAreWorkingAsExpected()
        {
            await _viewMangeRestrictedCoursesPage.VerifyPaginationLinks(new List<int> { 2 });
        }
}