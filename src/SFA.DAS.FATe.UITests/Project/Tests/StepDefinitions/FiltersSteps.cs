using SFA.DAS.FATe.UITests.Project.Tests.Pages;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class FiltersSteps
    {
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage;
        public FiltersSteps(ScenarioContext context)
        {
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
            await _apprenticeshipTrainingCoursesPage.ApplyFoundationStandardsFilterAndVerifyResultsForFoundationStandards();
        }
    }
}
