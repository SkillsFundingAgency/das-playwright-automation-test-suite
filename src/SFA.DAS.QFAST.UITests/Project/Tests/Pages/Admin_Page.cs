using SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;
public class Admin_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("QFAST homepage");
    public async Task AcceptCookieAndAlert()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept additional cookies" }).ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Hide cookie message" }).ClickAsync();
    }
    public async Task ValidateOptionsAsync(IEnumerable<string> expectedOptions)
    {
        if (expectedOptions is null || !expectedOptions.Any())
            throw new ArgumentException("No options available", nameof(expectedOptions));        
        var container = page.Locator("main");
        foreach (var optionText in expectedOptions)
        {
            var linkLocator = container.GetByRole(AriaRole.Link, new() { Name = optionText }).First;
            await Assertions.Expect(linkLocator).ToBeVisibleAsync();
        }
    }
    public async Task SelectOptions(string option)
    {
        var optionLocator = page.Locator($"a.govuk-link:has-text(\"{option}\")");
        await optionLocator.ClickAsync();        
    }
    public async Task<CheckDfeSignInPage> ClickLogOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();
        return await VerifyPageAsync(() => new CheckDfeSignInPage(context)); 
    }
}
