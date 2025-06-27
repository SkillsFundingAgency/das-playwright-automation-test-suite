namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_InviteUserPage(ScenarioContext context) : EPAO_BasePage(context)
{

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Invite user");

    public async Task<string> EnterUserDetailsAndSendInvite()
    {
        var newUserEmailId = ePAOAssesmentServiceDataHelper.RandomEmail;

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Given name" }).FillAsync("Test Given Name");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Family name" }).FillAsync("Test Family Name");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email address" }).FillAsync(newUserEmailId);

        await SelectAllPermissionCheckBoxes();

        await page.GetByRole(AriaRole.Button, new() { Name = "Send Invite" }).ClickAsync();

        return newUserEmailId;
    }

    private async Task SelectAllPermissionCheckBoxes()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Change organisation details" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Pipeline" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Completed assessments" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Manage standards" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Manage users" }).CheckAsync();
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Record grades and issue" }).CheckAsync();
    }
}
