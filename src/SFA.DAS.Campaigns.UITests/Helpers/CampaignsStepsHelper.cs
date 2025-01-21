using SFA.DAS.Campaigns.UITests.Project.Tests.Pages;
using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

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

        //public EmployerHubPage GoToEmployerHubPage() => GoToCampaingnsHomePage().NavigateToEmployerHubPage();

        //public InfluencersHubPage GoToInfluencersHubPage() => GoToCampaingnsHomePage().NavigateToInfluencersHubPage();
    }
}
