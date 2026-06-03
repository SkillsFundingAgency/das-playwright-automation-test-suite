using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class RequestSentToTrainingProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator cohortReference => page.Locator("dt:has-text('Reference') + dd");

        public override async Task VerifyPage()
        {
            var heading = page.Locator("h1").First;

            await Assertions.Expect(heading).ToContainTextAsync("Request sent to training provider");
        }

        internal async Task<string> GetCohortId() => await cohortReference.InnerTextAsync();
        

    }
}
