using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ViewApprenticesDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context), IPageWithABackLink
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToHaveTextAsync(new Regex(@"^View(?:\s\d+)?\s(?:apprentice|apprentices') details$"));
        }

        public async Task ClickOnBackLinkAsync() => await page.Locator("a.govuk-back-link").ClickAsync();




    }
}
