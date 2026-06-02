using Reqnroll;
using SFA.DAS.ApprenticeApp.UITests.Project.Helpers;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class StubSignInPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string StubIdInput = "input#Id";
        private const string EmailInput = "input#Email";
        private const string SignInButton = "button.app-button[type='submit']";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(StubIdInput)).ToBeVisibleAsync();
        }

        public async Task<WelcomePage> SignInAsync(string userId, string email)
        {
            await page.Locator(StubIdInput).ClearAsync();
            await page.Locator(StubIdInput).FillAsync(userId);

            await page.Locator(EmailInput).ClearAsync();
            await page.Locator(EmailInput).FillAsync(email);

            await page.Locator(SignInButton).ClickAsync();
            return await VerifyPageAsync(() => new WelcomePage(context));
        }
    }
}