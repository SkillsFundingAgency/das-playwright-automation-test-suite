using Microsoft.Playwright;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class Driver(IPage page, ObjectContext objectContext)
{
    public IPage Page => page;

    private readonly ScreenShotHelper screenShotHelper = new(page, objectContext);

    public async Task GotoAsync(string url)
    {
        objectContext.SetDebugInformation($"Navigated to {url}");

        await page.GotoAsync(url);
    }

    public async Task ScreenshotAsync(bool isTestComplete) => await screenShotHelper.ScreenshotAsync(isTestComplete);

    public async Task SubmitAsync(Func<IPage, Task> func, string name)
    {
        objectContext.SetDebugInformation($"Submitted '{name}' page");

        await ScreenshotAsync(false);

        await func(page);
    }

    public async Task ClickAsync(Func<IPage, Task> func, string name)
    {
        objectContext.SetDebugInformation($"Clicked '{name}'");

        await func(page);
    }

    public async Task FillAsync(Func<IPage, ILocator> locator, string value)
    {
        objectContext.SetDebugInformation($"Entered '{value}'");

        await locator(page).FillAsync(value);
    }
}
