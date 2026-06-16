using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class NotificationsStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private NotificationsPage _notificationsPage;

        [When("the apprentice clicks on the notifications tab")]
        public async Task WhenTheApprenticeClicksOnTheNotificationsTab()
        {
            _notificationsPage = await _stepsHelper.NavigateToNotificationsPageAsync();
        }

        [Then("the notifications are displayed")]
        public async Task ThenTheNotificationsAreDisplayed()
        {
            Assert.AreEqual(
                "Notifications",
                await _notificationsPage.NotificationsPageTitleAsync());
        }
    }
}
