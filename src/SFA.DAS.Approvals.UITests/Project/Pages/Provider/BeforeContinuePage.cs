using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Pages.Provider
{
    internal class BeforeContinuePage(ScenarioContext context) : ApprovalsBasePage(context)
    {
        private ILocator continueButton => page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

        public override async Task VerifyPage()
        {
            await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Before you continue");
        }

        internal async Task ContinueToChooseACohort()
        {
            await continueButton.ClickAsync();
        }
    }
}