namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmRequestSentPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator cohortReference => page.Locator("dt:has-text('Reference') + dd");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Add apprentice request sent to training provider");
        }

        internal async Task<string> GetCohortId() => await cohortReference.InnerTextAsync();
        

    }
}
