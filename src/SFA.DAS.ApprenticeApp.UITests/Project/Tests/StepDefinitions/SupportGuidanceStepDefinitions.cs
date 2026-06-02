using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class SupportGuidanceStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private SupportGuidancePage _supportGuidancePage;

        [When("the apprentice clicks on the support and guidance tab")]
        public async Task WhenTheApprenticeClicksOnTheSupportAndGuidanceTab()
        {
            _supportGuidancePage = await _stepsHelper.NavigateToSupportGuidancePageAsync();
        }

        [Then("the support and guidance articles are displayed")]
        public async Task ThenTheSupportAndGuidanceArticlesAreDisplayed()
        {
            Assert.AreEqual(
                "Support and guidance",
                await _supportGuidancePage.SupportGuidancePageTitleAsync());
        }
    }
}
