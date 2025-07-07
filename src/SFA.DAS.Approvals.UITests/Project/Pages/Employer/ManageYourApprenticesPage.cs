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
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Manage your apprentices");
        }

        internal async Task<ApprenticeDetails_ProviderPage> SelectViewCurrentApprenticeDetails(string ULN, string name)
        {
            await SearchApprentice(ULN);
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetails_ProviderPage(context, name));
        }

        internal async Task VerifyApprenticeFound(string ULN, string name)
        {
            await SearchApprentice(ULN);
            var apprenticeLink = page.GetByRole(AriaRole.Link, new() { Name = name });

            if (await apprenticeLink.CountAsync() > 0)
            {
                var statusLocator = page.Locator("tbody.govuk-table__body tr.govuk-table__row:first-of-type td[data-label='Status'] strong");
                string status = await statusLocator.InnerTextAsync();

                context.Get<ObjectContext>().SetDebugInformation($"A '{status.ToUpper()}' Apprenticeship record found for ULN: {ULN} and Name: {name} in Employer Portal");
            }
            else
            {
                throw new Exception($"Apprentice with ULN {ULN} and name {name} not found.");
            }

        }

        private async Task SearchApprentice(string ULN)
        {
            var searchBox = page.GetByRole(AriaRole.Textbox, new() { Name = "Search by apprentice name" });
            var searchButton = page.GetByRole(AriaRole.Button, new() { Name = "Search" });

            await searchBox.FillAsync("");
            await searchBox.FillAsync(ULN);
            await searchButton.ClickAsync();
        }


    }
}
