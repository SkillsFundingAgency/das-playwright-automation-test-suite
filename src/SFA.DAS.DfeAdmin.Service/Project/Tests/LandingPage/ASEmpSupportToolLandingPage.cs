namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class ASEmpSupportToolLandingPage(ScenarioContext context) : ASLandingCheckBasePage(context)
{
    protected override string PageTitle => "Apprenticeship service support tools";

    public override async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();
}
