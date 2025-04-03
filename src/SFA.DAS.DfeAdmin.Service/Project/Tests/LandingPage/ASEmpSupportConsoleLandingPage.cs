namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class ASEmpSupportConsoleLandingPage(ScenarioContext context) : ASLandingCheckBasePage(context)
{
    protected override string PageTitle => "Apprenticeship service employer support tool";

    public override async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();

}