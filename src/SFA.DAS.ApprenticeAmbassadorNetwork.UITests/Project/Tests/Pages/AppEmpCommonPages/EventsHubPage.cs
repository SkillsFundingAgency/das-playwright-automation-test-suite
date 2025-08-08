namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class EventsHubPage(ScenarioContext context) : AanBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Events hub");
    }

    private static string EventInCalendarLinkSelector => ("a.app-calendar__event");

    private static string EventMonth => (".app-calendar__row .govuk-heading-s");

    public async Task<SearchNetworkEventsPage> AccessAllNetworkEvents()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "All network events" }).ClickAsync();

        return await VerifyPageAsync(() => new SearchNetworkEventsPage(context));
    }

    public async Task GoToEventMonth(DateTime date)
    {
        static string ToEventMonthFormat(DateTime date) => date.ToString("MMMM yyyy");

        for (int i = 0; i < 12; i++)
        {
            var eventMonth = await page.Locator(EventMonth).TextContentAsync();

            if (eventMonth == ToEventMonthFormat(date))
            {
                break;
            }

            await page.GetByRole(AriaRole.Link, new() { Name = "Next month" }).ClickAsync();

            await Assertions.Expect(page.Locator(EventMonth)).ToContainTextAsync(ToEventMonthFormat(DateTime.Now.AddMonths(i + 1)));
        }
    }

    public async Task<EventPage> AccessFirstEventFromCalendar()
    {
        await page.Locator(EventInCalendarLinkSelector).First.ClickAsync();

        return await VerifyPageAsync(() => new EventPage(context));
    }

    public async Task<int> NoOfEventsFoundInCalender()
    {
        var x = await page.Locator(EventInCalendarLinkSelector).AllAsync();

        return x.Count;
    }
}