using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class Driver : IDisposable
{
    private readonly Task<IPage> _page;

    private IBrowserContext _context;

    public IPage Page => _page.Result;

    public Driver()
    {
        _page = Task.Run(InitializePlaywright);
    }

    public async Task<IPage> InitializePlaywright()
    {
        var factory = new DriverFactory(GetBrowserTypeFromEnv());

        var driver = await factory.CreateDriver();

        var browser = await driver.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });

        _context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        return await _context.NewPageAsync();
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
        _context?.CloseAsync();
    }
}
