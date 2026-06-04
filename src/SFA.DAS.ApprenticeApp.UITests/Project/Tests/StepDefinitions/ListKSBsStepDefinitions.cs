using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ListKSBsStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper stepsHelper = new(context);
        private KsbPage ksbPage;

        [When("the apprentice clicks on the KSBs tab")]
        public async Task WhenTheApprenticeClicksOnTheKSBsTab()
        {
            ksbPage = await stepsHelper.NavigateToKsbPageAsync();
        }

        [Then("the KSBs are displayed")]
        public async Task ThenTheKSBsAreDisplayed()
        {
            Assert.AreEqual(
                "Knowledge, skills and behaviours (KSBs)",
                await ksbPage.KsbPageTitleAsync(),
                "The KSB page title did not match the expected header value.");
        }
    }
}