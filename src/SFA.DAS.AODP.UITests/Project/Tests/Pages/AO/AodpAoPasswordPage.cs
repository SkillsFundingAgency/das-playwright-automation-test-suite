
namespace SFA.DAS.AODP.UITests.Project.Tests.Pages.AO
{
    public class AodpAoPasswordPage(ScenarioContext context) : AodpAoHomePage(context)
    {


        private ILocator Password => page.GetByLabel("Password");
        private ILocator SignIn => page.GetByText("Sign in");


        public async Task<AodpAoPasswordPage> LoginAsReviewer()
        {
            await Password.Nth(0).FillAsync("TestApprenticeshipAutomation2025");
            await SignIn.Nth(1).ClickAsync();

            return new AodpAoPasswordPage(context);
        }
    }
}
