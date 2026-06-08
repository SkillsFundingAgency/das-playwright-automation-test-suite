using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using System.Text.RegularExpressions;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class SelectLearnerFromILRPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator clearFilterLink => page.GetByRole(AriaRole.Link, new() { Name = "Clear search and filters" });
        #endregion

        public override async Task VerifyPage()
        {
            var employerName = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship)
                .FirstOrDefault()?
                .EmployerDetails?
                .EmployerName;

            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync($"Select learners from ILR for {employerName}");
        }

        internal async Task<CheckApprenticeDetailsPage> SelectApprenticeFromILRList(Apprenticeship apprenticeship)
        {
            await ClearSearch();

            await SearchULN(apprenticeship.ApprenticeDetails.ULN, apprenticeship.TrainingDetails.StartDate.Year);

            var tableRow = apprenticeship.ApprenticeDetails.FullName + " " + apprenticeship.ApprenticeDetails.ULN;

            await page.GetByRole(AriaRole.Row, new PageGetByRoleOptions { Name = tableRow })
                      .GetByRole(AriaRole.Link)
                      .First
                      .ClickAsync();

            return await VerifyPageAsync(() => new CheckApprenticeDetailsPage(context));
        }

        internal async Task SearchULN(string uln, int startYear)
        {
            
            await page.Locator("#searchTerm").FillAsync(uln);
            await page.Locator("#FilterModel_StartYear").SelectOptionAsync(startYear.ToString());
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).ClickAsync();
        }


        internal async Task ClearSearch()
        {
            if (await clearFilterLink.IsVisibleAsync())
            {
                await clearFilterLink.ClickAsync();
            }
        }

        internal async Task VerifyNoResultsFound() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("There are no matching results.");


    }

}
