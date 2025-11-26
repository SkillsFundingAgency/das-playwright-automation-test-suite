namespace SFA.DAS.Approvals.UITests.Project.Pages;

public abstract class ApprovalsBasePage(ScenarioContext context) : BasePage(context)
{
    public async Task NavToHomePage() => await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

    public async Task NavigateBrowserBack() => await page.GoBackAsync();

    internal async Task ClickOnLink(string linkText) => await page.GetByRole(AriaRole.Link, new() { Name = $"{linkText}", Exact = true }).ClickAsync();

    internal async Task ClickOnButton(string linkText) => await page.GetByRole(AriaRole.Button, new() { Name = $"{linkText}" }).ClickAsync();

}
