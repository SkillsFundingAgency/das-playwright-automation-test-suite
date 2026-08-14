namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages.RollOver;
public class ListOfCandidatesFound_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "List of candidates found" })).ToBeVisibleAsync();
    public async Task ClickContinueButton() => await page.Locator("button:has-text('Continue')").ClickAsync();
    public async Task SelectRemovePreviousIfDisplayed()
    {
        var previousCandidatesPage = page.GetByText(
            "We found a list of candidates for rollover you worked on previously."
        );

        if (await previousCandidatesPage.IsVisibleAsync())
        {
            await page.Locator("input[name='SelectedOption'][value='RemovePrevious']").CheckAsync();
        }
        await ClickContinueButton();
    }
}
