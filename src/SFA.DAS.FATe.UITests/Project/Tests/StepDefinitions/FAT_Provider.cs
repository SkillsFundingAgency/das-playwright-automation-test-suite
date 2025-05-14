using Polly;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FAT_Provider(ScenarioContext context)
    {
        [Given(@"the provider logs into portal")]
        public async Task GivenTheProviderLogsIntoPortal() => await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
    }
}
