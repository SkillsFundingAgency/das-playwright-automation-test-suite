

using SFA.DAS.AODP.UITests.Project.Tests.Pages.AO;
using SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.Common;

namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.AO
{
    [Binding]
    public class AoLoginSteps(ScenarioContext context)
    {

        private readonly LoginSteps _login = new(context);


        [Given(@"a user with (.*) role ao should able to login")]
        public async Task LoginToApplicationAsAO(string role) => await _login.LoginToApplicationAsUser(role);


        [Then(@"access the ao Dashboard as (.*)")]
        public async Task VerifyTheDashBoardVisibility(string role) => await new AodpAoLoginPage(context).LoginAsReviewer();


        [Then(@"Navigate to AODP AO start page and verify the content")]
        public async Task VerifyTheStartPageVisibility() => await new AodpAoHomePage(context).VerifyPage();
    }
}
