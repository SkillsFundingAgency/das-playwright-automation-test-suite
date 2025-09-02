using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class DetailsOfIlrDataMismatchPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Details of ILR data mismatch");
        }

        internal async Task<DetailsOfIlrDataMismatchPage> SelectILRDataMismatchOptions()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await this.VerifyPageAsync(() => new DetailsOfIlrDataMismatchPage(context));
        }
        internal async Task ClickOnCancel() => await page.GetByRole(AriaRole.Link, new() { Name = "Cancel" }).ClickAsync();

    }
}
