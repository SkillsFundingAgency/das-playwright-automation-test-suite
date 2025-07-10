using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ApprenticesHomePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentices");
        }

        internal async Task GoToAddAnApprentice()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Add an apprentice" }).ClickAsync();
        }

        internal async Task<ApprenticeRequestsPage> GoToApprenticeRequests()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Apprentice requests" }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
        }

        internal async Task GoToManageYourApprentices()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Manage your apprentices" }).ClickAsync();
        }

    }
}
