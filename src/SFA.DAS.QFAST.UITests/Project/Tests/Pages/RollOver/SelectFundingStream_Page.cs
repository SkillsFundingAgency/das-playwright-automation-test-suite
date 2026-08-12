namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;

public class SelectFundingStream_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Select funding stream(s)" })).ToBeVisibleAsync(); 
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ClickSelectAllButton() => await page.Locator("button:has-text('Select all')").ClickAsync();
    public async Task VerifySelectFundingStreamErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#funding_00000000-0000-0000-0000-000000000006']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "Select at least one funding stream.";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }
}
