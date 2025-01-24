using Microsoft.Playwright;
using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class Driver(IBrowserContext browserContext, IPage page, ObjectContext objectContext)
{
    public IBrowserContext BrowserContext => browserContext;

    public IPage Page => page;

    private readonly ScreenShotHelper screenShotHelper = new(page, objectContext);

    public async Task ScreenshotAsync(bool isTestComplete) => await screenShotHelper.ScreenshotAsync(isTestComplete);
}
