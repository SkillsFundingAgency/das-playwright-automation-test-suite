using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class SelectFundingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override  async Task VerifyPage()    
        {
            await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Select funding");
        }

        internal async Task ClickContinueButton()
        {
            await continueButton.ClickAsync();
        }

        internal async Task SelectFundingtype()
        {
            await page.GetByLabel("Select funding").SelectOptionAsync("Current levy funds");

            await ClickContinueButton();
        }

    }
}
