using System.Threading.Tasks;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;

namespace SFA.DAS.AODP.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 22)]
        public async Task Navigate()
        {
            var driver = context.Get<Driver>();

            var url = UrlConfig.Aodp_DfeAdmin_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);
        }
    }
}