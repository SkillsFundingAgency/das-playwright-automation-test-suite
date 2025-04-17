

using SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE;
namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.DfE
{
    [Binding]
    public class DfeLoginSteps
    {

        // private readonly LoginSteps _login = new(context);

        private readonly AodpLoginPage _loginPage;


        public DfeLoginSteps(ScenarioContext context)
        {
            _loginPage = context.Get<AodpLoginPage>();
        }

        [Given(@"a user with (.*) role should able to login")]
        public async Task LoginToApplicationAsDfe(string role) => await _loginPage.LoginAsDfeUser(role);


    }
}
