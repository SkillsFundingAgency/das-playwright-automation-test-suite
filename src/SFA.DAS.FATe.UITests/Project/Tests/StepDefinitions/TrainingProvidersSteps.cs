using Azure;
using Polly;
using SFA.DAS.FATe.UITests.Helpers;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using TechTalk.SpecFlow.CommonModels;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class TrainingProviderSteps
    {
        private readonly FATeHomePage _fATeHomePage;
        private readonly FATeStepsHelper _stepsHelper;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;
        private readonly TrainingProvidersPage _trainingProvidersPage;
        private int _expectedProviderCount;

        public TrainingProviderSteps(ScenarioContext context)
        {
            _fATeHomePage = new FATeHomePage(context);
            _stepsHelper = new FATeStepsHelper(context);
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _trainingProvidersPage = new TrainingProvidersPage(context);    
        }

        [When("the user selects a course and views training providers")]
        public async Task WhenTheUserSelectsACourseAndViewsTrainingProviders()
        {
            var (count, _trainingProvidersPage) = await _fATeHomePage.ViewTrainingProvidersForCourse("119");

            _expectedProviderCount = count;
        }

        [Then("the training provider count should be displayed correctly")]
        public async Task ThenTheTrainingProviderCountShouldBeDisplayedCorrectly()
        {
            await _fATeHomePage.VerifyProvidersCount(_expectedProviderCount); 
        }
        [Given("the user navigates to the training providers page")]
        public async Task GivenTheUserNavigatesToTheTrainingProvidersPage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
            await _fATeHomePage.ViewTrainingProvidersForCourse("204");
        }
        [Given("verify the filters functionality")]
        public async Task GivenVerifyTheFiltersFunctionality()
        {
            await _trainingProvidersPage.VerifyAndApplySingleFilters_ProviderPage();
            await _trainingProvidersPage.VerifyAndApplyMultipleFilters_ProviderPage();
        }
        [Then("verify default sort order results with no location")]
        public async Task ThenVerifyDefaultSortOrderResultsWithNoLocation()
        {
            await _trainingProvidersPage.VerifyDefaultSortOrder_AchievementRate();
            await _trainingProvidersPage.VerifyAchievementRatesDescendingAsync();
            await _trainingProvidersPage.SelectEmployerProviderRating("EmployerProviderRating");
            await _trainingProvidersPage.VerifyEmployerReviewsSortedAsync("employer reviews");
            await _trainingProvidersPage.SelectEmployerProviderRating("ApprenticeProviderRating");
            await _trainingProvidersPage.SelectEmployerProviderRating("Distance");
        }
    }
}