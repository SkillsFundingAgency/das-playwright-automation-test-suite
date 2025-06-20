using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class FundingForNonLevyEmployersPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Funding for non-levy employers");
        }

        internal async Task<ReserveFundingForNonLevyEmployersPage> ClickOnReserveMoreFundingLink()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Reserve more funding" }).ClickAsync();
            return await VerifyPageAsync(() => new ReserveFundingForNonLevyEmployersPage(context));
        }   


    }
}
