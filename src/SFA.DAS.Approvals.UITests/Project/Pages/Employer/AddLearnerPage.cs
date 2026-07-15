using SFA.DAS.Approvals.UITests.Project.Pages.Employer;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Employer
{
    internal class AddLearnerPage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator startNowButton => page.GetByRole(AriaRole.Button, new() { Name = "Start now" });

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Add a learner or send a learner request");
        }       


        internal async Task<ChooseYourMainTrainingProviderPage> ClickStartNowButton()
        {
            await startNowButton.ClickAsync();

            return await VerifyPageAsync(() => new ChooseYourMainTrainingProviderPage(context));
        }

        internal async Task<ChooseFundingPage> ClickStartNowButtonNonLevyFlow()
        {
            await startNowButton.ClickAsync();

            return await VerifyPageAsync(() => new ChooseFundingPage(context));
        }
    }
}
