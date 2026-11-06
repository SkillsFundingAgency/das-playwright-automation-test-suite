namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class ConnectionConfirmedPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Connection confirmed", Level = 2 })).ToBeVisibleAsync();

    public async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Go to the homepage", Exact = true }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new HomePage(context));
    }
}
