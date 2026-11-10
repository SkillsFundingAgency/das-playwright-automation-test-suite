namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class TransferFundDetailsInErrorPage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => "Error";
    protected override ILocator PageHeader => page.Locator("xpath=/html/body/div[3]/h1");

    public override async Task VerifyPage()
    {
        await base.VerifyPage();
        await Assertions.Expect(page.Locator("xpath=/html/body/div[3]/h2"))
            .ToContainTextAsync("An error occurred while processing your request.");
    }
}
