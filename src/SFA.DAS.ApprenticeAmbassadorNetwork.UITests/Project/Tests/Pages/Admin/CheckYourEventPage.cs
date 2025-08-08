namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public class CheckYourEventPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Check your event before publishing");
    }

    private static string ChangeFormat => "a[href='/events/new/format']";

    private static string ChangeType => "a[href='/events/new/type']";

    private static string ChangeDateandTime => "a[href='/events/new/dateandtime']";

    private static string ChangeDescription => "a[href='/events/new/description']";

    private static string ChangeGuestSpeaker => "a[href='/events/new/guestspeakers/question']";

    private static string ChangeOrganiser => "a[href='/events/new/organiser']";

    private static string ChangeSchool => "a[href='/events/new/school/question']";

    private static string ChangeAttendees => "a[href='/events/new/attendees']";

    public async Task<SucessfullyPublisedEventPage> SubmitEvent()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new SucessfullyPublisedEventPage(context));
    }

    public async Task<EventPreviewPage> GoToEventPreviewPage(EventFormat eventFormat)
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "preview the event here" }).ClickAsync();

        return await VerifyPageAsync(() => new EventPreviewPage(context, eventFormat));
    }

    public async Task<EventFormatPage> ChangeEventFormat()
    {
        await page.Locator(ChangeFormat).ClickAsync();

        return await VerifyPageAsync(() => new EventFormatPage(context));
    }

    public async Task<EventTitlePage> ChangeEventType()
    {
        await page.Locator("form div").Filter(new() { HasText = "Type" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new EventTitlePage(context));
    }

    public async Task<EventDatePage> ChangeEventDateandTime()
    {
        await page.Locator(ChangeDateandTime).ClickAsync();

        return await VerifyPageAsync(() => new EventDatePage(context));
    }

    public async Task<EventOutlinePage> ChangeEventDescription()
    {
        await page.Locator("form div").Filter(new() { HasText = "Event outline" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new EventOutlinePage(context));
    }

    public async Task<IncludeGuestSpeakerPage> ChangeGuestSpeakers()
    {
        await page.Locator(ChangeGuestSpeaker).ClickAsync();

        return await VerifyPageAsync(() => new IncludeGuestSpeakerPage(context));
    }

    public async Task<EventOrganiserNamePage> ChangeEventOrganiser()
    {
        await page.Locator("form div").Filter(new() { HasText = "Organiser name" }).GetByRole(AriaRole.Link).ClickAsync();

        return await VerifyPageAsync(() => new EventOrganiserNamePage(context));
    }

    public async Task<IsEventAtSchoolPage> ChangeEventSchool()
    {
        await page.Locator(ChangeSchool).ClickAsync();

        return await VerifyPageAsync(() => new IsEventAtSchoolPage(context));
    }

    public async Task<EventAttendeesPage> ChangeEventAttendees()
    {
        await page.Locator(ChangeAttendees).ClickAsync();

        return await VerifyPageAsync(() => new EventAttendeesPage(context));
    }
}

public class EventPreviewPage(ScenarioContext context, EventFormat eventFormat) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Event preview");

        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(objectContext.GetAanEventTitle());

        await Assertions.Expect(page.Locator(EventTag).First).ToContainTextAsync(GetEventTag(eventFormat));
    }

    public async Task<CheckYourEventPage> GoToCheckYourEventPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "back to event page" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private static string GetEventTag(EventFormat eventFormat) => eventFormat == EventFormat.InPerson ? "In person" : eventFormat.ToString();
}

public class SucessfullyPublisedEventPage(ScenarioContext context) : AdminNotificationBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("You have successfully published a network event");
    }
}