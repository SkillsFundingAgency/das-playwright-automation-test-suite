namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class TransferRequestRejectedPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Transfer request rejected");
}
