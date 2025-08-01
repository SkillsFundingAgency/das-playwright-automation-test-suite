using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.Campaigns.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
    {
        [BeforeScenario(Order = 30)]
        public async Task SetUpHelpers()
        {
            context.Set(new CampaignsDataHelper());

            await Navigate(UrlConfig.CA_BaseUrl);
        }
    }
}