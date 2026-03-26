namespace SFA.DAS.Framework;

public class DriverFactory(BrowserType browserType)
{
    public async Task<IBrowserType> CreateDriver()
    {
        var playwright = await Playwright.CreateAsync();

        return browserType switch
        {
            BrowserType.Webkit => playwright.Webkit,
            BrowserType.Firefox => playwright.Firefox,
            _ => playwright.Chromium,
        };
    }
}
