using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ConfirmEmployerPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm employer");
        }

        public async Task<SelectApprenticeFromILRPage> ConfirmEmployer()
        {
            await page.Locator("#confirm-true").ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new SelectApprenticeFromILRPage(context));
        }
    }
}
