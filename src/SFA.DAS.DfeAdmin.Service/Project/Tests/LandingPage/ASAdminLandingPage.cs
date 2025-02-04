namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class ASAdminLandingPage(ScenarioContext context) : ASLandingBasePage(context)
{
    protected override string PageTitle => "Apprenticeship service employer support tool";

    public override async Task VerifyPage() => await Assertions.Expect(PageLocator).ToContainTextAsync(PageTitle);

    public override async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}
