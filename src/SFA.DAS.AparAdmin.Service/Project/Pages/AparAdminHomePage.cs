using Microsoft.Playwright;

namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

public class AparAdminHomePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Staff dashboard");
    public async Task ClickAddOrSearchForProvider()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add or search for a provider" }).ClickAsync();
    }
    public async Task ClickDownloadProviderData()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = " Download training provider data}" }).ClickAsync();
    }
    public async Task ClickDownloadapplicationData()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = " Download application data}" }).ClickAsync();
    }
}
   
