namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class RequestSentConfirmPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Request sent", Level = 2 })).ToBeVisibleAsync();

    public async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Go to the homepage", Exact = true }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new HomePage(context));
    }
}
