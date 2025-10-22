namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public class AddOrSearchProvidersPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Staff dashboard");
    }
    public async Task<SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddOrSearchProvidersPage> ClickAddOrSearchForProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add or search for a provider" }).ClickAsync();

        return await VerifyPageAsync(() => new SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddOrSearchProvidersPage(context));
    }
}
