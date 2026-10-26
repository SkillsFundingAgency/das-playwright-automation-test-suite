namespace SFA.DAS.Framework.Hooks;

public abstract class FrameworkBaseHooks(ScenarioContext context)
{
    protected readonly ScenarioContext context = context;

    protected async Task OpenNewTab()
    {
        var driver = context.Get<Driver>();

        var page = await driver.BrowserContext.NewPageAsync();

        driver.Page = page;
    }

    protected async Task Navigate(string url)
    {
        var driver = context.Get<Driver>();

        context.Get<ObjectContext>().SetDebugInformation(url);

        if (driver.Browser == null)     //its an api test, so no need to navigate to a page
        {
            return;
        }

        await driver.Page.GotoAsync(url);
    }
}
