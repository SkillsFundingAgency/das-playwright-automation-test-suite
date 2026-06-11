namespace SFA.DAS.Framework;

public class Driver(IBrowser browser, IBrowserContext context, IPage page, ObjectContext objectContext)
{
    public IBrowser Browser { get; } = browser;
    public IBrowserContext BrowserContext { get; } = context;
    public IPage Page = page;

    private readonly ScreenShotHelper screenShotHelper = new(objectContext);

    public async Task ScreenshotAsync(bool isTestComplete) => await screenShotHelper.ScreenshotAsync(Page, isTestComplete);

    public async Task SelectRowFromTable(string byLinkText, string byKey, ILocator nextPage)
    {
        do
        {
            var link = Page.GetByRole(AriaRole.Row, new() { Name = byKey }).GetByRole(AriaRole.Link);

            if (await link.IsVisibleAsync())
            {
                await link.ClickAsync();

                objectContext.SetDebugInformation($"Clicked LinkText - '{byLinkText}' using Key - '{byKey}'");

                break;
            }
            await nextPage.ClickAsync();

        } while (await nextPage.IsVisibleAsync());
    }
}
