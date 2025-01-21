using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class InitializeDriver
{
    private readonly Task<IBrowser> _browser;

    public IBrowser Browser => _browser.Result;

    public InitializeDriver()
    {
        _browser = Task.Run(Initialize);
    }

    public static async Task<IBrowser> Initialize()
    {
        var factory = new DriverFactory(GetBrowserTypeFromEnv());

        var driver = await factory.CreateDriver();

        return await driver.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = ["--start-maximized"] 
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

    public async Task Dispose()
    {
        await Browser?.CloseAsync();
    }
}
