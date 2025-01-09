using Microsoft.Playwright;
using SFA.DAS.Framework;
using System.Linq;
using TechTalk.SpecFlow;

namespace SFA.DAS.Campaigns.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        private readonly ScenarioContext _context = context;

        [BeforeScenario(Order = 30)]
        public async void SetUpHelpers()
        {
            string url = UrlConfig.CA_BaseUrl;

            //if (_context.ScenarioInfo.Tags.Contains("openemp")) url = UrlConfig.Live_Employer_BaseUrl;

            await _context.Get<IPage>().GotoAsync(url);
        }
    }
}