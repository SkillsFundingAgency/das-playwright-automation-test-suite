
using TechTalk.SpecFlow;


namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class ConfirmTrainingProviderPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm training provider");
        }

        internal async Task<StartAddingApprenticesPage> ConfirmProviderDetailsAreCorrece()
        {
            await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

            return await VerifyPageAsync(() => new StartAddingApprenticesPage(context));

        }
    }
}