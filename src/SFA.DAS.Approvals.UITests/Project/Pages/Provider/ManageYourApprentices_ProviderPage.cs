using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ManageYourApprentices_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Manage your apprentices");
        }

        internal async Task<ApprenticeDetails_ProviderPage> SelectViewCurrentApprenticeDetails(string ULN, string name)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search apprentice name or" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search apprentice name or" }).FillAsync(ULN);
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();            
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetails_ProviderPage(context, name));
        }

    }
}
