namespace SFA.DAS.Login.Service.Project.Helpers;

public abstract class CheckPage(ScenarioContext context) : BasePage(context)
{
    protected abstract string PageTitle { get; }

    protected abstract ILocator PageLocator { get; }

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

    public override async Task VerifyPage() => await Assertions.Expect(PageLocator).ToContainTextAsync(PageTitle);

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

        return list.Count > 0 && list.Any(x => x.Contains(page));
    }

    private async Task<IReadOnlyList<string>> ActualDisplayedPage()
    {
        string displayedPage = string.Empty;

        objectContext.SetDebugInformation($"Check that {string.Join(" OR ", PageTitles.Select(x => $"'{x}'"))} is displayed using Page title");

        var list = await page.Locator(Identifier).AllTextContentsAsync();

        return list;
    }
}