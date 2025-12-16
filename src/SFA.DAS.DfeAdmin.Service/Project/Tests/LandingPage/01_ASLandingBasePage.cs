using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class CheckDfeSignInPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => DfeSignInPage.DfePageTitle;

    protected override ILocator PageLocator => new DfeSignInPage(context).DfePageIdentifier;
}

public abstract class ASLandingCheckBasePage(ScenarioContext context) : CheckPage(context)
{
    protected override ILocator PageLocator => page.Locator("h1");

    public abstract Task ClickStartNowButton();
}

public class ASVacancyQaLandingPage(ScenarioContext context) : ASLandingCheckBasePage(context)
{
    protected override string PageTitle => "Apprenticeship service vacancy QA";

    protected override int VerifyPageTimeOutinMs => 30000;

    public override async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}

