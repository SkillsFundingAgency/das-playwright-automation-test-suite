

namespace SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.DfE
{
    [Binding]
    public class LoginSteps(ScenarioContext context)
    {
        public ScenarioContext Context { get; } = context;

        [Given(@"a user with (.*) role should able to login")]
        public async Task LoginToApplicationAsUser(string role) => await LoginUserRole(role);



        private async Task LoginUserRole(string role)
        {
            // Call Login function with user role
            await Task.CompletedTask;
        }

    }
}
