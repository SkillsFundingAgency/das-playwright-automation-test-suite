using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SettingsStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private SettingsPage _settingsPage;

        [When("the apprentice clicks on settings")]
        public async Task WhenTheApprenticeClicksOnSettings()
        {
            _settingsPage = await _stepsHelper.NavigateToSettingsPageAsync();
        }

        [Then("the settings options are displayed")]
        public async Task ThenTheSettingsOptionsAreDisplayed()
        {
            Assert.AreEqual(
                "Settings",
                await _settingsPage.SettingsPageTitleAsync());
        }
    }
}
