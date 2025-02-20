
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.Common
{
    public class AodpHomePage(ScenarioContext context) : AodpBasePage(context)
    {
        private ILocator StartButton => page.GetByText("Start now");




        public async Task<AodpBasePage> NavigateToLoginPage()
        {
            await StartButton.ClickAsync();

            return this;
        }

    }
}
