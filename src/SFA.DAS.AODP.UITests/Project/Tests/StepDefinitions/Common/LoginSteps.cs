

using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.Common
{
    [Binding]
    public class LoginSteps(ScenarioContext context)
    {
        [Given(@"a user with (.*) role should able to login")]
        public async Task LoginToApplicationAsUser(string role) => await LoginUserRole(role);


        private async Task LoginUserRole(string role) => await new AodpHomePage(context).NavigateToLoginPage();
    }
}
