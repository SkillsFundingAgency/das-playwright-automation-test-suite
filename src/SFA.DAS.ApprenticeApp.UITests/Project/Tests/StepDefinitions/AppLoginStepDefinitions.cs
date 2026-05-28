using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class AppLoginStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);

        [Given(@"the apprentice has accepted the cookies")]
        public async Task GivenTheApprenticeHasAcceptedCookies()
        {
            await _stepsHelper.GoToHomePageAsync();
        }

        [When("the apprentice logs into the app")]
        public async Task WhenTheApprenticeLogsIntoTheApp()
        {
            await _stepsHelper.GoToStubSignInAsync();
        }

        [When(@"the apprentice is taken to the welcome page")]
        public async Task ThenTheApprenticeIsTakenToTheHomeScreen()
        {
            await _stepsHelper.GoToWelcomePageAsync();
        }

        [Then("the apprentice is taken to the tasks page")]
        public async Task ThenTheApprenticeIsTakenToTheTasksPage()
        {
            await _stepsHelper.GoToTasksPageAsync();
        }

        [Given("the apprentice has logged into the app")]
        public async Task GivenTheApprenticeHasLoggedIntoTheApp()
        {
            await _stepsHelper.GoToHomePageAsync();
            await _stepsHelper.GoToStubSignInAsync();
            await _stepsHelper.GoToWelcomePageAsync();
            await _stepsHelper.GoToTasksPageAsync();
        }
    }
}
