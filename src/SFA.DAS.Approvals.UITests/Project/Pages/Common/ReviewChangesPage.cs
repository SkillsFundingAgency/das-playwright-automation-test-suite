using Azure;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Common
{
    internal class ReviewChangesPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Review changes");
        }

        internal async Task<ReviewChangesPage> SelectReviewChangesOptions()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await this.VerifyPageAsync(() => new ReviewChangesPage(context));

        }
        internal async Task ClickOnCancelAndReturn() => await page.GetByRole(AriaRole.Link, new() { Name = "Cancel and return" }).ClickAsync();





    }
}
