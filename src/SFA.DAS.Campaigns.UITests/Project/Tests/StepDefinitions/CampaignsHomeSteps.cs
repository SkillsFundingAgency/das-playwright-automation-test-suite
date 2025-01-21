using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class CampaignsHomeSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);

        private ApprenticeHubPage _apprenticeHubPage;

        private EmployerHubPage _employerHubPage;

        private InfluencersHubPage _influencersHubPage;

        [Given(@"the user navigates to Home page and verifies the content")]
        public async Task TheUserNavigatesToHomePageAndVerifiesTheContent() => await _stepsHelper.GoToCampaingnsHomePage();

        [Given(@"the user navigates to the employer page")]
        public async Task TheUserNavigatesToTheEmployerPage() => _employerHubPage = await _stepsHelper.GoToEmployerHubPage();

        [Given(@"the user navigates to the apprentice page")]
        public async Task TheUserNavigatesToTheApprenticePage() => _apprenticeHubPage = await _stepsHelper.GoToApprenticeshipHubPage();

        [Given(@"the user navigates to the influencers page")]
        public async Task GivenTheUserNavigatesToTheInfluencersPage() => _influencersHubPage = await _stepsHelper.GoToInfluencersHubPage();

        [Then("the apprentice fire it up tile card links are not broken")]
        public async Task ApprenticeFireItUpTileCardLinksAreNotBroken() => await _apprenticeHubPage.VerifySubHeadings();

        [Then("the employer fire it up tile card links are not broken")]
        public async Task EmployerFireItUpTileCardLinksAreNotBroken() => await _employerHubPage.VerifySubHeadings();

        [Then("the influencers fire it up tile card links are not broken")]
        public async Task InfluencersFireItUpTileCardLinksAreNotBroken() => await _influencersHubPage.VerifySubHeadings();
    }
}
