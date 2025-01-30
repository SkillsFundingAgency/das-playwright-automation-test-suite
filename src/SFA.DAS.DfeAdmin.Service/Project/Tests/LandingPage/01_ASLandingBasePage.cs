using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;


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
}

public class CheckDfeSignInPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => DfeSignInPage.DfePageTitle;

    protected override ILocator PageLocator => new DfeSignInPage(context).DfePageIdentifier;

    public override async Task VerifyPage() => await new DfeSignInPage(context).VerifyPage();
}

public abstract class ASLandingBasePage(ScenarioContext context) : CheckPage(context)
{
    protected override ILocator PageLocator => page.Locator("h1");

    public abstract Task ClickStartNowButton();
}
