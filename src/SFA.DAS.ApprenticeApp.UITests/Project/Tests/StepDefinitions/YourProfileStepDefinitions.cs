using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class YourProfileStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private YourProfilePage _yourProfilePage;

        [When("the apprentice clicks on the account tab")]
        public async Task WhenTheApprenticeClicksOnTheAccountTab()
        {
            await _stepsHelper.NavigateToAccountPageAsync();
        }

        [When("the apprentice clicks on your profile")]
        public async Task WhenTheApprenticeClicksOnYourProfile()
        {
            _yourProfilePage = await _stepsHelper.NavigateToYourProfilePageAsync();
        }

        [Then("the profile page is displayed")]
        public async Task ThenTheProfilePageIsDisplayed()
        {
            Assert.AreEqual(
                "Your apprenticeship details",
                await _yourProfilePage.YourProfilePageTitleAsync());
        }
    }
}
