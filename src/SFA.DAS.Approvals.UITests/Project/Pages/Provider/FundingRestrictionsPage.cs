using Azure;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class FundingRestrictionsPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private string fundingRestrictionsText = "Funding restrictions ! Warning This employer has reached their limit for active funding reservations and cannot reserve any more funding at this time. Any funding you have previously reserved for this employer is unaffected by this restriction. Return to account";
        
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(fundingRestrictionsText);
        }

        internal async Task<ProviderHomePage> ClickOnReturnToAccountButton()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Return to account" }).ClickAsync();
            return new ProviderHomePage(context);
        }

    }
}
