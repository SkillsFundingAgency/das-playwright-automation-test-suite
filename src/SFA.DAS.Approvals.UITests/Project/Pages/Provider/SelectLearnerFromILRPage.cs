using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using System.Text.RegularExpressions;
using SFA.DAS.Approvals.UITests.Project.Helpers;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class SelectLearnerFromILRPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        #region locators
        private ILocator entryMethod => page.Locator("text=Choose details from ILR (individual learner record)");
        private ILocator clearFilterLink => page.GetByRole(AriaRole.Link, new() { Name = "Clear search and filters" });
        #endregion

        public override async Task VerifyPage()
        {

            if (await entryMethod.IsVisibleAsync())     //this condition to be removed when APPMAN-1741 feature is rolled out
            {
                await entryMethod.CheckAsync();
                await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            }

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
