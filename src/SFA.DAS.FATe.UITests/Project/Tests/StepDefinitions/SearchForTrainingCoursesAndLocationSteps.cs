using SFA.DAS.FATe.UITests.Project.Tests.Pages;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class SearchForTrainingCoursesAndLocationSteps
    {
        private readonly FATeStepsHelper _stepsHelper;
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage;
        private readonly FATeHomePage _fATeHomePage;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;
        private readonly ApprenticeshipTrainingCourseDetailsPage _apprenticeshipTrainingCourseDetailsPage;

        public SearchForTrainingCoursesAndLocationSteps(ScenarioContext context)
        {
            _stepsHelper = new FATeStepsHelper(context);
            _fATeHomePage = new FATeHomePage(context);
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _apprenticeshipTrainingCoursesPage = new ApprenticeshipTrainingCoursesPage(context);
            _apprenticeshipTrainingCourseDetailsPage = new ApprenticeshipTrainingCourseDetailsPage(context);
        }

      [Given("the user navigates to the Search for apprenticeship training courses and training providers page")]
        public async Task GivenTheUserNavigatesToTheSearchForApprenticeshipTrainingCoursesAndTrainingProvidersPage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
        }

        [When("the user navigates to Training providers page")]
        public async Task WhenTheUserNavigatesToTrainingProvidersPage()
        {
            await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Adult care worker (level 2)");
            await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
        }

        [When("the user searches for a course with an apprenticeship location only")]
        public async Task WhenTheUserSearchesForACourseWithAnApprenticeshipLocationOnly()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithApprenticeWorkLocation();
        }

        [Then("the relevant training courses are displayed")]
        public async Task ThenTheRelevantTrainingCoursesAreDisplayed()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("Coventry, West Midlands (Across England)");
        }

        [When("the user searches for a course without location")]
        public async Task WhenTheUserSearchesForACourseWithoutLocation()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithCourseOnly();
        }

        [Then("the relevant training courses are displayed with filters set")]
        public async Task ThenTheRelevantTrainingCoursesAreDisplayedWithFiltersSet()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("worker");
            await _apprenticeshipTrainingCoursesPage.VerifyCourseSearchResults("worker");
        }

        [When(@"the user searches for a course without location and course name")]
        public async Task WhenTheUserSearchesForACourseWithoutLocationAndCourseName()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithoutCourseAndApprenticeWorkLocation();
        }

        [When("the user searches for a course with no results")]
        public async Task WhenTheUserSearchesForACourseWithNoResults()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithCourseNoResults();
        }

        [Then(@"all the courses are displayed without filters set")]
        public async Task ThenAllTheCoursesAreDisplayedWithoutFiltersSet()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
            await _apprenticeshipTrainingCoursesPage.VerifyUrlContainsWordCourses();
        }

        [Then("no courses that match your search is displayed with filters set")]
        public async Task ThenNoCoursesThatMatchYourSearchIsDisplayedWithFiltersSet()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("Selected");
            await _apprenticeshipTrainingCoursesPage.VerifyNoResultsMessage();
            await _apprenticeshipTrainingCoursesPage.VerifyNoresultStartAnewSearchLink();
        }
    }
}
