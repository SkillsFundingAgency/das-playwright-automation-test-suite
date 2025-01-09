using Microsoft.Playwright;
using SFA.DAS.Framework;
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
           await _context.Get<IPage>().GotoAsync(UrlConfig.CA_BaseUrl);
        }
    }
}