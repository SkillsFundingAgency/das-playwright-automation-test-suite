namespace SFA.DAS.Framework;

public class InitializeDriver
{
    private readonly Task<IBrowserType> _iBrowserType;

    public IBrowserType IBrowserType => _iBrowserType.Result;

    public InitializeDriver() => _iBrowserType = Task.Run(Initialize);

    public static async Task<IBrowserType> Initialize()
    {
        var factory = new DriverFactory(GetBrowserTypeFromEnv());

        return await factory.CreateDriver();
    }
        
    private static BrowserType GetBrowserTypeFromEnv()
    {
        string envBrowserType = Environment.GetEnvironmentVariable("BROWSER_TYPE");

        string browserType = string.IsNullOrEmpty(envBrowserType) ? "Chromium" : envBrowserType;

        if (!Enum.TryParse(browserType, true, out BrowserType type))
            type = BrowserType.Chromium;

        return type;
    }
}
