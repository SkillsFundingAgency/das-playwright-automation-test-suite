﻿using System;
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

        internal async Task<CheckApprenticeDetailsPage> SelectApprenticeFromILRList(Apprenticeship apprenticeship)
        {
            await SearchULN(apprenticeship.ApprenticeDetails.ULN);

            var tableRow = apprenticeship.ApprenticeDetails.FullName + " " + apprenticeship.ApprenticeDetails.ULN;


            await page.GetByRole(AriaRole.Row, new PageGetByRoleOptions { Name = tableRow })
                      .GetByRole(AriaRole.Link)
                      .ClickAsync();

            return await VerifyPageAsync(() => new CheckApprenticeDetailsPage(context));
        }

        internal async Task SearchULN(string uln)
        {
            
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Search apprentice name or unique learner number (ULN)" })
                      .FillAsync(uln);

            // Apply filters with default year (2025)
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" })
                      .ClickAsync();

            // Wait for potential results or "no records" message
            var resultText = await page.Locator("text=apprentice records found").TextContentAsync();

            // Check if zero records found
            if (resultText != null && resultText.Contains("0 apprentice records found"))
            {
                // Select 2024 from "Start year" dropdown
                await page.Locator("#FilterModel_StartYear")
                  .SelectOptionAsync("2024");

                // Re-apply the filter
                await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" })
                          .ClickAsync();
            }
        }


        internal async Task ClearSearch() => await page.GetByRole(AriaRole.Link, new() { Name = "Clear search and filters" }).ClickAsync();

        internal async Task VerifyNoResultsFound() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("0 apprentice records");


    }

}
