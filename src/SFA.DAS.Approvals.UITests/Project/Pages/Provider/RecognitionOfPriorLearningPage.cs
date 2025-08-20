using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class RecognitionOfPriorLearningPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Recognition of prior learning (RPL)");
        }

        internal async Task<ApproveApprenticeDetailsPage> SelectNoForRPL()
        {
            await page.GetByText("No", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            return await VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<AddRecognitionOfPriorLearningDetailsPage> SelectYesForRPL()
        {
            await page.GetByText("Yes", new() { Exact = true }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Save and continue" }).ClickAsync();

            return await VerifyPageAsync(() => new AddRecognitionOfPriorLearningDetailsPage(context));
        }
    }
}
