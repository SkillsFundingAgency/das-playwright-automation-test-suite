using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using TechTalk.SpecFlow;
using System;
using SpecFlow;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class SearchForTrainingProviderSteps
    {
        private readonly FATeStepsHelper _stepsHelper;
        private readonly SearchForTrainingProviderPage _searchForTrainingProviderPage;
        private readonly FATeHomePage _fATeHomePage;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;

        public SearchForTrainingProviderSteps(ScenarioContext context)
        {
            _stepsHelper = new FATeStepsHelper(context);
            _fATeHomePage = new FATeHomePage(context);
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _searchForTrainingProviderPage = new SearchForTrainingProviderPage(context);
        }

        [Given("the user navigates to the Search for a training provider page")]
        public async Task GivenTheUserNavigatesToTheSearchForATrainingProviderPage()
        {   
            await _stepsHelper.AcceptCookiesAndGoToFATeHomePage();
            await _fATeHomePage.ClickStartNow();
            await _search_TrainingCourses_ApprenticeworkLocationPage.AccessSearchForTrainingProvider();
        }

        [When("the user searches with a valid ukprn")]
        public async Task WhenTheUserSearchesWithAValidUkprn()
        {
            await _searchForTrainingProviderPage.SearchWithAUkprn();
        }

        [Then("the results page is displayed with training providers")]
        public async Task ThenTheResultsPageIsDisplayedWithTrainingProviders()
        {
            await _searchForTrainingProviderPage.VerifyPage(); // The reuslt page is yet to be developed
        }

        [When("the user should not be able to search without a UKPRN")]
        public async Task WhenTheUserShouldNotBeAbleToSearchWithoutAUKPRN()
        {
            await _searchForTrainingProviderPage.SearchWithoutAUKPRN();
        }

        [When("the user should not be able to search with an invalid UKPRN")]
        public async Task WhenTheUserShouldNotBeAbleToSearchWithAnInvalidUKPRN()
        {
            await _searchForTrainingProviderPage.SearchWithAnInvalidUkprn();
        }
    }
}
