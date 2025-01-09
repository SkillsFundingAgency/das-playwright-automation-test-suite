using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class Driver : IDisposable
{
    private readonly Task<IBrowserContext> _browsercontext;

    public IBrowserContext BrowserContext => _browsercontext.Result;

    public Driver()
    {
        _browsercontext = Task.Run(InitializePlaywright);
    }

    public static async Task<IBrowserContext> InitializePlaywright()
    {
        var factory = new DriverFactory(GetBrowserTypeFromEnv());

        var driver = await factory.CreateDriver();

        var browser = await driver.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });

        return await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });
    }

    public static BrowserType GetBrowserTypeFromEnv()
    {
        string envBrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE");

        string browserType = string.IsNullOrEmpty(envBrowserType) ? "Chrome" : envBrowserType;

        if (!Enum.TryParse(browserType, true, out BrowserType type))
            throw new ArgumentException($"Invalid browser type: {browserType}");

        return type;
    }

    public void Dispose()
    {
        BrowserContext?.CloseAsync();
    }
}
