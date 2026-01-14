namespace SFA.DAS.AparAdmin.UITests.Project.Tests.Pages.AddJourney;

public class ProviderRoutePage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1"))
            .ToContainTextAsync("What provider route are they using?");
    }
    public async Task<string> GetHeadingTextAsync()
    {
        return await page.Locator("h1").InnerTextAsync();
    }
    public async Task SelectProviderType(string providerType)
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = providerType }).ClickAsync();
        await page.Locator("#continue").ClickAsync();

        await page.WaitForLoadStateAsync();
    }
}