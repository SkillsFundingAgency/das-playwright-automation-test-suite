using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ApprenticeDetails_ProviderPage : ApprovalsBasePage
    {
        private readonly ScenarioContext context;
        private readonly string pageTitle; 

        public ApprenticeDetails_ProviderPage(ScenarioContext context, string pageTitle) : base(context)
        {
            this.context = context;
            this.pageTitle = pageTitle; 
        }

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync(pageTitle);
        }

        internal async Task<ManageYourApprentices_ProviderPage> ReturnBackToManageYourApprenticesPage()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Manage your apprentices" }).ClickAsync();
            return await VerifyPageAsync(() => new ManageYourApprentices_ProviderPage(context));
        }


    }
}
