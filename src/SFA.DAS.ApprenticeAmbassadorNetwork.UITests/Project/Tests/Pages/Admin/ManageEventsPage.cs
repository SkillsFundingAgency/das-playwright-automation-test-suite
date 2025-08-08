namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public class ManageEventsPage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage events");
    }

    private static string CancelEventLink(string id) => ($"a[href='/events/{id}/cancel']");

    public async Task<CancelEventPage> CancelEvent()
    {
        var eventId = context.Get<ObjectContext>().GetAanAdminEventId();

        await page.Locator(CancelEventLink(eventId)).ClickAsync();

        return await VerifyPageAsync(() => new CancelEventPage(context));

    }

    public async Task FilterEventBy(AanAdminCreateEventDatahelper data)
    {
        await FilterEventBy(data.EventStartDateAndTime, data.EventEndDateAndTime, data.EventType, data.EventRegion);
    }

    public async Task<EventFormatPage> CreateEvent()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Create event" }).ClickAsync();

        return await VerifyPageAsync(() => new EventFormatPage(context));
    }

    public new async Task FilterEventFromTomorrow()
    {
        await base.FilterEventFromTomorrow();
    }

    public new async Task FilterEventByOneMonth()
    {
        await base.FilterEventByOneMonth();
    }

    public new async Task FilterEventsByLocation(string location, int radius)
    {
        await base.FilterEventsByLocation(location, radius);
    }

    public new async Task FilterEventByEventStatus_Published()
    {
        await base.FilterEventByEventStatus_Published();
    }

    public new async Task FilterEventByEventStatus_Cancelled()
    {
        await base.FilterEventByEventStatus_Cancelled();
    }

    public new async Task FilterEventByEventType_TrainingEvent()
    {
        await base.FilterEventByEventType_TrainingEvent();
    }

    public new async Task FilterEventByEventRegion_London()
    {
        await base.FilterEventByEventRegion_London();
    }

    public new async Task ClearAllFilters()
    {
        await base.ClearAllFilters();
    }

    public new async Task VerifyEventStatus_Published_Filter()
    {
        await base.VerifyEventStatus_Published_Filter();
    }

    public new async Task VerifyEventStatus_Cancelled_Filter()
    {
        await base.VerifyEventStatus_Cancelled_Filter();
    }
    public new async Task VerifyEventType_TrainingEvent_Filter()
    {
        await base.VerifyEventType_TrainingEvent_Filter();
    }

    public new async Task VerifyEventRegion_London_Filter()
    {
        await base.VerifyEventRegion_London_Filter();
    }
}
