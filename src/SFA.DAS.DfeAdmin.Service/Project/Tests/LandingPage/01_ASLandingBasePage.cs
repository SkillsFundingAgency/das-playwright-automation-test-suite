using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class CheckDfeSignInPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => DfeSignInPage.DfePageTitle;

    protected override ILocator PageLocator => new DfeSignInPage(context).DfePageIdentifier;
}

public abstract class ASLandingBasePage(ScenarioContext context) : CheckPage(context)
{
    protected override ILocator PageLocator => page.Locator("h1");

    public abstract Task ClickStartNowButton();
}
