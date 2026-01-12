
namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    // Dynamic homepage screen
    internal class ContinueSettingupAnApprenticeship(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#call-to-action-continue-setting-up-an-apprenticeship")).ToContainTextAsync("Continue setting up an apprenticeship");

        internal async Task<DoYouNeedToCreateAdvertForThisApprenticeship> Continue()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
            return await VerifyPageAsync(() => new DoYouNeedToCreateAdvertForThisApprenticeship(context));
        }

    }
}