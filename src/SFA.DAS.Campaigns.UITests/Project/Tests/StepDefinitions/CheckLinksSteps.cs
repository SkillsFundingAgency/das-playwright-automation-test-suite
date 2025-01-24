using SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class CheckLinksSteps(ScenarioContext context)
    {
        [Then(@"the links are not broken")]
        public async Task ThenTheLinksAreNotBroken() => await new CampaingnsVerifyLinks(context).VerifyLinks();

        [Then(@"the video links are not broken")]
        public async Task ThenTheVideoLinksAreNotBroken() => await new CampaingnsVerifyLinks(context).VerifyVideoLinks();
    }
}
