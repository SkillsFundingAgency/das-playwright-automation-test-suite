namespace SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

public class ASEmpSupportToolLandingPage(ScenarioContext context) : ASLandingBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprenticeship service bulk stop utility");
}
