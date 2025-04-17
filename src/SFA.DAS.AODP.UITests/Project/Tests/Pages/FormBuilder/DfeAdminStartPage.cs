
using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class DfeAdminStartPage(ScenarioContext context) : AodpHomePage(context)
    {

        private ILocator DfeStartPage => page.GetByText("Admin");
        private ILocator StartButton => page.Locator(".govuk-button--start");

        public async Task ClickStartButton() => await StartButton.ClickAsync();

        public override async Task VerifyPage() => await Assertions.Expect(DfeStartPage).ToBeVisibleAsync();

    }
}
