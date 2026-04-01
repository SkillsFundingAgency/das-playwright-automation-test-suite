
namespace SFA.DAS.Framework.Hooks;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    private static InitializeDriver driver;

    private IBrowserContext browserContext;

    private static IBrowser Browser;

    private static readonly DateTime Date;

    static PlaywrightHooks()
    {
        Date = DateTime.Now;
    }

    [BeforeTestRun]
    public static async Task BeforeAll()
    {
        driver = new InitializeDriver();

        string isHeadlessVar = Environment.GetEnvironmentVariable("headless");

        bool isHeadless = !string.IsNullOrEmpty(isHeadlessVar) && isHeadlessVar.ContainsCompareCaseInsensitive("true");

        Browser = await driver.IBrowserType.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = isHeadless,
            Args = ["--start-maximized"],
        });
    }

    [AfterTestRun]
    public static async Task AfterAll()
    {
        await Browser.CloseAsync();
    }

    [BeforeScenario(Order = 8)]
    public async Task SetupPlaywrightDriver()
    {
		var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetupPlaywrightDriver Order = 8 hook");

        browserContext = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        if (ShouldTrace())
        {
            await browserContext.Tracing.StartAsync(new()
            {
                Title = context.ScenarioInfo.Title,
                Screenshots = true,
                Snapshots = true
            });
        }

        var page = await browserContext.NewPageAsync();

        context.Set(new Driver(browserContext, page, context.Get<ObjectContext>()));
    }

    [AfterScenario(Order = 98)]
    public async Task StopTracing()
    {
        var pDriver = context.Get<Driver>();

        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(() => pDriver.ScreenshotAsync(true));

        if (ShouldTrace())
        {
            var tracefileName = $"PLAYWRIGHTDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

            var tracefilePath = $"{context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

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

        // Marking test status in BrowserStack if running in cloud
        //if (isCloud)
        //{
        //    if (context.TestError == null)
        //        await MarkTestStatus("passed", string.Empty, pDriver.Page);
        //    else
        //        await MarkTestStatus("failed", context.TestError.Message, pDriver.Page);
        //}

        await browserContext.CloseAsync();
    }

    //public static async Task MarkTestStatus(string status, string reason, IPage page)
    //{
    //    await page.EvaluateAsync("_ => {}", "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"" + status + "\", \"reason\": \"" + reason + "\"}}");
    //}

    private bool ShouldTrace() => !context.ScenarioInfo.Tags.Contains("donottracelogin");
}
