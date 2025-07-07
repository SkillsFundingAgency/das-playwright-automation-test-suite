
using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class AoStartPage(ScenarioContext context) : AodpHomePage(context)
    {

        private ILocator AoStartPage1 => page.GetByText("Apply for qualification funding");
        private ILocator StartButton => page.Locator(".govuk-button--start");

        public async Task ClickStartButton() => await StartButton.ClickAsync();

        public override async Task VerifyPage() => await Assertions.Expect(AoStartPage1).ToBeVisibleAsync();

    }
}
