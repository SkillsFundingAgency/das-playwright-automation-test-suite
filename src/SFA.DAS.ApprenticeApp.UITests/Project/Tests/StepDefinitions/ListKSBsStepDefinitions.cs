using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using Reqnroll;
using NUnit.Framework; // Ensured assertion framework is present

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ListKSBsStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private KsbPage _ksbPage;

        [When("the apprentice clicks on the KSBs tab")]
        public async Task WhenTheApprenticeClicksOnTheKSBsTab()
        {
            _ksbPage = await _stepsHelper.NavigateToKsbPageAsync();
        }

        [Then("the KSBs are displayed")]
        public async Task ThenTheKSBsAreDisplayed()
        {
            Assert.AreEqual(
                "Knowledge, skills and behaviours (KSBs)",
                await _ksbPage.KsbPageTitleAsync(),
                "The KSB page title did not match the expected header value.");
        }
    }
}