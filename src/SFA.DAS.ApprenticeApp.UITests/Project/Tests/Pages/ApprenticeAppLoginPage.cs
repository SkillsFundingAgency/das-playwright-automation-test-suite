
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class ApprenticeAppLoginPage(ScenarioContext context) : BasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("button#js-sign-in")).ToBeVisibleAsync();
        }

        public async Task AcceptCookies()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Accept additional cookies" }).ClickAsync();

        }

        public async Task ClickSignInButton()
        {
            await page.Locator("button#js-sign-in").ClickAsync();
        }

        public async Task EnterUserId(string userId)
        {
            await page.Locator("input#Id").FillAsync(userId);
        }

        public async Task EnterEmail(string email)
        {
            await page.Locator("input#Email").FillAsync(email);
        }

        public async Task ClickSubmitSignInButton()
        {
            await page.Locator("button[type='submit']").ClickAsync();
        }

        public async Task<ApprenticeAppDashboardPage> SignInWithValidCredentials(string userId, string email)
        {
            await ClickSignInButton();
            await EnterUserId(userId);
            await EnterEmail(email);
            await ClickSubmitSignInButton();

            return await VerifyPageAsync(() => new ApprenticeAppDashboardPage(context));
        }
    }
}
