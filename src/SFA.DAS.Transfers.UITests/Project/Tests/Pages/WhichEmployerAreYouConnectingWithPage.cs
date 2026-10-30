namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class WhichEmployerAreYouConnectingWithPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Which employer are you connecting with?");

    public async Task<ConfirmConnectionDetailsPage> ConnectWithReceivingEmployer(string receiverAccountId)
    {
        await page.Locator("#ReceiverAccountPublicHashedId").FillAsync(receiverAccountId);
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue", Exact = true }).ClickAsync();
        return await VerifyPageAsync(() => new ConfirmConnectionDetailsPage(context));
    }
}
