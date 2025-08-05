namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

public class InPersonOrOnlinePage(ScenarioContext context, EventFormat eventFormat) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("form")).ToContainTextAsync(_pageTitle);
    }

    private readonly string _pageTitle = eventFormat == EventFormat.Online ? "Online event link" : "In person event location";

    private static string LinkInput => "input#onlineEventLink";

    public async Task<IsEventAtSchoolPage> SubmitInPersonDetails(string location = null)
    {
        await SubmitInPerson(location);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new IsEventAtSchoolPage(context));
    }

    public async Task<EventOrganiserNamePage> SubmitOnlineDetails()
    {
        await SubmitOnline();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new EventOrganiserNamePage(context));
    }


    public async Task<IsEventAtSchoolPage> SubmitHybridDetails(string location = null)
    {
        await SubmitInPerson(location);

        await SubmitOnline();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new IsEventAtSchoolPage(context));
    }

    public async Task<CheckYourEventPage> ContinueToCheckYourEventPage(EventFormat neweventFormat)
    {
        if (neweventFormat == EventFormat.InPerson)
        {
            await SubmitInPerson();
        }
        else if (neweventFormat == EventFormat.Online)
        {
            await SubmitOnline();
        }
        else
        {
            await SubmitInPerson();

            await SubmitOnline();
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task SubmitInPerson(string location = null)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            await SelectAutoDropDown(aanAdminCreateEventDatahelper.EventInPersonLocation);
        }
        else
        {
            await SelectAutoDropDown(location);
        }
    }

    private async Task SubmitOnline() => await page.Locator(LinkInput).FillAsync(aanAdminCreateEventDatahelper.EventOnlineLink);
}


public class IsEventAtSchoolPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("legend")).ToContainTextAsync("Is this event held at a school or place of learning?");
    }

    public async Task<NameOfTheSchoolPage> SubmitIsEventAtSchoolAsYes()
    {
        await EnterYesOrNoRadioOption("Yes");

        return await VerifyPageAsync(() => new NameOfTheSchoolPage(context));
    }

    public async Task<EventOrganiserNamePage> SubmitIsEventAtSchoolAsNo()
    {
        await EnterYesOrNoRadioOption("No");

        return await VerifyPageAsync(() => new EventOrganiserNamePage(context));
    }
}

public class NameOfTheSchoolPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("legend")).ToContainTextAsync("Name of the school");
    }

    public async Task<EventOrganiserNamePage> SubmitSchoolName()
    {
        await EnterSchoolName(aanAdminCreateEventDatahelper);

        return await VerifyPageAsync(() => new EventOrganiserNamePage(context));
    }

    public async Task<CheckYourEventPage> UpdateSchoolName()
    {
        await EnterSchoolName(aanAdminUpdateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task EnterSchoolName(AanAdminCreateEventBaseDatahelper datahelper)
    {
        await SelectAutoDropDown(datahelper.EventSchoolName);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class EventOrganiserNamePage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event Provide details of the event organiser");
    }

    private static string OrganiserName => "input#OrganiserName";

    private static string OrganiserEmail => "input#OrganiserEmail";

    public async Task<EventAttendeesPage> SubmitOrganiserName()
    {
        await EnterOrganiserName(aanAdminCreateEventDatahelper);

        return await VerifyPageAsync(() => new EventAttendeesPage(context));
    }

    public async Task<CheckYourEventPage> UpdateOrganiserName()
    {
        await EnterOrganiserName(aanAdminUpdateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task EnterOrganiserName(AanAdminCreateEventBaseDatahelper datahelper)
    {
        await page.Locator(OrganiserName).FillAsync(datahelper.EventOrganiserName);

        await page.Locator(OrganiserEmail).FillAsync(datahelper.EventOrganiserEmail);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}

public class EventAttendeesPage(ScenarioContext context) : AanAdminBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Heading)).ToContainTextAsync("Create event How many audience members do you expect at this event?");
    }

    private static string NumberOfAttendees => "input#NumberOfAttendees";

    public async Task<CheckYourEventPage> SubmitEventAttendees()
    {
        await EnterEventAttendees(aanAdminCreateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    public async Task<CheckYourEventPage> UpdateEventAttendees()
    {
        await EnterEventAttendees(aanAdminUpdateEventDatahelper);

        return await VerifyPageAsync(() => new CheckYourEventPage(context));
    }

    private async Task EnterEventAttendees(AanAdminCreateEventBaseDatahelper datahelper)
    {
        await page.Locator(NumberOfAttendees).FillAsync(datahelper.EventAttendeesNo);

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
    }
}