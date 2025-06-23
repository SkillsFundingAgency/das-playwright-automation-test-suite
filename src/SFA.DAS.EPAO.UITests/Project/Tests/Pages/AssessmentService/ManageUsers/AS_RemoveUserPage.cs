namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;

public class AS_RemoveUserPage(ScenarioContext context) : EPAO_BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Remove user");

    public async Task<AS_UserRemovedPage> ClickRemoveUserButtonInRemoveUserPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Remove user" }).ClickAsync();

        return await VerifyPageAsync(() => new AS_UserRemovedPage(context));
    }
}
