

namespace SFA.DAS.AparAdmin.Service.Project.Pages;

public class AparAdminHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Staff dashboard");
}
