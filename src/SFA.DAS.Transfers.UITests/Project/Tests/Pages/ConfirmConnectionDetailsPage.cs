namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class ConfirmConnectionDetailsPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Confirm details");

    public async Task<RequestSentConfirmPage> SendTransferConnectionRequest()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, I want to send a request to connect", Exact = true }).CheckAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new RequestSentConfirmPage(context));
    }
}
