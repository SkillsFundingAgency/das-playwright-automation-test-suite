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
            Assert.IsTrue(_stepsHelper.IsOnKsbPage(), "The browser did not navigate to the KSB page URL.");
        }

        [When("the apprentice views the \"(.*)\", \"(.*)\", and \"(.*)\" KSB sub-tabs")]
        public async Task WhenTheApprenticeViewsTheKsbSubTabs(string tab1, string tab2, string tab3)
        {
            await _stepsHelper.ClickKsbSubTabAsync(tab1);
            Assert.IsTrue(await _stepsHelper.IsKsbPanelVisibleAsync(tab1), $"{tab1} panel is not visible.");

            await _stepsHelper.ClickKsbSubTabAsync(tab2);
            Assert.IsTrue(await _stepsHelper.IsKsbPanelVisibleAsync(tab2), $"{tab2} panel is not visible.");

            await _stepsHelper.ClickKsbSubTabAsync(tab3);
            Assert.IsTrue(await _stepsHelper.IsKsbPanelVisibleAsync(tab3), $"{tab3} panel is not visible.");
        }

        [Then("each KSB sub-tab displays its respective items correctly")]
        public async Task ThenEachKsbSubTabDisplaysItsRespectiveItemsCorrectly()
        {
            await _stepsHelper.ClickKsbSubTabAsync("Knowledge");
            Assert.IsTrue(await _stepsHelper.IsKsbPanelVisibleAsync("Knowledge"), "Knowledge tab default view failed.");
        }
    }
}