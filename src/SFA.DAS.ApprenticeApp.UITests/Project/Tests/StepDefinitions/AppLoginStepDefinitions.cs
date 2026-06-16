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

            await stepsHelper.SignInViaStubAsync();
        }

        [When(@"the apprentice skips the onboarding tour if present")]
        public async Task WhenTheApprenticeSkipsTheOnboardingTourIfPresent()
        {
            await stepsHelper.HandleOnboardingTourIfPresentAsync();
        }

        [Then(@"the apprentice is taken to the KSBs tab")]
        public async Task ThenTheApprenticeIsTakenToTheKSBsTab()
        {
            await stepsHelper.VerifyOnKsbsTabAsync();
        }
    }
}