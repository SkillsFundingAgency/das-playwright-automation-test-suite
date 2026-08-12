using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "apprentice")]
    public class CampaignsApprenticeSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);
        private ApprenticeHubPage _apprenticeHubPage;

        [Then(@"^the apprentice sub headings are displayed$")]
        public async Task ThenTheApprenticeSubHeadingsAreDisplayed() => await _apprenticeHubPage.VerifySubHeadings();

        [Given(@"^the user navigates to (?:the )?Become An Apprentice page$")]
        public async Task GivenTheUserNavigatesToBecomeAnApprenticePage() => _apprenticeHubPage = await GoToApprenticeshipHubPage();

        [When(@"^the user clicks on the apprentice card ""(.*)""$")]
        public async Task WhenTheUserClicksOnTheApprenticeCard(string cardName)
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToApprenticeCard(cardName);
        }

        [Given(@"^the user navigates to the browse apprenticeship page$")]
        public async Task GivenTheUserNavigatesToBrowseApprenticeshipPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToApprenticeCard("Browse by interest");
        }

        [Given(@"^the user navigates to the Site Map page$")]
        public async Task GivenTheUserNavigatesToTheSiteMapPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToSiteMapPage();
        }

        private async Task<ApprenticeHubPage> GoToApprenticeshipHubPage() => await _stepsHelper.GoToApprenticeshipHubPage();
    }
}