using Azure;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class AddApprenticeDetails_EntryMothodPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add apprentice details");
        }

        public async Task<AddApprenticeDetails_SelectJourneyPage> SelectOptionToApprenticesFromILR()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Select apprentices from ILR" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new AddApprenticeDetails_SelectJourneyPage(context));
        }

        

    }

}
