namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;


public class EventFormatPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event Choose an event format");
    }

    public async Task<EventTitlePage> SubmitEventFormat(EventFormat eventFormat)
    {
        aanAdminCreateEventDatahelper.SetEventFormat(eventFormat);

        await SelectEventFormatAndContinue(aanAdminCreateEventDatahelper.EventFormat.eventFormat);

        return await VerifyPageAsync(() => new EventTitlePage(context));
    }

    public async Task ChangeEventFormat(EventFormat eventFormat)
    {
        aanAdminUpdateEventDatahelper.SetEventFormat(eventFormat);

        await SelectEventFormatAndContinue(aanAdminUpdateEventDatahelper.EventFormat.eventFormat);
    }

    private async Task SelectEventFormatAndContinue(string value) { await page.GetByRole(AriaRole.Radio, new() { Name = value }).CheckAsync(); await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync(); }
}

public class EventTitlePage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event Confirm your event details");
    }

    private static string EventTitleSelector => ("input#EventTitle");

    public async Task<EventOutlinePage> SubmitEventTitle(string pageTitle = null)
    {
        await EnterEventTitle(aanAdminCreateEventDatahelper, pageTitle);

        return await VerifyPageAsync(() => new EventOutlinePage(context));
    }

    public async Task<CheckYourEventPage> UpdateEventTitle()
    {
        await EnterEventTitle(aanAdminUpdateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task EnterEventTitle(AanAdminCreateEventBaseDatahelper dataHelper, string pageTitle = null)
    {
        var eventTitle = pageTitle ?? dataHelper.EventTitle;

        await page.Locator(EventTitleSelector).FillAsync(eventTitle);

        objectContext.SetAanEventTitle(eventTitle);

        var eventType = await SelectRandomOption("#EventTypeId");

        var eventRegion = await SelectRandomOption("#EventRegionId");

        dataHelper.SetEventTypeAndRegion(eventType, eventRegion);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }

    protected async Task<string> SelectRandomOption(string cssSelector)
    {
        string locator = ($"select{cssSelector}");

        var options = await page.Locator($"{locator} option").AllTextContentsAsync();

        var availableOption = options.ToList().Where(x => !x.StartsWith("Select")).ToList();

        var x = RandomDataGenerator.GetRandomElementFromListOfElements(availableOption);

        await page.Locator($"{locator}").SelectOptionAsync(x);

        return x;
    }
}

public class EventOutlinePage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("form")).ToContainTextAsync("Create event Provide more information about your event");
    }

    private static string EventOutlineText => ("textarea#EventOutline");

    private static string EventSummaryText => ("#EventSummary p");

    public async Task<IncludeGuestSpeakerPage> SubmitEventOutline()
    {
        await EnterEventOutline(aanAdminCreateEventDatahelper);

        return await VerifyPageAsync(() => new IncludeGuestSpeakerPage(context));
    }

    public async Task<CheckYourEventPage> UpdateEventOutline()
    {
        await EnterEventOutline(aanAdminUpdateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task EnterEventOutline(AanAdminCreateEventBaseDatahelper datahelper)
    {
        await page.Locator(EventOutlineText).FillAsync(datahelper.EventOutline);

        await page.Locator(EventSummaryText).FillAsync(datahelper.EventSummary);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class IncludeGuestSpeakerPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("legend")).ToContainTextAsync("Does the event include guest speakers?");
    }

    public async Task<GuestSpeakersPage> SubmitGuestSpeakerAsYes()
    {
        await EnterYesOrNoRadioOption("Yes");

        return await VerifyPageAsync(() => new GuestSpeakersPage(context));
    }

    public async Task<EventDatePage> SubmitGuestSpeakerAsNo()
    {
        await EnterYesOrNoRadioOption("No");

        return await VerifyPageAsync(() => new EventDatePage(context));
    }
}


public class GuestSpeakersPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event Confirm details of guest speaker");
    }

    public async Task<EventDatePage> AddAndDeleteGuestSpeakers(int add)
    {
        await AddGuestSpeakers(add);

        int delete = RandomDataGenerator.GenerateRandomNumberBetweenTwoValues(1, add);

        for (int i = 0; i < delete; i++) await page.GetByRole(AriaRole.Link, new() { Name = "Delete" }).First.ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EventDatePage(context));
    }

    public async Task<CheckYourEventPage> UpdateGuestSpeakers(int add)
    {
        await AddGuestSpeakers(add);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task AddGuestSpeakers(int add)
    {
        for (int i = 1; i <= add; i++)
        {
            await page.GetByRole(AriaRole.Link, new() { Name = "Add Speaker" }).ClickAsync();

            await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event Provide details of any guest speakers");

            await page.Locator("input#Name").FillAsync($"{i}_{AanAdminCreateEventBaseDatahelper.GuestSpeakerName}");

            await page.Locator("input#JobRoleAndOrganisation").FillAsync($"{i}_{AanAdminCreateEventBaseDatahelper.GuestSpeakerRole}");

            await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        }
    }
}

public class EventDatePage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event Confirm your event date and time");
    }

    private static string EventDateSelector => ("#dateOfEvent");
    private static string StartHourSelector => ("#StartHour");
    private static string StartMinutesSelector => ("#startMinutes");
    private static string EndHourSelector => ("#EndHour");
    private static string EndMinutesSelector => ("#EndMinutes");

    public async Task SubmitEventDate()
    {
        await EnterEventDate(aanAdminCreateEventDatahelper);
    }

    public async Task<CheckYourEventPage> UpdateEventDate()
    {
        await EnterEventDate(aanAdminUpdateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task EnterEventDate(AanAdminCreateEventBaseDatahelper datahelper)
    {
        var startDate = datahelper.EventStartDateAndTime;

        var endDate = datahelper.EventEndDateAndTime;

        await page.Locator(EventDateSelector).FillAsync($"{startDate.ToString(DateFormat)}");

        await page.Locator(StartHourSelector).FillAsync($"{startDate:HH}");

        await page.Locator(StartMinutesSelector).FillAsync($"{startDate:mm}");

        await page.Locator(EndHourSelector).FillAsync($"{endDate:HH}");

        await page.Locator(EndMinutesSelector).FillAsync($"{endDate:mm}");

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}