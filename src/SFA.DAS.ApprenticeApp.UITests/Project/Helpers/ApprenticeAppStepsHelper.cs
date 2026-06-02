using SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Helpers
{
    public class ApprenticeAppUserConfig
    {
        public string Username { get; set; }
    }

    public class ApprenticeAppStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
    {
        public async Task<ApprenticeAppLoginPage> NavigateToApprenticeAppLoginPage()
        {
            await Navigate(UrlConfig.ApprenticeApp_BaseUrl);

            var loginPage = new ApprenticeAppLoginPage(context);

            await loginPage.AcceptCookies();
            await loginPage.VerifyPage();
            return loginPage;
        }

        public async Task<ApprenticeAppDashboardPage> SignInWithValidCredentials()
        {
            var loginPage = await NavigateToApprenticeAppLoginPage();

            var appUser = context.Get<ConfigSection>().GetConfigSection<ApprenticeAppUserConfig>();
            var email = appUser.Username;

            return await loginPage.SignInWithValidCredentials(email, email);
        }
    }
}