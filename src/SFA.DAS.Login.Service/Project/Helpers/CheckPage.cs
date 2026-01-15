namespace SFA.DAS.Login.Service.Project.Helpers;

public abstract class CheckPage(ScenarioContext context) : BasePage(context)
{
    protected abstract string PageTitle { get; }

    protected abstract ILocator PageLocator { get; }

    protected virtual int VerifyPageTimeOutinMs => 20000;

    public virtual async Task<bool> IsPageDisplayed()
    {
        objectContext.SetDebugInformation($"Check page using Page title : '{PageTitle}'");

        try
        {
            await VerifyPage();

            objectContext.SetDebugInformation($"'{await PageLocator.TextContentAsync()}' page is displayed");

            return true;
        }
        catch (Exception ex)
        {
            objectContext.SetDebugInformation($"CheckPage for {PageTitle} resulted in {ex.Message}");

            return false;
        }
    }

    public override async Task VerifyPage() => await Assertions.Expect(PageLocator).ToContainTextAsync(PageTitle, new LocatorAssertionsToContainTextOptions { IgnoreCase = true, Timeout = VerifyPageTimeOutinMs });

}



public abstract class CheckMultipleHomePage(ScenarioContext context) : BasePage(context)
{
    public abstract string[] PageIdentifierCss { get; }

    public abstract string[] PageTitles { get; }

    protected string Identifier => $"{string.Join(", ", PageIdentifierCss.Select(x => x).ToList())}";

    public override async Task VerifyPage() => await Task.CompletedTask;

    protected async Task<bool> ActualDisplayedPage(string page)
    {
        var list = await ActualDisplayedPage();

        objectContext.SetDebugInformation($"{string.Join(" and ", list.Select(x => $"'{x}'"))} is/are displayed");

        return list.Count > 0 && list.Any(x => x.Contains(page));
    }

    private async Task<IReadOnlyList<string>> ActualDisplayedPage()
    {
        string displayedPage = string.Empty;

        objectContext.SetDebugInformation($"Check that {string.Join(" OR ", PageTitles.Select(x => $"'{x}'"))} is displayed using Page title, Identifier '{Identifier}'");

        objectContext.SetDebugInformation($"Navigated to page - {await page.TitleAsync()}");

        var list = await page.Locator(Identifier).AllTextContentsAsync();

        return list;
    }
}