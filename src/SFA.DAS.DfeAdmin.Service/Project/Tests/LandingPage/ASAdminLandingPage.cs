namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class ASAdminLandingPage(ScenarioContext context) : ASLandingCheckBasePage(context)
{
    protected override string PageTitle => "Apprenticeship service bulk stop utility";

    public override async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}
