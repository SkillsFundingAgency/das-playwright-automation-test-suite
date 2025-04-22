

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class AoReviewPage(ScenarioContext context) : AoStartPage(context)
    {

        public ILocator AO_Requests_Page => page.Locator("//h1[.=\"Requests for funding\"]");
        private ILocator StartButton => page.Locator(".govuk-button--start");

        public override async Task VerifyPage() => await Assertions.Expect(AO_Requests_Page).ToBeVisibleAsync();
        public async Task StartApplication() => await StartButton.ClickAsync();
    }
}
