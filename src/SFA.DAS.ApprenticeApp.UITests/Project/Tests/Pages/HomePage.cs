
namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class HomePage(ScenarioContext context) : AppBasePage(context)
    {
        private const string SignInButton = "button.app-button";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your Apprenticeship");
        }

        public async Task<StubSignInPage> AppSignInAsync()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Sign in" }).ClickAsync();
            return await VerifyPageAsync(() => new StubSignInPage(context));
        }
    }
}
