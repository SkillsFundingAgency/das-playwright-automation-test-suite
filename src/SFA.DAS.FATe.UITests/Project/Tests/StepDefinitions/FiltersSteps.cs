using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using TechTalk.SpecFlow;
using System;
using SpecFlow;
using Azure;
using Polly;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class FiltersSteps
    {
        private readonly FATeHomePage _fATeHomePage;
        private readonly FATeDataHelper _fATeDataHelper;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;
        private readonly SearchForTrainingProviderPage _searchForTrainingProviderPage;
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage;

        public FiltersSteps(ScenarioContext context)
        {
            _fATeHomePage = new FATeHomePage(context);
            _fATeDataHelper = new FATeDataHelper();
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _searchForTrainingProviderPage = new SearchForTrainingProviderPage(context);
            _apprenticeshipTrainingCoursesPage = new ApprenticeshipTrainingCoursesPage(context);
        }

        [Then("the user verifies filters functionality")]
        public async Task ThenTheUserVerifiesFiltersFunctionality()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
            await _apprenticeshipTrainingCoursesPage.VerifyDistanceFilterSelection("Across England");
            await _apprenticeshipTrainingCoursesPage.EnterCourseJobOrStandard("Professional");
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("Professional");
            await _apprenticeshipTrainingCoursesPage.ClearSpecificFilter("Professional");
            await _apprenticeshipTrainingCoursesPage.EnterApprenticeWorkLocation(_fATeDataHelper.PartialPostCode, _fATeDataHelper.PostCodeDetails);
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("TW14 Hounslow (Across England)");
            await _apprenticeshipTrainingCoursesPage.ClearSpecificFilter("TW14 Hounslow (Across England)");
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
            await _apprenticeshipTrainingCoursesPage.SelectApprenticeTravelDistance("10 miles");
            await _apprenticeshipTrainingCoursesPage.EnterApprenticeWorkLocation(_fATeDataHelper.PartialPostCode, _fATeDataHelper.PostCodeDetails);
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("TW14 Hounslow (within 10 miles)");
            await _apprenticeshipTrainingCoursesPage.ClearSpecificFilter("TW14 Hounslow  (within 10 miles)");
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
            await _apprenticeshipTrainingCoursesPage.EnterApprenticeWorkLocation(_fATeDataHelper.PartialPostCode, _fATeDataHelper.PostCodeDetails);
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("TW14 Hounslow (Across England)");
            await _apprenticeshipTrainingCoursesPage.SelectApprenticeTravelDistance("10 miles");
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("TW14 Hounslow (within 10 miles)");
            await _apprenticeshipTrainingCoursesPage.SelectApprenticeTravelDistance("100 miles");
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("TW14 Hounslow (within 100 miles)");
            await _apprenticeshipTrainingCoursesPage.ClearSpecificFilter("TW14 Hounslow  (within 100 miles)");
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
            await _apprenticeshipTrainingCoursesPage.SelectJobCategory("Agriculture, environmental and animal care");
            await _apprenticeshipTrainingCoursesPage.ApplyFilters();
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("Agriculture, environmental and animal care");
            await _apprenticeshipTrainingCoursesPage.ClearSpecificFilter("Agriculture, environmental and animal care");
            await _apprenticeshipTrainingCoursesPage.VerifyNoFiltersAreApplied();
        }

        [When("verifies that all expected links are present and functional")]
        public async Task GivenVerifiesThatAllExpectedLinksArePresentAndFunctional()
        {
            await _search_TrainingCourses_ApprenticeworkLocationPage.AccessSearchForTrainingProvider();
            await _fATeHomePage.ViewShortlist();
            await _fATeHomePage.ReturnToSearch_TrainingCourses_ApprenticeworkLocationPage();
            await _fATeHomePage.ReturnToStartPage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.ViewShortlist();
            await _fATeHomePage.ReturnToSearch_TrainingCourses_ApprenticeworkLocationPage();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
            await _fATeHomePage.ReturnToSearch_TrainingCourses_ApprenticeworkLocationPage();

        }
    }
}
