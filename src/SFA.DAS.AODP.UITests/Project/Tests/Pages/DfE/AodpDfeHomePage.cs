
using SFA.DAS.AODP.UITests.Project.Tests.Pages.AO;

namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class AodpDfeHomePage(ScenarioContext context) : AodpDfeBasePage(context)
    {
        private ILocator StartButton => page.GetByText("Start now");


        public override async Task VerifyPage() => await Task.CompletedTask;

        public async Task<AodpAoLoginPage> NavigateToLoginPage()
        {
            await StartButton.ClickAsync();

            return new AodpAoLoginPage(context);
        }

    }
}
