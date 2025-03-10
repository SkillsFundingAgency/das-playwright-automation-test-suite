namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class NotificationSettingsPage(ScenarioContext context) : RegistrationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Notification settings");
    }
}
