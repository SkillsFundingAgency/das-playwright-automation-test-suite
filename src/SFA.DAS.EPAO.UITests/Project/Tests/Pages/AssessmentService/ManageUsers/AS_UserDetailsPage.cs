using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_UserDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("User details");

    public async Task<AS_EditUserPermissionsPage> ClickEditUserPermissionLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Edit user permissions" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_EditUserPermissionsPage(context));
    }

    public async Task<bool> IsViewDashboardPermissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("View dashboard"); }

    public async Task<bool> IsChangeOrganisationDetailsPersmissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("Change organisation details"); }

    public async Task<bool> IsPipelinePermissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("Pipeline"); }

    public async Task<bool> IsCompletedAssessmentsPermissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("Completed assessments"); }

    public async Task<bool> IsManageStandardsPermissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("Manage standards"); }

    public async Task<bool> IsManageUsersPermissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("View dashboard"); }

    public async Task<bool> IsRecordGradesPermissionDisplayed() { var text = await page.Locator("dl").AllTextContentsAsync(); return text.Contains("Record grades and issue certificates"); }

    public async Task<AS_RemoveUserPage> ClickRemoveThisUserLinkInUserDetailPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove this user" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_RemoveUserPage(context));
    }
}
