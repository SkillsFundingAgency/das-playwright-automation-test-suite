using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class AppLoginStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);

        [Given("the apprentice has logged into the app")]
        public async Task GivenTheApprenticeHasLoggedIntoTheApp()
        {
            await _stepsHelper.GoToHomePageAsync();
            await _stepsHelper.GoToStubSignInAsync();
            await _stepsHelper.GoToWelcomePageAsync();
            await _stepsHelper.HandleOnboardingTourIfPresentAsync();
        }

        [Then("the apprentice is taken to the KSBs tab")]
        public async Task ThenTheApprenticeIsTakenToTheKsbsTab()
        {
            await _stepsHelper.VerifyOnKsbsTabAsync();
        }
    }
}