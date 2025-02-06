namespace SFA.DAS.Login.Service.Project.Helpers;

public abstract class CheckPage(ScenarioContext context) : BasePage(context)
{
    protected abstract string PageTitle { get; }

    protected abstract ILocator PageLocator { get; }

    public virtual async Task<bool> IsPageDisplayed()
    {
        objectContext.SetDebugInformation($"Check page using Page title : '{PageTitle}'");

        if (await PageLocator.IsVisibleAsync())
        {
            var t = await PageLocator.TextContentAsync();

            return t.Contains(PageTitle);
        }
        return false;
    }

    public override async Task VerifyPage() => await Assertions.Expect(PageLocator).ToContainTextAsync(PageTitle);

}
