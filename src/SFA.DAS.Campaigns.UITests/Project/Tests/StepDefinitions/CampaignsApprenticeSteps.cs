using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "apprentice")]
    public class CampaignsApprenticeSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);
        private ApprenticeHubPage _apprenticeHubPage;

        [Then(@"the apprentice sub headings are displayed")]
        public async Task ThenTheApprenticeSubHeadingsAreDisplayed() => await _apprenticeHubPage.VerifySubHeadings();

        [Given(@"the user navigates to Become An Apprentice page")]
        public async Task GivenTheUserNavigatesToBecomeAnApprenticePage() => _apprenticeHubPage = await GoToApprenticeshipHubPage();

        [Given(@"the user navigates to apprentice How do they work Page")]
        public async Task GivenTheUserNavigatesToApprenticeHowDoTheyWorkPage()
        {
            var page = await GoToApprenticeshipHubPage();

            var page1 = await page.NavigateToHowDoTheyWorkPage();

            await page1.VerifyHowDoTheyWorkPageSubHeadings();
        }

        [Given(@"the user navigates to Getting started Page")]
        public async Task GivenTheUserNavigatesToGettingStartedPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToGettingStarted();
        }

        [Given(@"the user navigates to Are ApprenticeShip Right For You Page")]
        public async Task GivenTheUserNavigatesToAreApprenticeShipRightForYouPage()
        {
            var page = await GoToApprenticeshipHubPage();

            var page1 = await page.NavigateToAreApprenticeShipRightForMe();

            await page1.VerifyApprenticeAreTheyRightForYouPageSubHeadings();
        }

        [Given(@"the user navigates to the browse apprenticeship page")]
        public async Task GivenTheUserNavigatesToBrowseApprenticeshipPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToBrowseApprenticeshipPage();
        }

        [Given(@"the user navigates to the Set Up Service Account page")]
        public async Task GivenTheUserNavigatesToTheSetUpServiceAccountPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToSetUpServiceAccountPage();
        }

        [Given("the user navigates to the Apprentice Stories page")]
        public async Task GivenTheUserNavigatesToTheApprenticeStoriesPage()
        {
            var page = await GoToApprenticeshipHubPage();

            var page1 = await page.NavigateToApprenticeStories();

            await page1.VerifyPageAsync();
        }


        [Given(@"the user navigates to the Site Map page")]
        public async Task GivenTheUserNavigatesToTheSiteMapPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToSiteMapPage();
        }

        private async Task<ApprenticeHubPage> GoToApprenticeshipHubPage() => await _stepsHelper.GoToApprenticeshipHubPage();
    }
}
