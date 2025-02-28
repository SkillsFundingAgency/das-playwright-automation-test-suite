using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using TechTalk.SpecFlow;
using System;
using SpecFlow;
using Azure;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class SearchForTrainingCoursesAndLocationSteps
    {
        private readonly FATeStepsHelper _stepsHelper;
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage;
        private readonly FATeHomePage _fATeHomePage;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;

        public SearchForTrainingCoursesAndLocationSteps(ScenarioContext context)
        {
            _stepsHelper = new FATeStepsHelper(context);
            _fATeHomePage = new FATeHomePage(context);
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _apprenticeshipTrainingCoursesPage = new ApprenticeshipTrainingCoursesPage(context);
        }

      [Given("the user navigates to the Search for apprenticeship training courses and training providers page")]
        public async Task GivenTheUserNavigatesToTheSearchForApprenticeshipTrainingCoursesAndTrainingProvidersPage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
        }

        [When("the user searches for a course with an apprenticeship location only")]
        public async Task WhenTheUserSearchesForACourseWithAnApprenticeshipLocationOnly()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithApprenticeWorkLocation();
        }

        [Then("the relevant training courses are displayed")]
        public async Task ThenTheRelevantTrainingCoursesAreDisplayed()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("Coventry, West Midlands (within 10 miles)");
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
            await _apprenticeshipTrainingCoursesPage.VerifyResultsContainWordWorker("worker");
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
        }
    
    }
}
