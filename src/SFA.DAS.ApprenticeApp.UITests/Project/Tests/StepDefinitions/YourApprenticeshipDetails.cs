using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class YourApprenticeshipDetailsStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private YourApprenticeshipDetailsPage _yourApprenticeshipDetailsPage;

        [When("the apprentice clicks on the account tab")]
        public async Task WhenTheApprenticeClicksOnTheAccountTab()
        {
            await _stepsHelper.NavigateToAccountPageAsync();
        }

        [When("the apprentice clicks on your apprenticeship details")]
        public async Task WhenTheApprenticeClicksOnYourApprenticeshipDetails()
        {
            _yourApprenticeshipDetailsPage = await _stepsHelper.NavigateToYourApprenticeshipDetailsPageAsync();
        }

        [Then("the apprenticeship details page is displayed")]
        public async Task ThenTheApprenticeshipDetailsPageIsDisplayed()
        {
            Assert.AreEqual(
                "Your apprenticeship details",
                await _yourApprenticeshipDetailsPage.YourApprenticeshipDetailsPageTitleAsync());
        }
    }
}