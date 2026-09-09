namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;

public class InitialChecksAndExportComplete_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Initial checks and export complete" })).ToBeVisibleAsync();
    public async Task ValidateProcessCompleteText()
    {
        var expectedText ="You have exported a CSV file to your device with a list of candidates to be checked before completing rollover.";
        var textLocator = page.GetByText(expectedText, new() { Exact = true });
        if (!await textLocator.IsVisibleAsync())
        {
            throw new Exception(
                $"Expected process complete text '{expectedText}' was not found.");
        }
    }
    public async Task ClickGoToQFASTHomePage()
    {
        var homePageLink = page.GetByRole(AriaRole.Link,new() { Name = "Go to the QFAST homepage" });
        await homePageLink.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await homePageLink.ClickAsync();
    }
}
