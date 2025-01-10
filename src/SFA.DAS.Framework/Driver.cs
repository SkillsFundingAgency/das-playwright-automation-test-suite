using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class Driver : IDisposable
{
    private readonly Task<IBrowser> _browser;

    public IBrowser Browser => _browser.Result;

    public Driver()
    {
        _browser = Task.Run(InitializePlaywright);
    }

    public static async Task<IBrowser> InitializePlaywright()
    {
        var factory = new DriverFactory(GetBrowserTypeFromEnv());

        var driver = await factory.CreateDriver();

        return await driver.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
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
        Browser?.CloseAsync();
    }
}
