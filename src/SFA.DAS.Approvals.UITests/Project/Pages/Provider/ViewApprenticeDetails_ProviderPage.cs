using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ViewApprenticeDetails_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("View apprentice details");
        }

        internal async Task<RecognitionOfPriorLearningPage> UpdateEmail(string email)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(email);
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new RecognitionOfPriorLearningPage(context));
        }

    }
}
