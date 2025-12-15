using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
namespace SFA.DAS.Approvals.UITests.Project.Pages;

public abstract class ApprovalsBasePage(ScenarioContext context) : BasePage(context)
{
    internal async Task NavToHomePage() => await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

    internal async Task NavigateBrowserBack() => await page.GoBackAsync();

    internal async Task ClickOnLink(string linkText) => await page.GetByRole(AriaRole.Link, new() { Name = $"{linkText}", Exact = true }).ClickAsync();

    internal async Task ClickOnButton(string linkText) => await page.GetByRole(AriaRole.Button, new() { Name = $"{linkText}" }).ClickAsync();

    internal async Task ClearCacheAndReload()
    {
        await page.Context.ClearCookiesAsync();
        await page.EvaluateAsync("() => { localStorage.clear(); sessionStorage.clear(); }");
        await page.ReloadAsync();
    }
}
