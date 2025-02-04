namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class ASEmpSupportToolLandingPage(ScenarioContext context) : ASLandingBasePage(context)
{
    protected override string PageTitle => "Apprenticeship service bulk stop utility";

    public override async Task VerifyPage() => await Assertions.Expect(PageLocator).ToContainTextAsync(PageTitle);

    public override async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();
}
