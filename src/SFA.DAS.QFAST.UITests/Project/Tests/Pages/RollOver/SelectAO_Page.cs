namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class SelectAO_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Select awarding organisations (AO)" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ClickSelectAllButton() => await page.Locator("button:has-text('Select all')").ClickAsync();
    public async Task ClickSelectionOfAoOption()
    {
        var ssaLocator = page.Locator("input[type='radio'][value='SpecificSelection']");
        if (!await ssaLocator.IsVisibleAsync())
        {
            throw new Exception(
                "The SSA option 'SpecificSelection' is not available on the page.");
        }
        await ssaLocator.CheckAsync();
    }
    public async Task VerifySelectAllAOErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectionType']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "Select if you want to rollover all awarding organisations or only a selection";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }
    public async Task VerifySelectAOErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectedAwardingOrganisations']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "You must select at least one awarding organisation";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }

}
