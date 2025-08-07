using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmRequestSentPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator cohortReference => page.Locator("dt:has-text('Reference') + dd");

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Add apprentice request sent to training provider");
        }

        public async Task SetCohortReference(Apprenticeship apprenticeship)
        { 
            await GetCohortId(apprenticeship);

            await Task.Delay(100);
            context.Set(apprenticeship, "Apprenticeship");
        }

        internal async Task GetCohortId(Apprenticeship apprenticeship)
        {
            var cohortRef = await cohortReference.InnerTextAsync();
            apprenticeship.CohortReference = cohortRef;
        }

    }
}
