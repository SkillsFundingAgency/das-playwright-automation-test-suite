namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public class AdminAdministratorHubPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Administrator hub");
    }

    public async Task<ManageEventsPage> AccessManageEvents()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage events" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageEventsPage(context));
    }

    public async Task<ManageAmbassadorsPage> AccessManageAmbassadors()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage ambassadors" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageAmbassadorsPage(context));
    }

    public async Task<NotificationsSettingsPage> ManageNotifications()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Notification settings" }).ClickAsync();

        return await VerifyPageAsync(() => new NotificationsSettingsPage(context));
    }
}
