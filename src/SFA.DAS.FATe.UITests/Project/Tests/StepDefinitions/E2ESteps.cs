using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class E2ESteps : FrameworkBaseHooks
    {
        private readonly FATeStepsHelper _stepsHelper;
        private readonly SearchForTrainingProviderPage _searchForTrainingProviderPage;
        private readonly FATeHomePage _fATeHomePage;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage;
        private readonly Specific_TrainingProviderPage _specific_TrainingProviderPage;
        private readonly ShortlistPage _shortlistPage;
        private readonly TrainingProvidersPage _trainingProvidersPage;
        private readonly ApprenticeshipTrainingCourseDetailsPage _apprenticeshipTrainingCourseDetailsPage;
        private readonly string providerName;
        
        public E2ESteps(ScenarioContext context) : base(context) 
        {
            _stepsHelper = new FATeStepsHelper(context);
            _fATeHomePage = new FATeHomePage(context);
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _searchForTrainingProviderPage = new SearchForTrainingProviderPage(context);
            _specific_TrainingProviderPage = new Specific_TrainingProviderPage(context, providerName);
            _shortlistPage = new ShortlistPage(context);
            _trainingProvidersPage = new TrainingProvidersPage(context);
            _apprenticeshipTrainingCoursesPage = new ApprenticeshipTrainingCoursesPage(context);
            _apprenticeshipTrainingCourseDetailsPage = new ApprenticeshipTrainingCourseDetailsPage(context);
        }

        [Given("the user navigates to the training provider details page")]
        public async Task GivenTheUserNavigatesToTheTrainingProviderDetailsPage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.AccessSearchForTrainingProvider();
            await _searchForTrainingProviderPage.SearchWithAUkprn();
        }

        [When("the user accesses the training provider courses page")]
        public async Task WhenTheUserAccessesTheTrainingProviderCoursesPage()
        {
            await _specific_TrainingProviderPage.GoToTrainingProviderCoursePage();
        }

        [When("the user enters a location and performs a search")]
        public async Task WhenTheUserEntersALocationAndPerformsASearch()
        {
            await _specific_TrainingProviderPage.EnterLocationAndSearchForTrainingOptions();
        }

        [When("the user removes or updates the location")]
        public async Task WhenTheUserRemovesOrUpdatesTheLocation()
        {
            await _specific_TrainingProviderPage.RemoveAndUpdateLocation();
        }

        [When("the user accesses the feedback link for the last five years")]
        public async Task WhenTheUserAccessesTheFeedbackLinkForTheLastFiveYears()
        {
            await _specific_TrainingProviderPage.ViewReviewsWithFeedback();
            await _specific_TrainingProviderPage.ClickAllFeedbackTabs();
        }

        [When("the user changes the feedback view to table view")]
        public async Task WhenTheUserChangesTheFeedbackViewToTableView()
        {
            await _specific_TrainingProviderPage.ChangeToTableView();
        }

        [When("the user changes the feedback view to graph view")]
        public async Task WhenTheUserChangesTheFeedbackViewToGraphView()
        {
            await _specific_TrainingProviderPage.ChangeToGraphView();
        }

        [When("the user adds the provider to the shortlist")]
        public async Task WhenTheUserAddsTheProviderToTheShortlist()
        {
            await _specific_TrainingProviderPage.AddToShortList_TrainingProviderPage();
            await _specific_TrainingProviderPage.ClickViewShortlistAsync();
            await _shortlistPage.VerifyCourseNameShortlisted("Software developer (level 4)");
        }

        [When("the user accesses the training provider course page from shortlisted training providers")]
        public async Task WhenTheUserAccessesTheTrainingProviderCoursePageFromShortlistedTrainingProviders()
        {
            await _shortlistPage.ClickOnTrainingProvider("BARKING AND DAGENHAM COLLEGE");
        }

        [When("the user removes the provider from shortlist")]
        public async Task WhenTheUserRemovesTheProviderFromShortlist()
        {
            await _specific_TrainingProviderPage.ClickRemoveFromShortlistAsync();
        }

        [When("the user accesses training providers for this course")]
        public async Task WhenTheUserAccessesTrainingProvidersForThisCourse()
        {
            await _specific_TrainingProviderPage.ViewTrainingProvidersLink();
        }

        [Then("the user navigates back to the training provider details page")]
        public async Task ThenTheUserNavigatesBackToTheTrainingProviderDetailsPage()
        {
            await _trainingProvidersPage.ClickFirstProviderLink();
        }

        [Then("the user accesses training courses page")]
        public async Task ThenTheUserAccessesTrainingCoursesPage()
        {
            await _specific_TrainingProviderPage.ReturnToCourseSearchResults();
        }
        [Then("the user is able to access foundation apprenticeship courses")]
        public async Task ThenTheUserIsAbleToAccessFoundationApprenticeshipCourses()
        {
            await _apprenticeshipTrainingCoursesPage.ApplyFoundationStandardsFilter();
            await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Finishing trades foundation apprenticeship (level 2)");
            await _apprenticeshipTrainingCourseDetailsPage.ClickViewKnowledgeSkillsAndBehaviours();
            await _apprenticeshipTrainingCourseDetailsPage.VerifyIFATELinkOpensInNewTab();
        }


        [When("the provider is listed on the FAT training providers page")]
        public async Task WhenTheProviderIsListedOnTheFATTrainingProvidersPage()
        {
            await Navigate(UrlConfig.FAT_BaseUrl);
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
            await _fATeHomePage.EnterCourseJobOrStandard("Craft plasterer");
            await _fATeHomePage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Craft plasterer (level 3)");
            await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
            await _trainingProvidersPage.VerifyProviderListed("REMIT GROUP LIMITED", true);

            await Navigate(UrlConfig.Provider_BaseUrl);
            await _fATeHomePage.StartNow();
        }

        [When("the provider is not listed on the FAT training providers page")]
        public async Task WhenTheProviderIsNotListedOnTheFATTrainingProvidersPage()
        {
            await Navigate(UrlConfig.FAT_BaseUrl);
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
            await _fATeHomePage.EnterCourseJobOrStandard("Craft plasterer");
            await _fATeHomePage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.SelectCourseByName("Craft plasterer (level 3)");
            await _apprenticeshipTrainingCourseDetailsPage.ViewProvidersForThisCourse();
            await _trainingProvidersPage.VerifyProviderListed("REMIT GROUP LIMITED", false);
        }
    }
}
