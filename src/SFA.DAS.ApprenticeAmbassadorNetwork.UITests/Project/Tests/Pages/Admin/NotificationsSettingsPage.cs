namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public class NotificationsSettingsPage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Notification settings");
    }

    public async Task<ConfirmNotificationSuccess> SelectYesAndSave()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmNotificationSuccess(context));
    }

    public async Task<ConfirmNotificationSuccess> SelectNoAndSave()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "No" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();

        return await VerifyPageAsync(() => new ConfirmNotificationSuccess(context));
    }
}

public class ConfirmNotificationSuccess(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync("Notification settings saved");
    }
}