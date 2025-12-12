using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class YouCannotCreateAnotherFundingReservationPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("You cannot create another funding reservation");
        }

        public async Task<ApprenticeRequestsPage> ClickOnApprenticeRequestsLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Apprentice requests" }).First.ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
        }

    }
}
