using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using TechTalk.SpecFlow;
using System;
using SpecFlow;
using Azure;

namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "fate")]
    public class VerifyLinksSteps
    {
        private readonly FATeHomePage _fATeHomePage;
        private readonly Search_TrainingCourses_ApprenticeworkLocationPage _search_TrainingCourses_ApprenticeworkLocationPage;
        private readonly SearchForTrainingProviderPage _searchForTrainingProviderPage;

        public VerifyLinksSteps(ScenarioContext context)
        {
            _fATeHomePage = new FATeHomePage(context);
            _search_TrainingCourses_ApprenticeworkLocationPage = new Search_TrainingCourses_ApprenticeworkLocationPage(context);
            _searchForTrainingProviderPage = new SearchForTrainingProviderPage(context);
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
