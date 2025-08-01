using SFA.DAS.Framework.Hooks;
using System.Linq;

namespace SFA.DAS.FATe.UITests.Hooks;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 30)]
    public async Task SetUpHelpers()
    {
        context.Set(new FATeDataHelper());

        var url = UrlConfig.FAT_BaseUrl;

        if (context.ScenarioInfo.Tags.Contains("e2e02"))
        {
            url = UrlConfig.Provider_BaseUrl;
        }

        await Navigate(url);
    }
}