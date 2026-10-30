namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class ConnectWithReceivingEmployerPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Connect with a receiving employer");

    public async Task<WhichEmployerAreYouConnectingWithPage> ContinueToConnectWithReceiver()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new WhichEmployerAreYouConnectingWithPage(context));
    }
}
