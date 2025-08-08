using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Helpers;

public class AanAdminStepsHelper(ScenarioContext context)
{
    private readonly SharedStepsHelper _sharedStepsHelper = new(context);

    protected SucessfullyPublisedEventPage sucessfullyPublisedEventPage;

    public async Task<CheckYourEventPage> CheckYourEvent(EventFormat eventFormat, bool guestSpeakers, bool isSchoolEvent, string pageTitle = null, string location = null)
    {
        EventOrganiserNamePage eventOrganiserNamePage;

        var page = new InPersonOrOnlinePage(context, await SubmitEventDate(eventFormat, guestSpeakers, pageTitle));

        if (eventFormat == EventFormat.Online)
        {
            eventOrganiserNamePage = await page.SubmitOnlineDetails();
        }
        else if (eventFormat == EventFormat.InPerson)
        {
            eventOrganiserNamePage = await GetEventOrgNamePage(await page.SubmitInPersonDetails(location), isSchoolEvent);
        }
        else
        {
            eventOrganiserNamePage = await GetEventOrgNamePage(await page.SubmitHybridDetails(location), isSchoolEvent);
        }

        var page1 = await eventOrganiserNamePage.SubmitOrganiserName();

        return await page1.SubmitEventAttendees();
    }


    public async Task<EventFormat> SubmitEventDate(EventFormat eventFormat, bool guestSpeakers, string pageTitle = null)
    {
        EventDatePage eventDatePage;

        var page = await new AdminAdministratorHubPage(context).AccessManageEvents();

        var page1 = await page.CreateEvent();

        var page2 = await page1.SubmitEventFormat(eventFormat);

        var page3 = await page2.SubmitEventTitle(pageTitle);

        var page4 = await page3.SubmitEventOutline();

        if (guestSpeakers) { var page5 = await page4.SubmitGuestSpeakerAsYes(); eventDatePage = await page5.AddAndDeleteGuestSpeakers(5); }

        else eventDatePage = await page4.SubmitGuestSpeakerAsNo();

        await eventDatePage.SubmitEventDate();

        return eventFormat;
    }

    public async Task<SucessfullyPublisedEventPage> SubmitEvent(EventFormat eventFormat, bool guestSpeakers, bool isSchoolEvent)
    {
        var page = await CheckYourEvent(eventFormat, guestSpeakers, isSchoolEvent);

        var page1 = await page.GoToEventPreviewPage(eventFormat);

        var page2 = await page1.GoToCheckYourEventPage();

        return sucessfullyPublisedEventPage = await page2.SubmitEvent();
    }

    public static async Task<EventOrganiserNamePage> GetEventOrgNamePage(IsEventAtSchoolPage isEventAtSchoolPage, bool isSchoolEvent)
    {
        if (isSchoolEvent) { var page = await isEventAtSchoolPage.SubmitIsEventAtSchoolAsYes(); return await page.SubmitSchoolName(); } 

        else return await isEventAtSchoolPage.SubmitIsEventAtSchoolAsNo();
    }

    public async Task<List<NetworkEventSearchResult>> GetAllSearchResults()
    {
        var manageEvents = new ManageEventsPage(context);

        return await _sharedStepsHelper.GetAllSearchResults(manageEvents);
    }

    public void ClearEventTitleCache()
    {
        _sharedStepsHelper.ClearSearchResultsCache();
    }
}
