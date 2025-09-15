namespace SFA.DAS.Framework;

public class DriverFactory(BrowserType browserType)
{
    public async Task<IBrowserType> CreateDriver()
    {
        var playwright = await Playwright.CreateAsync();

        return browserType switch
        {
            BrowserType.Chrome => playwright.Chromium,
            BrowserType.Safari => playwright.Webkit,
            BrowserType.Firefox => playwright.Firefox,
            _ => playwright.Chromium,
        };
    }
}
