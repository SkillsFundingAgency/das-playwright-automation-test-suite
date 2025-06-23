using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_UsersPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Users");

    public async Task<AS_UserDetailsPage> ClickManageUserNameLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Mr Preprod Epao0007" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_UserDetailsPage(context));
    }

    public async Task<AS_UserDetailsPage> ClickPermissionsEditUserLink()
    {
        
        await page.GetByRole(AriaRole.Link, new() { Name = "Liz Kemp" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_UserDetailsPage(context));
    }

    //public async Task<AS_InviteUserPage> ClickInviteNewUserButton()
    //{
        
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Invite new user" }).ClickAsync();

    //    return await VerifyPageAsync(() => new AS_InviteUserPage(context));
    //}

    public async Task<AS_UserDetailsPage> ClickOnNewlyAddedUserLink(string userEmail)
    {
        await page.GetByRole(AriaRole.Cell, new() { Name = userEmail }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new AS_UserDetailsPage(context));
    }
}
