using Reqnroll;

namespace SFA.DAS.ApprenticeApp.UITests.Project.Tests.Pages
{
    public class AccountPage(ScenarioContext context) : AppBasePage(context)
    {
        private const string AccountHeader = "h1.govuk-heading-xl";

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(AccountHeader)).ToBeVisibleAsync();
        }

        public async Task<string> AccountPageTitleAsync()
            => await page.Locator(AccountHeader).InnerTextAsync();
    }
}
