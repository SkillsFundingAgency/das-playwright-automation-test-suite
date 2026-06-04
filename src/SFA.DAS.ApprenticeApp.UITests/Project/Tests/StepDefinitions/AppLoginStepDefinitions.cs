using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class AppLoginStepDefinitions(ScenarioContext context)
    {
        private readonly AppStepsHelper stepsHelper = new(context);

        [Given(@"the apprentice has logged into the app")]
        public async Task GivenTheApprenticeHasLoggedIntoTheApp()
        {
            await stepsHelper.GoToHomePageAsync();

            await stepsHelper.GoToStubSignInAsync();

            _ = await stepsHelper.GoToWelcomePageAsync();

            await stepsHelper.HandleOnboardingTourIfPresentAsync();

            await stepsHelper.NavigateToKsbPageAsync();
        }

        [Then(@"the apprentice is taken to the KSBs tab")]
        public async Task ThenTheApprenticeIsTakenToTheKSBsTab()
        {
            await stepsHelper.VerifyOnKsbsTabAsync();
        }
    }
}