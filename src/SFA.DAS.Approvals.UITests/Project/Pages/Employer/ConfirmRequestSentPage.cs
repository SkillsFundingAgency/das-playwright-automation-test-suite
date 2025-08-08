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

        public async Task SetCohortReference(List<Apprenticeship> listOfApprenticeship)
        { 
            var cohortRef = await GetCohortId();
            await Task.Delay(100);
            listOfApprenticeship.ForEach(a => a.CohortReference = cohortRef);
            context.Set(listOfApprenticeship);
        }

        internal async Task<string> GetCohortId() => await cohortReference.InnerTextAsync();
        

    }
}
