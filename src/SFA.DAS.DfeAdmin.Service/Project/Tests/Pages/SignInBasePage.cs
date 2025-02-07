namespace SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

public abstract class SignInBasePage(ScenarioContext context) : BasePage(context)
{
    public async Task EnterValidLoginDetails(string username, string password)
    {
        await page.GetByLabel("Email address").FillAsync(username);

        await page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Enter your password");

        await page.GetByLabel("Password", new() { Exact = true }).FillAsync(password);

        objectContext.SetDebugInformation($"Entered {username}, {password}");
    }

    protected virtual async Task ClickSignInButton() => await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
}
