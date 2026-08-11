namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class RollOverFunding_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Rollover funding");
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ClickGoBackLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Back" }).ClickAsync();
    public async Task ValidateSelectStageErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectedProcess']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage ="You must select which stage of the rollover process you need to do.";
        if (!string.Equals(actualErrorMessage,expectedErrorMessage,StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }   
    public async Task<DoYouNeedToUpdateAnyData_Page> SelectInitialSelectionStage()
    {
        const string stageValue = "InitialSelection";
        var stageLocator = page.Locator($"input[type='radio'][value='{stageValue}']");
        if (!await stageLocator.IsVisibleAsync())
        {
            throw new Exception(
                $"The rollover stage '{stageValue}' is not available on the page.");
        }
        await stageLocator.CheckAsync();
        await ClickContinueButton();
        return await VerifyPageAsync(() => new DoYouNeedToUpdateAnyData_Page(context));
    }
    public async Task SelectFinalUploadStage()
    {
        const string stageValue = "FinalUpload";
        var stageLocator = page.Locator($"input[type='radio'][value='{stageValue}']");
        if (!await stageLocator.IsVisibleAsync())
        {
            throw new Exception(
                $"The rollover stage '{stageValue}' is not available on the page.");
        }
        await stageLocator.CheckAsync();
    }
    public async Task VerifyAllCheckboxesAreSelected()
    {
        var checkboxes = page.Locator("input.govuk-checkboxes__input");
        int count = await checkboxes.CountAsync();
        for (int i = 0; i < count; i++)
        {
            if (!await checkboxes.Nth(i).IsCheckedAsync())
            {
                throw new Exception(
                    $"Checkbox at position {i + 1} is NOT selected.");
            }
        }
    }
}