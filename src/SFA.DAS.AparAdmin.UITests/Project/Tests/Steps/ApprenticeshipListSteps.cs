using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.ApprenticeshipList;
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.SearchAndUpdate;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Steps;

[Binding, Scope(Tag = "apar")]
public class ApprenticeshipListSteps
{
    private readonly ScenarioContext _context;
    private readonly ProviderRestrictedCoursesPage _providerRestrictedCoursesPage;
    private readonly RestrictACoursesPage _restrictACoursePage;


public ApprenticeshipListSteps(ScenarioContext context)
{
    _context = context;
    _providerRestrictedCoursesPage = new ProviderRestrictedCoursesPage(context);
    _restrictACoursePage = new RestrictACoursesPage(context);
    
}

    private async Task<ProviderRestrictedCoursesPage>OpenManageProviderCoursePage()
    {
        var home = new SearchforATrainingProviderPage(_context);
        await home.ClickManageRestrictedCourses();
        return await new ProviderRestrictedCoursesPage(_context)
            .VerifyPageAsync( () => new ProviderRestrictedCoursesPage(_context));
    }

    private async Task<RestrictACoursesPage>ClickRestrictACourse()
    {
        var home = new ProviderRestrictedCoursesPage(_context);
        await home.ClickRestrictACourse();
        return await new RestrictACoursesPage(_context)
            .VerifyPageAsync( () => new RestrictACoursesPage(_context));
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

    [Then(@"the user restricts a course that is not available in the drop down")]

    public async Task ThenTheUserRestrictsACourseThatIsNotAvailableInTheDropDown()
    {
        await _providerRestrictedCoursesPage.ClickRestrictACourse();
        await _restrictACoursePage.SelectCourse("Singing");
        await _restrictACoursePage.ErrorMessage();
    }

    [Then(@"^the user restricts for the provider course ""(.*)"" which they currently do not provide$")]

    public async Task ThenTheUserRestrictsForTheProviderCourseWhichTheyCurrentlyDoNotProvide(string courseName)
    {
        await _restrictACoursePage.SelectCourse(courseName);
        await _restrictACoursePage.ConfirmSelection();
        await _providerRestrictedCoursesPage.SearchFunctionality(courseName);
        await _providerRestrictedCoursesPage.VerifyResults(courseName, "yes");
    }

    [Then(@"the user restricts for the provider course ""(.*)"" which they currently provide")]

    public async Task ThenTheUserRestrictsForTheProviderCourseWhichTheyCurrentlyProvide(string courseName)
    {
        await _providerRestrictedCoursesPage.ClickRestrictACourse();
        await _restrictACoursePage.SelectCourse(courseName);
        await _restrictACoursePage.EnterDate("20-08-2027");
        await _restrictACoursePage.ConfirmSelection();
        await _restrictACoursePage.ErrorMessage();
        await _restrictACoursePage.EnterDate("20-08-2020");
        await _restrictACoursePage.ConfirmSelection();
        await _providerRestrictedCoursesPage.SearchFunctionality(courseName);
        await _providerRestrictedCoursesPage.VerifyResults(courseName, "yes");
    }
}