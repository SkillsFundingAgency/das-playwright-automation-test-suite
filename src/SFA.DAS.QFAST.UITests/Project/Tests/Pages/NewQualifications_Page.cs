using System.Text.RegularExpressions;
namespace SFA.DAS.QFAST.UITests.Project.Tests.Pages;
public class NewQualifications_Page(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "New Qualifications" })).ToBeVisibleAsync();    
    public async Task<NewQualifications_Page> VerifyQANnumberIsLink()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await page.Locator("table.govuk-table tbody tr").First.WaitForAsync();
        var firstLink = page.Locator("table.govuk-table tbody tr").First.Locator("td").Nth(1).Locator("a");
        if (!await firstLink.IsVisibleAsync())
        {
            throw new Exception("The first qualification is not a link.");
        }
        var Qan = await firstLink.GetAttributeAsync("href");
        if (string.IsNullOrWhiteSpace(Qan))
        {
            throw new Exception("The first qualification link has no valid href.");
        }
        return await VerifyPageAsync(() => new NewQualifications_Page(context));
    }
    public async Task VerifyClickingOnLinkOpensNewTab()
    {
        var firstQanLink = page.Locator("table.govuk-table tbody tr").First.Locator("td").Nth(1).Locator("a");
        var popupTask = page.WaitForPopupAsync();    
        await firstQanLink.ClickAsync();
        var popupPage = await popupTask;
        var URL = new Regex(@"^https://find-a-qualification.services.ofqual.gov.uk/qualifications/\d+$");
        await Assertions.Expect(popupPage).ToHaveURLAsync(URL);
    }
    public async Task ClickOnSelectAllOnThisPageLink() 
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Select all on this page" }).ClickAsync();
    }
    public async Task ClickOnApplyToThisSelectionButton() 
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Apply to this selection" }).ClickAsync();
    }
    public async Task VerifyAllCheckboxesAreSelected()
    {
        var checkboxes = page.Locator("table.govuk-table tbody input.govuk-checkboxes__input");
        int count = await checkboxes.CountAsync();
        if (count != 10)
            throw new Exception($"Expected 10 checkboxes but found {count}");
        for (int i = 0; i < count; i++)
        {
            bool isChecked = await checkboxes.Nth(i).IsCheckedAsync();
            if (!isChecked)
                throw new Exception($"Checkbox at Line {i} is NOT selected");
        }
    }
    public async Task VefiryErrorMessage() 
    {
        var selectonequalfication = page.Locator(".govuk-error-summary__list a:has-text(\"Select at least 1 qualification before applying actions\")");
        var selectstatus = page.Locator(".govuk-error-summary__list a:has-text(\"Select a status\")");
        await Assertions.Expect(selectonequalfication).ToBeVisibleAsync();
        await Assertions.Expect(selectstatus).ToBeVisibleAsync();
    }
    public async Task ClickOnSelectStatusDropdown()
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Choose a status" }).ClickAsync();
    }
    public async Task SelectOption(string option) 
    {
        await page.Locator("#BulkAction_ProcessStatusId").SelectOptionAsync(option);
    }
    public async Task VerifySuccessMessage(string message)
    {
        var successMessage = page.Locator(".govuk-notification-banner__heading");
        await Assertions.Expect(successMessage).ToHaveTextAsync(message);
    }
    public async Task ChangeTheQualificationStatusManually(string option)
    {
        var firstRow = page.Locator("table.govuk-table tbody tr").First;       
        await firstRow.Locator("input[type='checkbox']").ClickAsync();
        await SelectOption(option);
        await ClickOnApplyToThisSelectionButton();       
    }    
}

