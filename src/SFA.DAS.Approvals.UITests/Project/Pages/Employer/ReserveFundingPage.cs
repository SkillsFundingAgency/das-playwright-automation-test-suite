
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ReserveFundingPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl").First).ToContainTextAsync("Reserve funding");


        internal async Task<DoYouKnowWhichTrainingCourseYourLearnerWillTakePage> YesContinueToReserveFunding()
        {
            await page.Locator("//*[.='Reserve funding']").ClickAsync();
            return await VerifyPageAsync(() => new DoYouKnowWhichTrainingCourseYourLearnerWillTakePage(context));
        }
    }
}