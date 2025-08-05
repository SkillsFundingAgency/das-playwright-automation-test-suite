namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin.CancelEvent;

public class CancelEventPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("form")).ToContainTextAsync($"Cancel event You are about to cancel '{aanAdminCreateEventDatahelper.EventTitle}'");
    }

    public async Task<SucessfullyCancelledEventPage> CancelEvent()
    {
        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Yes" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Cancel event" }).ClickAsync();

        return await VerifyPageAsync(() => new SucessfullyCancelledEventPage(context));
    }
}

public class SucessfullyCancelledEventPage(ScenarioContext context) : AdminNotificationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You have successfully cancelled a network event");
    }
}

public abstract class AdminNotificationBasePage(ScenarioContext context) : AanAdminBasePage(context)
{
    public async Task<ManageEventsPage> AccessManageEvents()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage events" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageEventsPage(context));
    }

    public async Task<AdminAdministratorHubPage> AccessHub()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage AAN service" }).ClickAsync();

        return await VerifyPageAsync(() => new AdminAdministratorHubPage(context));
    }
}