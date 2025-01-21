using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Influencers;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "influencers")]
    public class CampaignsInfluencersSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);

        [Given(@"the user navigates to the browse apprenticeship page")]
        public async Task GivenTheUserNavigatesToTheBrowseApprenticeshipPage()
        {
            var page = await GoToInfluencersHubPage();

            await page.NavigateToBrowseApprenticeshipPage();
        }

        [Given(@"the user navigates to influencers How they work page")]
        public async Task GivenTheUserNavigatesToInfluencersHowTheyWorkPage()
        {
            var page = await GoToInfluencersHubPage();

            var page1 = await page.NavigateToHowDoTheyWorkPage();

            await page1.VerifyInfluencersHowTheyWorkPageSubHeadings();
        }

        [Given(@"the user navigates to influencers Request support page")]
        public async Task GivenTheUserNavigatesToInfluencersRequestSupportPage()
        {
            var page = await GoToInfluencersHubPage();

            var page1 = await page.NavigateToRequestSupportPage();

            await page1.VerifySubHeadings();
        }

        [Given(@"the user navigates to influencers Resource hub page")]
        public async Task GivenTheUserNavigatesToInfluencersResourceHubPage()
        {
            var page = await GoToInfluencersHubPage();

            var page1 = await page.NavigateToResourceHubPage();

            await page1.VerifySubHeadings();
        }

        [Given(@"the user navigates to influencers Apprentice ambassador network page")]
        public async Task GivenTheUserNavigatesToInfluencersApprenticeAmbassadorNetworkPage()
        {
            var page = await GoToInfluencersHubPage();

            var page1 = await page.NavigateToApprenticeAmbassadorNetworkPage();

            await page1.VerifySubHeadings();
        }

        private async Task<InfluencersHubPage> GoToInfluencersHubPage() => await _stepsHelper.GoToInfluencersHubPage();
    }
}
