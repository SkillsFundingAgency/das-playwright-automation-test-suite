

using SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE;

namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.DfE
{
    [Binding]
    public class DfeLoginSteps(ScenarioContext context)
    {

        private readonly LoginSteps _login = new(context);


        [Given(@"a user with (.*) role dfe should able to login")]
        public async Task LoginToApplicationAsDfe(string role) => await _login.LoginToApplicationAsUser(role);

        [Then(@"access the dfe Dashboard as (.*)")]
        public async Task VerifyTheDashBoardVisibility(string role) => await new AodpDfeLoginPage().VerifyPageVisibility();


        [Then(@"Navigate to AODP DFE start page and verify the content")]
        public async Task VerifyTheStartPageVisibility() => await new AodpDfeLoginPage().VerifyPageVisibility();
    }
}
