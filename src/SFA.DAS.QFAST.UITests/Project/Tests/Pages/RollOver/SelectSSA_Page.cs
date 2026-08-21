namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class SelectSSA_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Select the level of rollover you want to do" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ClickSelectAllButton() => await page.Locator("button:has-text('Select all')").ClickAsync();
    public async Task ClickSelectionOfSSAOption()
    {
        var ssaLocator = page.Locator("input[type='radio'][value='SpecificSelection']");
        if (!await ssaLocator.IsVisibleAsync())
        {
            throw new Exception(
                "The SSA option 'SpecificSelection' is not available on the page.");
        }
        await ssaLocator.CheckAsync();
    }
    public async Task VerifySelectAllSSAErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectionType']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "Select if you want to rollover all SSAs or only a selection";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }
    public async Task VerifySelectSSAErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectedSectorSubjectAreas']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "You must select at least one SSA";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }
}
