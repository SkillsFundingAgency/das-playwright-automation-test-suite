using System.Security;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_UserDetailsPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("User details");

    private List<string> listOfPermissisons = [];

    public async Task<AS_EditUserPermissionsPage> ClickEditUserPermissionLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Edit user permissions" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_EditUserPermissionsPage(context));
    }

    public async Task<List<string>> GetDashboardPermissions()
    {
        var text = await page.Locator("dl").AllTextContentsAsync();

        listOfPermissisons = [.. text];

        objectContext.SetDebugInformation("Permissions available : ");

        objectContext.SetDebugInformation($"{listOfPermissisons.ToString(",")}");

        return listOfPermissisons;
    }

    public async Task<AS_RemoveUserPage> ClickRemoveThisUserLinkInUserDetailPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove this user" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_RemoveUserPage(context));
    }
}
