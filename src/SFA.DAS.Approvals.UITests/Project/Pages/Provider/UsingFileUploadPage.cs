using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class UsingFileUploadPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Using file upload");
        }

        internal async Task<UploadCsvFilePage> ClickContinueButton()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Continue to upload" }).ClickAsync();
            return await VerifyPageAsync(() => new UploadCsvFilePage(context));
        }
    }
}
