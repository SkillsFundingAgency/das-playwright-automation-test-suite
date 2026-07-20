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

        [Given(@"^the user navigates to Become An Apprentice page$")]
        public async Task GivenTheUserNavigatesToBecomeAnApprenticePage() => _apprenticeHubPage = await GoToApprenticeshipHubPage();

        [Given(@"^the user navigates to About Apprenticeships Page$")]
        public async Task GivenTheUserNavigatesToAboutApprenticeshipsPage()
        {
            var page = await GoToApprenticeshipHubPage();

            var aboutPage = await page.NavigateToAboutApprenticeshipsPage();

            await aboutPage.VerifyAboutApprenticeshipsPageSubHeadings();
        }

        [Given(@"^the user navigates to Preparing For An Apprenticeship Page$")]
        public async Task GivenTheUserNavigatesToPreparingForAnApprenticeshipPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToPreparingForAnApprenticeshipPage();
        }

        [Given(@"^the user navigates to Is An Apprenticeship Right For You Page$")]
        public async Task GivenTheUserNavigatesToIsAnApprenticeshipRightForYouPage()
        {
            var page = await GoToApprenticeshipHubPage();

            var rightForYouPage = await page.NavigateToIsAnApprenticeshipRightForYouPage();

            await rightForYouPage.VerifyIsAnApprenticeshipRightForYouPageSubHeadings();
        }

        [Given(@"^the user navigates to the browse apprenticeship page$")]
        public async Task GivenTheUserNavigatesToBrowseApprenticeshipPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToBrowseApprenticeshipPage();
        }

        [Given(@"^the user navigates to Create An Account To Search And Apply Page$")]
        public async Task GivenTheUserNavigatesToCreateAnAccountToSearchAndApplyPage()
        {
            var page = await GoToApprenticeshipHubPage();

            await page.NavigateToCreateAccountPage();
        }

        [Given("^the user navigates to the Apprentice Stories page$")]
        public async Task GivenTheUserNavigatesToTheApprenticeStoriesPage()
        {
            var page = await GoToApprenticeshipHubPage();

            var page1 = await page.NavigateToApprenticeStories();

            await page1.VerifyPageAsync();
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
