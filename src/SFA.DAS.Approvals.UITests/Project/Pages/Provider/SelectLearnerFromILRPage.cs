using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class SelectLearnerFromILRPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator entryMethod => page.GetByRole(AriaRole.Radio, new() { NameRegex = new Regex("Select (apprentices|learners) from ILR") });

        #endregion

        public override async Task VerifyPage()
        {
            if (await entryMethod.IsVisibleAsync())     //this condition to be removed when APPMAN-1741 feature is rolled out
            {
                await entryMethod.CheckAsync();
                await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            }
            
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(new Regex("Select (learner|apprentices) from ILR"));
        }

        internal async Task<AddApprenticeDetailsPage> SelectApprenticeFromILRList(Apprenticeship apprenticeship)
        {
            await SearchULN(apprenticeship.ApprenticeDetails.ULN);

            var tableRow = apprenticeship.ApprenticeDetails.FullName + " " + apprenticeship.ApprenticeDetails.ULN;


            await page.GetByRole(AriaRole.Row, new PageGetByRoleOptions { Name = tableRow })
                      .GetByRole(AriaRole.Link)
                      .ClickAsync();

            return await VerifyPageAsync(() => new AddApprenticeDetailsPage(context));
        }

        internal async Task SearchULN(string uln)
        {
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search apprentice name or unique learner number (ULN)" }).FillAsync(uln);
            await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        }

        internal async Task ClearSearch() => await page.GetByRole(AriaRole.Link, new() { Name = "Clear search" }).ClickAsync();

        internal async Task VerifyNoResultsFound() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("0 apprentice records");


    }

}
