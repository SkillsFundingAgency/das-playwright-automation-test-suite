namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_UserInvitedPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("User invited");

    public async Task<AS_InviteUserPage> ClickInviteSomeoneElseLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Invite someone else" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_InviteUserPage(context));
    }
}
