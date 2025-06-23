using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_EditUserPermissionsPage(ScenarioContext context) : EPAO_BasePage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Edit user permissions");

    public async Task IsChangeOrganisationDetailsCheckBoxSelected() => await Assertions.Expect(page.GetByRole(AriaRole.Checkbox, new() { Name = "Change organisation details" })).ToBeCheckedAsync();

    public async Task<AS_UserDetailsPage> ClickSaveButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_UserDetailsPage(context));
    }

    public async Task UnSelectChangeOrganisationDetailsCheckBox()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Change organisation details" }).UncheckAsync();
    }

    public async Task SelectAllPermissionCheckBoxes()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Change organisation details" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Pipeline" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Completed assessments" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Manage standards" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Manage users" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Record grades and issue" }).CheckAsync();
    }

    public async Task UnSelectAllPermissionCheckBoxes()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Change organisation details" }).UncheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Pipeline" }).UncheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Completed assessments" }).UncheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Manage standards" }).UncheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Manage users" }).UncheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Record grades and issue" }).UncheckAsync();
    }
}
