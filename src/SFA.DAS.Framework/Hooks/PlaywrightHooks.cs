using Microsoft.Playwright;
using NUnit.Framework;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework.Hooks;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    private static InitializeDriver driver;

    private IBrowserContext browserContext;

    [BeforeTestRun]
    public static Task BeforeAll()
    {
        driver = new InitializeDriver();

        return Task.CompletedTask;
    }

    [BeforeScenario(Order = 8)]
    public async Task SetupPlaywrightDriver()
    {
        browserContext = await driver.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        //if (context.ScenarioInfo.Tags.Contains("donottracelogin") == false)
        //{
        await browserContext.Tracing.StartAsync(new()
        {
            Title = context.ScenarioInfo.Title,
            Screenshots = true,
            Snapshots = true
        });
        //}

        var page = await browserContext.NewPageAsync();

        Assertions.SetDefaultExpectTimeout(10000);

        context.Set(new Driver(browserContext, page, context.Get<ObjectContext>()));
    }

    [AfterScenario(Order = 98)]
    public async Task StopTracing()
    {
        //if (context.ScenarioInfo.Tags.Contains("donottracelogin")) return;

        var tracefileName = $"PLAYWRIGHTDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

        var tracefilePath = $"{context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(() => context.Get<Driver>().ScreenshotAsync(true));

        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(
            async () =>
            {
                await browserContext.Tracing.StopAsync(new()
                {
                    Path = tracefilePath
                });

                TestContext.AddTestAttachment(tracefilePath, tracefileName);
            }
            );
    }
}
