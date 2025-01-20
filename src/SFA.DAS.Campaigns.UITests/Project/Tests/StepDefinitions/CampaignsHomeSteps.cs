using SFA.DAS.Campaigns.UITests.Helpers;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class CampaignsHomeSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);

        private ApprenticeHubPage _apprenticeHubPage;

        //private EmployerHubPage _employerHubPage;

        //private InfluencersHubPage _influencersHubPage;

        [Given(@"the user navigates to Home page and verifies the content")]
        public async Task GivenTheUserNavigatesToHomePageAndVerifiesTheContent() => await _stepsHelper.GoToCampaingnsHomePage();

        //[Given(@"the user navigates to the employer page")]
        //public void GivenTheUserNavigatesToTheEmployerPage() => _employerHubPage = _stepsHelper.GoToEmployerHubPage();

        [Given(@"the user navigates to the apprentice page")]
        public async Task GivenTheUserNavigatesToTheApprenticePage() => _apprenticeHubPage = await _stepsHelper.GoToApprenticeshipHubPage();

        //[Given(@"the user navigates to the influencers page")]
        //public void GivenTheUserNavigatesToTheInfluencersPage() => _influencersHubPage = _stepsHelper.GoToInfluencersHubPage();

        [Then(@"the fire it up tile card links are not broken")]
        public async Task ThenTheFireItUpTileCardLinksAreNotBroken()
        {
            await _apprenticeHubPage?.VerifySubHeadings();

            //_employerHubPage?.VerifySubHeadings();

            //_influencersHubPage?.VerifySubHeadings();
        }
    }
}
