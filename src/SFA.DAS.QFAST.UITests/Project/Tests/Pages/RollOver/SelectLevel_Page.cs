namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class SelectLevel_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Select level(s)" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ClickSelectAllButton() => await page.Locator("button:has-text('Select all')").ClickAsync();

    public async Task VerifySelectLevelErrorMessage ()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectedLevels']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "Select the qualification levels you want to rollover";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }

}
