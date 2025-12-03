using Azure;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ManageYourApprenticesPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator statusLocator => page.Locator("tbody.govuk-table__body tr.govuk-table__row:first-of-type td[data-label='Status'] strong");
        private ILocator searchBox => page.GetByRole(AriaRole.Textbox, new() { Name = "Search by apprentice name" });
        private ILocator searchButton => page.GetByRole(AriaRole.Button, new() { Name = "Search" });
        private ILocator apprenticeLink(string apprenticeName) => page.GetByRole(AriaRole.Link, new () { Name = apprenticeName });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Manage your apprentices");
        }

        internal async Task<ApprenticeDetails_ProviderPage> SelectViewCurrentApprenticeDetails(long ULN, string name)
        {
            await SearchApprentice(ULN);
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetails_ProviderPage(context, name));
        }

        internal async Task SearchApprentice(long ULN, string name)
        {
            var apprenticeLink = page.GetByRole(AriaRole.Link, new() { Name = name });
            
            if (await searchBox.IsVisibleAsync())       //search box and other filters only become available when ther eare more than 20 apprentices
            {
                await SearchApprentice(ULN);
            }
                      

            if (await apprenticeLink.CountAsync() > 0)
            {
                string status = await statusLocator.InnerTextAsync();
                context.Get<ObjectContext>().SetDebugInformation($"A '{status.ToUpper()}' Apprenticeship record found for ULN: {ULN} and Name: {name} in Employer Portal");
            }
            else
            {
                throw new Exception($"Apprentice with ULN {ULN} and name {name} not found.");
            }

        }

        internal async Task<ApprenticeDetailsPage> OpenFirstItemFromTheList(string apprenticeName)
        {
            await apprenticeLink(apprenticeName).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetailsPage(context, apprenticeName));
        }

        private async Task SearchApprentice(long ULN)
        {
            await searchBox.FillAsync("");
            await searchBox.FillAsync(ULN.ToString());
            await searchButton.ClickAsync();
        }


    }
}
