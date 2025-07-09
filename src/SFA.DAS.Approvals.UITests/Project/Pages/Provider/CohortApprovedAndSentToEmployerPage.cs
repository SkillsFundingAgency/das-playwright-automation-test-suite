using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class CohortApprovedAndSentToEmployerPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator cohortReference => page.Locator("dt:has-text('Cohort reference') + dd");
        private ILocator sentTo => page.Locator("dt:has-text('Sent to') + dd");
        private ILocator messageForEmployer => page.Locator("dt:has-text('Message for employer') + dd");

        private ILocator goToApprenticeRequestsLink => page.Locator("a:has-text('Go to apprentice requests')");
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Cohort approved and sent to employer");
        }

        public async Task VerifyCohortApprovedAndSentToEmployer(Apprenticeship apprenticeship)
        {
            await Assertions.Expect(cohortReference).ToHaveTextAsync(apprenticeship.CohortReference);
            await Assertions.Expect(sentTo).ToHaveTextAsync(apprenticeship.EmployerDetails.EmployerName.ToString());
            await Assertions.Expect(messageForEmployer).ToHaveTextAsync("Please review the details and approve the request.");
        }
        public async Task<ApprenticeRequests_ProviderPage> GoToApprenticeRequests()
        {
            await goToApprenticeRequestsLink.ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequests_ProviderPage(context));
        }


    }
}
