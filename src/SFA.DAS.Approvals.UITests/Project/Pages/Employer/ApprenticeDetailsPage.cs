using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ApprenticeDetailsPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private readonly string pageTitle;

        internal ApprenticeDetailsPage(ScenarioContext context, string pageTitle) : base(context)
        {
            this.context = context;
            this.pageTitle = pageTitle;
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(pageTitle);
        }

        internal async Task<ManageYourApprenticesPage> ReturnBackToManageYourApprenticesPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to manage your apprentices" }).ClickAsync();
            return await VerifyPageAsync(() => new ManageYourApprenticesPage(context));
        }

        internal async Task<EditApprenticeDetailsPage> ClickOnEditApprenticeDetailsLink()
        { 
            await page.Locator("#edit-apprentice-link").ClickAsync();
            return await VerifyPageAsync(() => new EditApprenticeDetailsPage(context));
        }

    }
}
