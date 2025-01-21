using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class CampaingnsCommomSteps(ScenarioContext context)
    {
        [Then(@"the user can search for an apprenticeship")]
        public async Task ThenTheUserCanSearchForAnApprenticeship() => await new BrowseApprenticeshipPage(context).SearchForAnApprenticeship();

    }
}
