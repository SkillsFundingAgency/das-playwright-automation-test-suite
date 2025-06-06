using System.Linq;

namespace SFA.DAS.FATe.UITests.Hooks
{
    [Binding]
    public class Hooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 30)]
        public async Task SetUpHelpers()
        {
            var driver = context.Get<Driver>();

            context.Set(new FATeDataHelper());

            var url = UrlConfig.FAT_BaseUrl;

            if (context.ScenarioInfo.Tags.Contains("e2e02"))
            {
                url = UrlConfig.Provider_BaseUrl;
            }

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);
        }
    }
}