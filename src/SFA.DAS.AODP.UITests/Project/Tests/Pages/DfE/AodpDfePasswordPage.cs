
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE
{
    public class AodpDfePasswordPage(ScenarioContext context) : AodpDfeHomePage(context)
    {


        private ILocator Password => page.GetByLabel("Password");
        private ILocator SignIn => page.GetByText("Sign in");


        public async Task<AodpDfePasswordPage> LoginAsReviewer()
        {
            await Password.Nth(0).FillAsync("TestApprenticeshipAutomation2025");
            await SignIn.Nth(1).ClickAsync();

            return await new AodpDfePasswordPage(context).LoginAsReviewer();
        }
    }
}
