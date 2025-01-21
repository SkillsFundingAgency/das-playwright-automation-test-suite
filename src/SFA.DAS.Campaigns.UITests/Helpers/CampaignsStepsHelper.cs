using SFA.DAS.Campaigns.UITests.Project.Tests.Pages;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Helpers
{
    public class CampaignsStepsHelper(ScenarioContext context)
    {
        public async Task<CampaingnsHomePage> GoToCampaingnsHomePage() => await new CampaingnsHomePage(context).AcceptCookieAndAlert();

        public async Task<ApprenticeHubPage> GoToApprenticeshipHubPage()
        {
            var page = await GoToCampaingnsHomePage();

            return await page.NavigateToApprenticeshipHubPage();
        }

        public async Task<EmployerHubPage> GoToEmployerHubPage()
        {
            var page = await GoToCampaingnsHomePage();

            return await page.NavigateToEmployerHubPage();
        }

        public async Task<InfluencersHubPage> GoToInfluencersHubPage()
        {
            var page = await GoToCampaingnsHomePage();
            
            return await page.NavigateToInfluencersHubPage();
        }
    }
}
