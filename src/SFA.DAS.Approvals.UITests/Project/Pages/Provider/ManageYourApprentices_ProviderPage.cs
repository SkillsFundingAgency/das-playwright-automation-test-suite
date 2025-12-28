using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class ManageYourApprentices_ProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Manage your apprentices");
        }

        internal async Task<ApprenticeDetails_ProviderPage> SelectViewCurrentApprenticeDetails(string name, long? ULN=null)
        {
            _ = ULN == null ? SearchApprentice(name) : SearchApprentice(ULN.ToString());
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetails_ProviderPage(context, name));
        }

        internal async Task VerifyApprenticeFound(long ULN, string name)
        {
            var apprenticeLink = page.GetByRole(AriaRole.Link, new() { Name = name });

            await SearchApprentice(ULN.ToString());

            if (await apprenticeLink.CountAsync() > 0)
            {
                var statusLocator = page.Locator("tbody.govuk-table__body tr.govuk-table__row:first-of-type td[data-label='Status'] strong");
                string status = await statusLocator.InnerTextAsync();

                context.Get<ObjectContext>().SetDebugInformation($"A '{status.ToUpper()}' Apprenticeship record found for ULN: {ULN} and Name: {name} in Provider Portal");
            }
            else
            {
                throw new Exception($"Apprentice with ULN {ULN} and name {name} not found.");
            }

        }

        internal async Task SearchApprentice(string ULN)
        {
            var searchBox = page.GetByRole(AriaRole.Textbox, new() { Name = "Search apprentice name or" });
            var searchButton = page.GetByRole(AriaRole.Button, new() { Name = "Search" });

            await searchBox.FillAsync("");
            await searchBox.FillAsync(ULN.ToString());
            await searchButton.ClickAsync();
        }

        internal async Task<ApprenticeDetails_ProviderPage> OpenFirstItemFromTheList(string name)
        {
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetails_ProviderPage(context, name));
        }

        internal async Task<ManageYourApprentices_ProviderPage> DownloadCsv()
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Download all data" }).ClickAsync();
            return await VerifyPageAsync(() => new ManageYourApprentices_ProviderPage(context));
        }


    }
}
