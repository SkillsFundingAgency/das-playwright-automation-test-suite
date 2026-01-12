
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class SetupAnApprenticeshipPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#set-up-an-apprenticeship")).ToContainTextAsync("Set up an apprenticeship");

        internal async Task<DoYouKnowWhichCourseYourApprenticeWillTakePage> StartNow()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouKnowWhichCourseYourApprenticeWillTakePage(context));
        }

    }
}