
namespace SFA.DAS.Framework.Hooks;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    
    private static IPlaywright _playwright;

    private readonly ScenarioContext _context = context;

    private static bool _isHeadless;

    private static BrowserType _browserType;

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        _playwright = await Playwright.CreateAsync();

        _isHeadless = GetheadlessFromEnv();

        _browserType = GetBrowserTypeFromEnv();
    }

    [BeforeScenario(Order = 8)]
    public async Task BeforeScenario()
    {
        var objectContext = _context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetupPlaywrightDriver Order = 8 hook");

        var browserType = await CreateDriver(_playwright, _browserType);

        var browser = await browserType.LaunchAsync(new()
        {
            Headless = _isHeadless,
            Args = ["--start-maximized"]
        });

        var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        if (ShouldTrace())
        {
            await browserContext.Tracing.StartAsync(new()
            {
                Title = _context.ScenarioInfo.Title,
                Screenshots = true,
                Snapshots = true
            });
        }

        var page = await browserContext.NewPageAsync();

        _context.Set(new Driver(browser, browserContext, page, objectContext));
    }

    [AfterScenario(Order = 98)]
    public async Task AfterScenario()
    {
        var x = _context.TryGetValue(out Driver driver);

        if (!x) return;

        await _context.Get<TryCatchExceptionHelper>().AfterScenarioException(() => driver.ScreenshotAsync(true));

        if (ShouldTrace())
        {
            var tracefileName = $"PLAYWRIGHTDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

            var tracefilePath = $"{_context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

            await _context.Get<TryCatchExceptionHelper>().AfterScenarioException(
                async () =>
                {
                    await driver.BrowserContext.Tracing.StopAsync(new()
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

        await driver.BrowserContext.CloseAsync(new BrowserContextCloseOptions { Reason = $"Closed browser context for scenario: {_context.ScenarioInfo.Title}" });

    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _playwright?.Dispose();
    }

    //public static async Task MarkTestStatus(string status, string reason, IPage page)
    //{
    //    await page.EvaluateAsync("_ => {}", "browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"" + status + "\", \"reason\": \"" + reason + "\"}}");
    //}

    private bool ShouldTrace() => !_context.ScenarioInfo.Tags.Contains("donottracelogin");

    private static bool GetheadlessFromEnv()
    {
        string isHeadlessVar = Environment.GetEnvironmentVariable("headless");

        return !string.IsNullOrEmpty(isHeadlessVar) && isHeadlessVar.ContainsCompareCaseInsensitive("true");
    }

    private static BrowserType GetBrowserTypeFromEnv()
    {
        string envBrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE");

        string browserType = string.IsNullOrEmpty(envBrowserType) ? "Chromium" : envBrowserType;

        if (!Enum.TryParse(browserType, true, out BrowserType type))
            type = BrowserType.Chromium;

        return type;
    }

    private static async Task<IBrowserType> CreateDriver(IPlaywright playwright, BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Webkit => playwright.Webkit,
            BrowserType.Firefox => playwright.Firefox,
            _ => playwright.Chromium,
        };
    }
}
