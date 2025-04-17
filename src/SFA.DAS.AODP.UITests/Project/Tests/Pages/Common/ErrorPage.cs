

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.Common
{
    public class ErrorPage(ScenarioContext context) : AodpLandingPage(context)
    {
        public ILocator ErrorSummary => page.Locator(".govuk-error-summary .govuk-error-summary__body");
        public ILocator listOfErrors => page.Locator(".govuk-label");


        public override async Task VerifyPage() => await Task.CompletedTask;

        public async Task VerifySummary(string name) => await Assertions.Expect(ErrorSummary).ToBeDisabledAsync();
    }
}
