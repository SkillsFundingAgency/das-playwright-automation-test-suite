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
