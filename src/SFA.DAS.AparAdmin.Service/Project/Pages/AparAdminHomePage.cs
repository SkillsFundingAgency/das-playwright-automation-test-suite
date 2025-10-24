

namespace SFA.DAS.AparAdmin.Service.Project.Pages;

public class AparAdminHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Staff dashboard");
    public async Task<ManageTrainingProviderInformationPage> ClickAddOrSearchForProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add or search for a provider" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageTrainingProviderInformationPage(context));
    }
}
