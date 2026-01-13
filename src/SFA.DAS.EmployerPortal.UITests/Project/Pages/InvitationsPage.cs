namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class InvitationsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Invitations");

    public async Task<HomePage> ClickAcceptInviteLink()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Accept invite" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }
}
