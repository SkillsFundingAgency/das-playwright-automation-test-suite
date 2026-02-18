using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using System.Linq;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class SearchForTrainingCoursesAndLocationSteps(ScenarioContext context)
    {
        private readonly FATeStepsHelper _stepsHelper = new(context);
        private readonly ApprenticeshipTrainingCoursesPage _apprenticeshipTrainingCoursesPage = new(context);
        private readonly FATeHomePage _fATeHomePage = new(context);
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage = new(context);
        private readonly ApprenticeshipTrainingCourseDetailsPage _apprenticeshipTrainingCourseDetailsPage = new(context);

        [Given("the user navigates to the Search for apprenticeship training courses and training providers page")]
        public async Task GivenTheUserNavigatesToTheSearchForApprenticeshipTrainingCoursesAndTrainingProvidersPage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.BrowseAllCourses();
        }

        [Given("the user navigates to find training page")]
        public async Task GivenTheUserNavigatesToFindTrainingPage()
        {
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
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
            await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet("Worker");
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

        [When("I select the following training types")]
        public async Task WhenISelectTheFollowingTrainingTypes(Table table)
        {

            var trainingTypes = table.Rows
                    .Select(r => r["TrainingType"])
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => Enum.Parse<TrainingType>(x.Replace(" ", ""), ignoreCase: true))
                    .ToArray();

            await _search_TrainingCourses_ApprenticeworkLocationPage.SelectTrainingTypes(trainingTypes);
        }

        [Then(@"the following training type filters are applied")]
        public async Task ThenTheFollowingTrainingTypeFiltersAreApplied(Table table)
        {
            var expectedTypes = table.Rows
                .Select(r => r["TrainingType"])
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => Enum.Parse<TrainingType>(x, ignoreCase: true))
                .ToArray();

            foreach (var type in expectedTypes)
            {
                var filterText = type switch
                {
                    TrainingType.ApprenticeshipUnits => "Apprenticeship units",
                    TrainingType.FoundationApprenticeships => "Foundation apprenticeships",
                    TrainingType.Apprenticeships => "Apprenticeships",
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                await _apprenticeshipTrainingCoursesPage.VerifyFilterIsSet(filterText);
            }
        }
    }
 }
