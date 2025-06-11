using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ApprenticeRequestsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentice requests");
        }

        public async Task<EmployerApproveApprenticeDetailsPage> OpenApprenticeRequestReadyForReview(string cohortRef)
        {
            await page.Locator($"tr[data-cohort='{cohortRef}'] a.govuk-link").ClickAsync();

            return new EmployerApproveApprenticeDetailsPage(context);
        }




    }
}
