using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;
using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class ApprenticeAppLoginSteps(ScenarioContext context)
    {
        private readonly AppStepsHelper _stepsHelper = new(context);
        private ApprenticeAppDashboardPage _dashboardPage;

        [When(@"the user signs in to the Apprentice app")]
        public async Task WhenTheUserSignsInToTheApprenticeApp()
        {
            _dashboardPage = await _stepsHelper.SignInWithValidCredentials();
        }

        [Then(@"the user should be redirected to the dashboard")]
        public async Task ThenTheUserShouldBeRedirectedToTheDashboard()
        {
            await _dashboardPage.VerifyPage();
        }
    }
}
