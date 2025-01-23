using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class InitializeDriver
{
    private readonly Task<IBrowserType> _iBrowserType;

    public IBrowserType IBrowserType => _iBrowserType.Result;

    public static BrowserType BrowserType;

    public static bool isCloud;

    public InitializeDriver()
    {
        _iBrowserType = Task.Run(Initialize);
    }

    public static async Task<IBrowserType> Initialize()
    {
        BrowserType = GetBrowserTypeFromEnv();

        isCloud = BrowserType == BrowserType.Cloud;

        var factory = new DriverFactory(isCloud ? BrowserType.Chrome : BrowserType);

        return await factory.CreateDriver();
    }

    public static BrowserType GetBrowserTypeFromEnv()
    {
        string envBrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE");

        string browserType = string.IsNullOrEmpty(envBrowserType) ? "Chrome" : envBrowserType;

        if (!Enum.TryParse(browserType, true, out BrowserType type))
            type = BrowserType.Chrome;

        return type;
    }
}
