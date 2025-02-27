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

        [When("the user searches for a course with an apprenticeship location")]
        public async Task WhenTheUserSearchesForACourseWithAnApprenticeshipLocation()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithApprenticeWorkLocation();
        }

        [Then("the relevant training courses are displayed")]
        public async Task ThenTheRelevantTrainingCoursesAreDisplayed()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyPage(); // The reuslt page is yet to be developed
        }

        [When("the user searches for a course without location")]
        public async Task WhenTheUserSearchesForACourseWithoutLocation()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithCourseOnly();
        }

        [Then("the relevant training courses are displayed with filters set")]
        public async Task ThenTheRelevantTrainingCoursesAreDisplayedWithFiltersSet()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyWorkerFilterIsSet();
            await _apprenticeshipTrainingCoursesPage.VerifyResultsContainWordWorker();
        }

        [When(@"the user searches for a course without location and course name")]
        public async Task WhenTheUserSearchesForACourseWithoutLocationAndCourseName()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.SearchWithoutCourseAndApprenticeWorkLocation();
        }

        [Then(@"all the courses are displayed without filters set")]
        public async Task ThenAllTheCoursesAreDisplayedWithoutFiltersSet()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
            await _apprenticeshipTrainingCoursesPage.VerifyUrlContainsWordCourses();
        }
    }
}
