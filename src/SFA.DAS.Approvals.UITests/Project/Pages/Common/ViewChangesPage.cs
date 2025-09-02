using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Common
{
    internal class ViewChangesPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("View changes");
        }

        internal async Task<ViewChangesPage> SelectViewChangesOptions()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await this.VerifyPageAsync(() => new ViewChangesPage(context));

        }
        internal async Task ClickOnCancelAndReturn() => await page.GetByRole(AriaRole.Link, new() { Name = "Cancel and return" }).ClickAsync();
    }
}
