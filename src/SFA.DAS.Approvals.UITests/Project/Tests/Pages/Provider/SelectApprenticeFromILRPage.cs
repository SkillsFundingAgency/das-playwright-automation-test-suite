using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Tests.Pages.Provider
{
    internal class SelectApprenticeFromILRPage(ScenarioContext context) : ApprovalsProviderBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select apprentice from ILR for ");
        }

        public async Task<AddApprenticeDetailsPage> SelectApprenticeFromILRList()
        {
            await page.GetByRole(AriaRole.Row, new PageGetByRoleOptions { Name = "Chloe Murphy 7947822688" })
                      .GetByRole(AriaRole.Link)
                      .ClickAsync();

            return await VerifyPageAsync(() => new AddApprenticeDetailsPage(context));
        }
    }

}
