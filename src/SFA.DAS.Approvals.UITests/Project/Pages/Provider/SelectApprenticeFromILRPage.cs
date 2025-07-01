using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class SelectApprenticeFromILRPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Select apprentices from ILR for ");
        }

        public async Task<AddApprenticeDetailsPage> SelectApprenticeFromILRList(Apprenticeship apprenticeship)
        {
            await SearchULN(apprenticeship.ApprenticeDetails.ULN);

            var tableRow = apprenticeship.ApprenticeDetails.FirstName + " " + apprenticeship.ApprenticeDetails.LastName + " " + apprenticeship.ApprenticeDetails.ULN;


            await page.GetByRole(AriaRole.Row, new PageGetByRoleOptions { Name = tableRow })
                      .GetByRole(AriaRole.Link)
                      .ClickAsync();

            return await VerifyPageAsync(() => new AddApprenticeDetailsPage(context));
        }

        private async Task SearchULN(string uln)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search apprentice name or" }).FillAsync(uln);
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        }


    }

}
