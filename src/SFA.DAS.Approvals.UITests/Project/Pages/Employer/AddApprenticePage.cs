using SFA.DAS.Approvals.UITests.Project.Pages.Employer;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AddApprenticePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator startNowButton => page.GetByRole(AriaRole.Button, new() { Name = "Start now" });

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add an apprentice");
        }       


        internal async Task<AddTrainingProviderPage> ClickStartNowButton()
        {
            await startNowButton.ClickAsync();

            return await VerifyPageAsync(() => new AddTrainingProviderPage(context));
        }

        internal async Task<SelectFundingPage> ClickStartNowButtonNonLevyFlow()
        {
            await startNowButton.ClickAsync();

            return await VerifyPageAsync(() => new SelectFundingPage(context));
        }
    }
}
