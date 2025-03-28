﻿using SFA.DAS.FATe.UITests.Project.Tests.Pages;


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
        [Then("the user is able to select and clear single filters")]
        public async Task ThenTheUserIsAbleToSelectAndClearSingleFilters()
        {
            await _apprenticeshipTrainingCoursesPage.VerifyAndApplySingleFilters();
        }


        [Then("the user is able to add multiple filters and clear all")]
        public async Task ThenTheUserIsAbleToAddMultipleFiltersAndClearAll()
        {
            await _apprenticeshipTrainingCoursesPage.ApplyMultipleFilters_ClearAtOnce();
        }

        [Then("the user is able to verify results for the filters set")]
        public async Task ThenTheUserIsAbleToVerifyResultsForTheFiltersSet()
        {
            await _apprenticeshipTrainingCoursesPage.ApplyCourseFilterAndVerifyResultsForProfessional();
            await _apprenticeshipTrainingCoursesPage.ApplyLocationFilterAndVerifyResultsForTW14_50miles();
            await _apprenticeshipTrainingCoursesPage.ApplyJobcategoriesFilterAndVerifyResults_ProtectiveServices();
        }

    }
}
