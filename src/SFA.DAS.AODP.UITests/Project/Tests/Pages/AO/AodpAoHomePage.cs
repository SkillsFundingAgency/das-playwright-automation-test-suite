
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class AodpAoHomePage(ScenarioContext context) : AodpAoBasePage(context)
    {
        private ILocator StartButton => page.GetByText("Start now");




        public async Task<AodpAoHomePage> NavigateToLoginPage()
        {
            await StartButton.ClickAsync();

            return new AodpAoLoginPage(context);
        }
    }
}
