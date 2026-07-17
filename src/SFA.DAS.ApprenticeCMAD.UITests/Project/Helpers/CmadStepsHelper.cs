using Microsoft.Playwright;
using Reqnroll;

namespace SFA.DAS.ApprenticeCMAD.UITests.Project.Helpers
{
    public class CmadStepsHelper(ScenarioContext context)
    {
        // Extracts the running Playwright Page driver instance managed by PlaywrightHooks
        public IPage Page => context.Get<IPage>();

        // Targets the standalone CMAD portal route environment entry point
        private readonly string _cmadBaseUrl = "https://pp-cmad.apprenticeships.education.gov.uk";

        public async Task NavigateToCmadStubSignInAsync()
        {
            await Page.GotoAsync($"{_cmadBaseUrl}/Account/StubSignIn");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}