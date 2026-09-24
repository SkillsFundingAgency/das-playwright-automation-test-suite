namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class HowDoYouWantToSelectCandidates_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "How do you want to select candidates for rollover?" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task ValidateSelectCandidateErrorMessage()
    {
        var errorMessageLocator = page.Locator("a[href='#SelectedOption']");
        var actualErrorMessage = (await errorMessageLocator.InnerTextAsync()).Trim();
        var expectedErrorMessage = "You must select an option";
        if (!string.Equals(actualErrorMessage, expectedErrorMessage, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Expected error message to be '{expectedErrorMessage}', " +
                $"but the actual message is '{actualErrorMessage}'.");
        }
    }

    public async Task SelectWithImportList()
    {
        const string stageValue = "ImportAList";
        var stageLocator = page.Locator($"input[type='radio'][value='{stageValue}']");
        if (!await stageLocator.IsVisibleAsync())
        {
            throw new Exception(
                $"The rollover stage '{stageValue}' is not available on the page.");
        }
        await stageLocator.CheckAsync();
        await ClickContinueButton();

    }
    public async Task<SelectLevel_Page> SelectWithQuerybuilder()
    {
        const string stageValue = "GenerateAList";
        var stageLocator = page.Locator($"input[type='radio'][value='{stageValue}']");
        if (!await stageLocator.IsVisibleAsync())
        {
            throw new Exception(
                $"The rollover stage '{stageValue}' is not available on the page.");
        }
        await stageLocator.CheckAsync();
        await ClickContinueButton();
        return await VerifyPageAsync(() => new SelectLevel_Page(context));
    }

}
