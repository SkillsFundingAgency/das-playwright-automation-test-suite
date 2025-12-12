using SFA.DAS.EmployerPortal.UITests.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class EmployerHomePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(objectContext.GetOrganisationName());
        }

        internal async Task<YourFundingReservationsPage> ClickOnYourFundingReservationsLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Your funding reservations" }).ClickAsync();
            return await VerifyPageAsync(() => new YourFundingReservationsPage(context));
        }
    }
}
