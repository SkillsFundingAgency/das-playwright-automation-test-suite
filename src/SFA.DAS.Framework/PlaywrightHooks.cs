using Microsoft.Playwright;
using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    private static InitializeDriver driver;

    [BeforeTestRun]
    public static Task BeforeAll()
    {
        driver = new InitializeDriver();

        return Task.CompletedTask;
    }

    [BeforeScenario(Order = 4)]
    public async Task SetupPlaywrightDriver()
    {
        var browserContext = await driver.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        var page = await browserContext.NewPageAsync();

        context.Set(new Driver(page, context.Get<ObjectContext>()));
    }

    [AfterTestRun]
    public static void AfterAll()
    {
        driver.Dispose();
    }
}
