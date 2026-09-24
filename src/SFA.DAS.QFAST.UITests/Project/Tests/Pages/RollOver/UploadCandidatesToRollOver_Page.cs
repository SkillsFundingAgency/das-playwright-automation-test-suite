namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class UploadCandidatesToRollOver_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Upload candidates to rollover" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ValidateSelectCSVFileErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#fileUpload']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "You must select a CSV file.";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }    
}