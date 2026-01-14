namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class NotificationSettingsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Notification settings");
    }
}
