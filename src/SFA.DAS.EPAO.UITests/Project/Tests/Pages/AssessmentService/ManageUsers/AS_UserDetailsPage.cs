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

    public async Task IsViewDashboardPermissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("View dashboard");

    public async Task IsChangeOrganisationDetailsPersmissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("Change organisation details");

    public async Task IsPipelinePermissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("Pipeline");

    public async Task IsCompletedAssessmentsPermissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("Completed assessments");

    public async Task IsManageStandardsPermissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("Manage standards");

    public async Task IsManageUsersPermissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("Manage users");

    public async Task IsRecordGradesPermissionDisplayed() => await Assertions.Expect(page.Locator("dl")).ToContainTextAsync("Record grades and issue certificates");

    //public async Task<AS_RemoveUserPage> ClicRemoveThisUserLinkInUserDetailPage()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Remove this user" }).ClickAsync();

    //    return await VerifyPageAsync(() => new AS_RemoveUserPage(context));
    //}
}
