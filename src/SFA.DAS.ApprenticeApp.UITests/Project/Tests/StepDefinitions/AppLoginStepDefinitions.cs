using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class AppLoginStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);

        [Given(@"the apprentice has logged into the app")]
        public async Task GivenTheApprenticeHasLoggedIntoTheApp()
        {
            await _stepsHelper.GoToHomePageAsync();

            await _stepsHelper.GoToStubSignInAsync();

            _ = await _stepsHelper.GoToWelcomePageAsync();

            await _stepsHelper.HandleOnboardingTourIfPresentAsync();

            await _stepsHelper.NavigateToKsbPageAsync();
        }

        [Then(@"the apprentice is taken to the KSBs tab")]
        public async Task ThenTheApprenticeIsTakenToTheKSBsTab()
        {
            // Uses your existing validation helper to verify the URL structure matches the KSB page layout
            await _stepsHelper.VerifyOnKsbsTabAsync();
        }
    }
}