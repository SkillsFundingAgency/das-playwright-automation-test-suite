
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ReserveFundingToTrainAndAssessAnApprenticePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Reserve funding to train and assess an apprentice");


        internal async Task<DoYouKnowWhichApprenticeshipTrainingYourApprenticeWillTakePage> YesContinueToReserveFunding()
        {
            await page.Locator("//*[.='Reserve funding']").ClickAsync();
            return await VerifyPageAsync(() => new DoYouKnowWhichApprenticeshipTrainingYourApprenticeWillTakePage(context));
        }
    }
}