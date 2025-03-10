namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class InvitationsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Invitations");

    public async Task<HomePage> ClickAcceptInviteLink()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept invite" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
