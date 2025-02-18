using System.Threading.Tasks;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;

namespace SFA.DAS.AODP.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 22), Scope(Tag = "@dfe_aodp")]
        public async Task DfeNavigate()
        {
            var driver = context.Get<Driver>();

            var url = UrlConfig.Aodp_Dfe_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);
        }


        [BeforeScenario(Order = 22), Scope(Tag = "@ao_aodp")]
        public async Task AoNavigate()
        {
            var driver = context.Get<Driver>();

            var url = UrlConfig.Aodp_AO_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);
        }
    }
}