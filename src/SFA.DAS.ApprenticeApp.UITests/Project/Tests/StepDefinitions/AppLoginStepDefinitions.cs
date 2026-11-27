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
            var appOrigin = new Uri(UrlConfig.ApprenticeApp_BaseUrl).GetLeftPart(UriPartial.Authority);
            await context.Get<Driver>().BrowserContext.GrantPermissionsAsync(
                ["notifications"],
                new() { Origin = appOrigin });

            await stepsHelper.SeedPushNotificationCookieAsync();

            await stepsHelper.GoToHomePageAsync();
            await stepsHelper.SignInViaStubAsync();
        }

        [When(@"the apprentice skips the onboarding tour if present")]
        public async Task WhenTheApprenticeSkipsTheOnboardingTourIfPresent()
        {
            await stepsHelper.HandlePushNotificationPromptIfPresentAsync();
            await stepsHelper.HandleOnboardingTourIfPresentAsync();
        }

        [Then(@"the apprentice is taken to the Home tab")]
        public async Task ThenTheApprenticeIsTakenToTheHomeTab()
        {
            await stepsHelper.VerifyOnHomeTabAsync();
        }
    }
}