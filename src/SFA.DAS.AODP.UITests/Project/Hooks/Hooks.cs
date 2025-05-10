using System.Threading.Tasks;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.AO;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.Common;
using SFA.DAS.AODP.UITests.Project.Tests.Pages.DfE;
using SFA.DAS.AODP.UITests.Project.Tests.StepDefinitions.DfE;
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

            var url = UrlConfig.Aodp_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);

            DfeAdminHomePage _dfeAdminHomepage = new(context);
            AoHomePage _aoHomepage = new(context);
            AodpHomePage _aodpHomepage = new(context);
            AodpLoginPage _LoginPage = new(context);
            context.Set<DfeAdminHomePage>(_dfeAdminHomepage);
            context.Set<AoHomePage>(_aoHomepage);
            context.Set<AodpHomePage>(_aodpHomepage);
            context.Set<AodpLoginPage>(_LoginPage);
        }


        // Then following step may be obsolete since we have common navigation
        [BeforeScenario(Order = 22), Scope(Tag = "@ao_aodp")]
        public async Task AoNavigate()
        {
            var driver = context.Get<Driver>();

            var url = UrlConfig.Aodp_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);
            AodpHomePage _aodpHomepage = new(context);
            context.Set<AodpHomePage>(_aodpHomepage);
        }
    }
}