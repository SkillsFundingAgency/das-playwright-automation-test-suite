using SFA.DAS.Campaigns.UITests.Helpers;
using SFA.DAS.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 30)]
        public async Task SetUpHelpers()
        {
            var driver = context.Get<Driver>();

            context.Set(new CampaignsDataHelper());

            await driver.GotoAsync(UrlConfig.CA_BaseUrl);
        }

        [AfterScenario(Order = 30)]
        public async Task Screenshot()
        {
            var driver = context.Get<Driver>();

            await driver.ScreenshotAsync(true);
        }
    }
}