

namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;

public class RequestForFundign_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Requests for funding" })).ToBeVisibleAsync();
    public async Task ClickOnApplyActionsToThisSelections()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Apply actions to this selection" }).ClickAsync();
    }
    public async Task ClickOnApplyAssignReviewersToThisSelection()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Assign reviewers to this selection" }).ClickAsync();
    }
    public async Task VefiryErrorMessage()
    {
        var selectoneapplication = page.Locator(".govuk-error-summary__list a:has-text(\"Select at least 1 application before applying actions\")");
        var selectaction = page.Locator(".govuk-error-summary__list a:has-text(\"Select an action\")");
        await Assertions.Expect(selectoneapplication).ToBeVisibleAsync();
        await Assertions.Expect(selectaction).ToBeVisibleAsync();
    }
    public async Task VefiryErrorMessageForAssignReviewers()
    {
        var selectoneapplication = page.Locator(".govuk-error-summary__list a:has-text(\"Select at least 1 application before assigning reviewers\")");
        var selectaction = page.Locator(".govuk-error-summary__list a:has-text(\"Select a value for reviewer 1 or reviewer 2\")");
        await Assertions.Expect(selectoneapplication).ToBeVisibleAsync();
        await Assertions.Expect(selectaction).ToBeVisibleAsync();
    }
    public async Task SelectActionForApplications(string option)
    {
        var action = page.Locator("[id='BulkActionInputViewModel.BulkActionType']");
        await action.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        await action.SelectOptionAsync(option);
    }
    public async Task SelectReviewerForApplications(string reviewer1,string reviewer2)
    {
        var reviewer1dropdown = page.Locator("[id='BulkActionInputViewModel.Reviewer1']");
        await reviewer1dropdown.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        await reviewer1dropdown.SelectOptionAsync(reviewer1);
        var reviewer2dropdown = page.Locator("[id='BulkActionInputViewModel_Reviewer2']");
        await reviewer2dropdown.WaitForAsync(new() { State = WaitForSelectorState.Visible });
        await reviewer2dropdown.SelectOptionAsync(reviewer2);
    }
}
