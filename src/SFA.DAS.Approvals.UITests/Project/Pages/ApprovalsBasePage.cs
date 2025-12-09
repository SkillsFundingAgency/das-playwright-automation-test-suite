using SFA.DAS.Approvals.UITests.Project.Pages.Employer;

namespace SFA.DAS.Approvals.UITests.Project.Pages;

public abstract class ApprovalsBasePage(ScenarioContext context) : BasePage(context)
{
    internal async Task<EmployerHomePage> NavToHomePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();
        return await VerifyPageAsync(() => new EmployerHomePage(context));
    }

    public async Task NavigateBrowserBack() => await page.GoBackAsync();

    internal async Task ClickOnLink(string linkText) => await page.GetByRole(AriaRole.Link, new() { Name = $"{linkText}", Exact = true }).ClickAsync();

    internal async Task ClickOnButton(string linkText) => await page.GetByRole(AriaRole.Button, new() { Name = $"{linkText}" }).ClickAsync();

}
