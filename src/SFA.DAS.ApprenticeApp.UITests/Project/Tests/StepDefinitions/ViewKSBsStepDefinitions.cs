using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ViewKSBsStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private KsbPage _ksbPage;

        [When("the apprentice clicks on the KSBs tab")]
        public async Task WhenTheApprenticeClicksOnTheKSBsTab()
        {
            _ksbPage = await _stepsHelper.NavigateToKsbPageAsync();
        }

        [Then("the apprentice is taken to the KSBs tab")]
        public async Task ThenTheApprenticeIsTakenToTheKSBsTab()
        {
            await _ksbPage.VerifyPage();
        }

        [When("the apprentice views the \"(.*)\", \"(.*)\", and \"(.*)\" KSB sub-tabs")]
        public async Task WhenTheApprenticeViewsTheKsbSubTabs(string tab1, string tab2, string tab3)
        {
            await _ksbPage.SelectTabAsync(tab1);
            await _ksbPage.SelectTabAsync(tab2);
            await _ksbPage.SelectTabAsync(tab3);
        }

        [Then("each KSB sub-tab displays its respective items correctly")]
        public async Task ThenEachKsbSubTabDisplaysItsRespectiveItemsCorrectly()
        {
            await _ksbPage.VerifyTabHasItemsAsync("Knowledge");
            await _ksbPage.VerifyTabHasItemsAsync("Skills");
            await _ksbPage.VerifyTabHasItemsAsync("Behaviours");
        }
    }
}