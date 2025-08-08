using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmTrainingProvider(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override  async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator(".govuk-heading-l").First).ToContainTextAsync("Confirm training provider");
        }

        internal async Task<ConfirmAddApprentices> ConfirmTrainingProviderDetails()
        {
            await page.Locator("#UseThisProvider").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new ConfirmAddApprentices(context));
        }
    }
}
