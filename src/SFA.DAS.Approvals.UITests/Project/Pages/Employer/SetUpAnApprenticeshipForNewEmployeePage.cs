
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    // Dynamic homepage screen
    internal class SetUpAnApprenticeshipForNewEmployeePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Set up an apprenticeship for a new employee");

        internal async Task<ReserveFundingToTrainAndAssessAnApprenticePage> YesContinueToReserveFunding()
        {
            await page.Locator("#reserve-funding").ClickAsync();
            return await VerifyPageAsync(() => new ReserveFundingToTrainAndAssessAnApprenticePage(context));
        }
    }
}