namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public abstract class ASLandingBasePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship service employer support tool");

    public async Task ClickStartNowButton() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}
