using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    private static Driver driver;

    [BeforeTestRun]
    public static Task BeforeAll()
    {
        driver = new Driver();

        return Task.CompletedTask;
    }

    [BeforeScenario(Order = 4)]
    public async Task SetupPlaywrightDriver()
    {
        var Page = await driver.BrowserContext.NewPageAsync();

        context.Set(Page);
    }

    [AfterTestRun]
    public static void AfterAll()
    {
        driver.Dispose();
    }
}
