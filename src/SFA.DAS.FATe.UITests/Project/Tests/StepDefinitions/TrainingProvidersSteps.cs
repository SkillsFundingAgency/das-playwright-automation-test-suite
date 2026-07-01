using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions;

[Binding, Scope(Tag = "fate")]
public class TrainingProviderSteps(ScenarioContext context)
{
    private readonly FATeHomePage _fATeHomePage = new(context);
    private readonly FATeStepsHelper _stepsHelper = new(context);
    private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage = new(context);
    private readonly TrainingProvidersPage _trainingProvidersPage = new(context);
    private int _expectedProviderCount;

    [When("^the user selects a course and views training providers$")]
    public async Task WhenTheUserSelectsACourseAndViewsTrainingProviders()
    {
        var (count, _trainingProvidersPage) = await _fATeHomePage.ViewTrainingProvidersForCourse("119");

        _expectedProviderCount = count;
    }

    [Then("^the training provider count should be displayed correctly$")]
    public async Task ThenTheTrainingProviderCountShouldBeDisplayedCorrectly()
    {
        await _fATeHomePage.VerifyProvidersCount(_expectedProviderCount); 
    }
    [Given("^the user navigates to the training providers page$")]
    public async Task GivenTheUserNavigatesToTheTrainingProvidersPage()
    {
        await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
        await _fATeHomePage.ClickStartNow();
        await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
        await _fATeHomePage.ViewTrainingProvidersForCourse("274");
    }
    [Given("^verify the filters functionality$")]
    public async Task GivenVerifyTheFiltersFunctionality()
    {
        await _trainingProvidersPage.VerifyAndApplySingleFilters_ProviderPage();
        await _trainingProvidersPage.VerifyAndApplyMultipleFilters_ProviderPage();
    }
    [Then("^verify default sort order results with no location$")]
    public async Task ThenVerifyDefaultSortOrderResultsWithNoLocation()
    {
        await _trainingProvidersPage.VerifyDefaultSortOrder_AchievementRate();
        await _trainingProvidersPage.VerifyAchievementRatesDescendingAsync();
        await _trainingProvidersPage.SelectSortResultsDropdown("EmployerProviderRating");
        await _trainingProvidersPage.VerifyEmployerReviewsSortedAsync("Employer reviews");
        await _trainingProvidersPage.SelectSortResultsDropdown("ApprenticeProviderRating");
        await _trainingProvidersPage.SelectSortResultsDropdown("AchievementRate");
    }
}