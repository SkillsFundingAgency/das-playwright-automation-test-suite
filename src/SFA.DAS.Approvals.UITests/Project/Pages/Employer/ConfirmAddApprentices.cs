using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmAddApprentices(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Start adding apprentices");
        }

        internal async Task SelectAddApprencticesByProvider()
        {
            await page.Locator("#WhoIsAddingApprentices-Provider").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        }
    
    }
}
