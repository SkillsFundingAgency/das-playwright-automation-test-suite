namespace SFA.DAS.Transfers.UITests.Project.Tests.Pages;

public class TransferRequestApprovedPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() =>
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Transfer request approved");
}
