using Polly;
using SFA.DAS.FATe.UITests.Helpers;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class TrainingCourseSteps
    {
        private readonly FATeHomePage _fATeHomePage;
        private readonly FATeStepsHelper _stepsHelper;
        private readonly FATeDataHelper _fATeDataHelper;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;
        private readonly SearchForTrainingProviderPage _searchForTrainingProviderPage;
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage;
        private readonly ApprenticeshipTrainingCourseDetailsPage _apprenticeshipTrainingCourseDetailsPage;

        public TrainingCourseSteps(ScenarioContext context)
        {
            _fATeHomePage = new FATeHomePage(context);
            _stepsHelper = new FATeStepsHelper(context);
            _fATeDataHelper = new FATeDataHelper();
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _searchForTrainingProviderPage = new SearchForTrainingProviderPage(context);
            _apprenticeshipTrainingCoursesPage = new ApprenticeshipTrainingCoursesPage(context);
            _apprenticeshipTrainingCourseDetailsPage = new ApprenticeshipTrainingCourseDetailsPage(context);
        }
        [Given("the user navigates to the Apprenticeship training course page")]
        public async Task GivenTheUserNavigatesToTheApprenticeshipTrainingCoursePage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
            await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Accountancy or taxation professional (level 7)");
       }

        [When("the user searches for a provider without entering a location")]
        public async Task WhenTheUserSearchesForAProviderWithoutEnteringALocation()
        {
            await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
            await _fATeHomePage.ReturnToCourseDetail();
        }

        [When("the user searches for a provider with a location")]
        public async Task WhenTheUserSearchesForAProviderWithALocation()
        {
            await _apprenticeshipTrainingCourseDetailsPage.EnterLocationAndViewProviders_PartialPostcode_AcrossEngland();
        }

        [Then("the user verifies that all links on the training course page are working as expected")]
        public async Task ThenTheUserVerifiesThatAllLinksOnTheTrainingCoursePageAreWorkingAsExpected()
        {
            await _fATeHomePage.ReturnToCourseDetail();
            await _apprenticeshipTrainingCourseDetailsPage.ClickRemoveLocation();
            await _apprenticeshipTrainingCourseDetailsPage.VerifyWorkLocationAndTravelDistanceNotPresent();
            await _apprenticeshipTrainingCourseDetailsPage.ClickViewKnowledgeSkillsAndBehaviours();
            await _apprenticeshipTrainingCourseDetailsPage.VerifyIFATELinkOpensInNewTab();
        }
        [When("the user enters and selects a location and verifies the selected location is displayed correctly")]
        public async Task WhenTheUserEntersAndSelectsALocationAndVerifiesTheSelectedLocationIsDisplayedCorrectly()
        {
            await _apprenticeshipTrainingCourseDetailsPage.EnterLocationAndViewProviders_10miles_Coventry();
        }

        [When("verifies the location is stored in the filters on the results page")]
        public async Task WhenVerifiesTheLocationIsStoredInTheFiltersOnTheResultsPage()
        {
            await _fATeHomePage.ReturnToCourseSearchResults();
            await _fATeHomePage.VerifyFilterIsSet("Coventry, West Midlands (within 10 miles)");
        }

        [When("updates the apprentice travel distance")]
        public async Task WhenUpdatesTheApprenticeTravelDistance()
        {
            await _fATeHomePage.SelectApprenticeTravelDistance("50 miles");
            await _fATeHomePage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Accountancy or taxation professional (level 7)");
        }

        [When("verifies the travel distance is updated correctly")]
        public async Task WhenVerifiesTheTravelDistanceIsUpdatedCorrectly()
        {
            await _apprenticeshipTrainingCourseDetailsPage.VerifyWorkLocationAndTravelDistance("Apprentice's work location:", "Coventry, West Midlands");
            await _apprenticeshipTrainingCourseDetailsPage.VerifyWorkLocationAndTravelDistance("Apprentice can travel:", "50 miles");
        }

        [When("removes the location")]
        public async Task WhenRemovesTheLocation()
        {
            await _apprenticeshipTrainingCourseDetailsPage.ClickRemoveLocation();

            await _apprenticeshipTrainingCourseDetailsPage.VerifyWorkLocationAndTravelDistanceNotPresent();
        }

        [When("searches for a new location")]
        public async Task  WhenSearchesForANewLocation()
        {
            await _fATeHomePage.EnterApprenticeWorkLocation("shear", "Shear Brow, Lancashire");
        }

        [Then("verifies the new location and travel distance are updated correctly")]
        public async Task ThenVerifiesTheNewLocationAndTravelDistanceAreUpdatedCorrectly()
        {
            await _apprenticeshipTrainingCourseDetailsPage.VerifyWorkLocationAndTravelDistance("Apprentice's work location:", "Shear Brow, Lancashire");
            await _apprenticeshipTrainingCourseDetailsPage.VerifyWorkLocationAndTravelDistance("Apprentice can travel:", "50 miles");
        }
    }
}