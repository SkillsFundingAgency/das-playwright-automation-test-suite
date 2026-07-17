using SFA.DAS.Framework; // Targets your actual core BasePage layer
using Microsoft.Playwright;
using Reqnroll;

namespace SFA.DAS.ApprenticeCMAD.UITests.Project.Pages
{
    public class CmadStubSignInPage(ScenarioContext context) : BasePage(context)
    {
        // Adjust these to match the exact HTML input field IDs used on the CMAD sign-in screen
        private const string CmadIdInput = "input#Id";
        private const string CmadEmailInput = "input#Email";
        private const string CmadSignInButton = "button[type='submit']";

        public override async Task VerifyPage()
        {
            // Verifies the driver successfully reached the standalone CMAD bypass layout
            await Assertions.Expect(page.Locator(CmadIdInput)).ToBeVisibleAsync();
        }

        public async Task SignInAsync(string userId, string email)
        {
            await page.Locator(CmadIdInput).FillAsync(userId);
            await page.Locator(CmadEmailInput).FillAsync(email);
            await page.Locator(CmadSignInButton).ClickAsync();
        }
    }
}