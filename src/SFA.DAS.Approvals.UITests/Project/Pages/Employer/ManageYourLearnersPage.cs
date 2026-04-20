using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ManageYourLearnersPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator statusLocator => page.Locator("tbody.govuk-table__body tr.govuk-table__row:first-of-type td[data-label='Status'] strong");
        private ILocator searchBox => page.Locator("input#searchTerm[name='searchTerm']");
        private ILocator searchButton => page.Locator("button.govuk-button.das-search-form__button", new() { HasTextString = "Search" });
        private ILocator apprenticeLink(string apprenticeName) => page.GetByRole(AriaRole.Link, new () { Name = apprenticeName });
        #endregion

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1").First).ToContainTextAsync("Manage your learners");
        }

        internal async Task<ApprenticeDetails_ProviderPage> SelectViewCurrentApprenticeDetails(string ULN, string name)
        {
            await SearchApprentice(ULN);
            await page.GetByRole(AriaRole.Link, new() { Name = name }).ClickAsync();
            return await VerifyPageAsync(() => new ApprenticeDetails_ProviderPage(context, name));
        }

        internal async Task SearchApprentice(string ULN, string name)
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

        private async Task SearchApprentice(string ULN)
        {
            await searchBox.FillAsync("");
            await searchBox.FillAsync(ULN);
            await searchButton.ClickAsync();
        }


    }
}
