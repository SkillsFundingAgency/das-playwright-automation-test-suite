
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class SetUpAnApprenticeshipForExistingEmployeePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Are you setting up an apprenticeship for an existing employee?");


   
        internal async Task<ReserveFundingToTrainAndAssessAnApprenticePage> YesContinueToReserveFunding()
        {
            await page.Locator("#reserve-funding").ClickAsync();
            return await VerifyPageAsync(() => new ReserveFundingToTrainAndAssessAnApprenticePage(context));
        }
    }
}