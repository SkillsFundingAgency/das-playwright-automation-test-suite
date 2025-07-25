using NUnit.Framework;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;
using System;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps;

public abstract class AppEmp_BaseSteps(ScenarioContext context) : BaseSteps(context)
{
    private (string id, DateTime startdate) Event;

    protected NetworkDirectoryPage networkDirectoryPage;

    protected (string id, string FullName) Apprentice;

    protected async Task AccessNetworkDirectory(NetworkHubPage networkHubPage, bool isRegionalChair, string email)
    {
        string x = isRegionalChair ? "is" : "is not";

        networkDirectoryPage = await networkHubPage.AccessNetworkDirectory();

        Apprentice = await _aanSqlHelper.GetLiveApprenticeDetails(isRegionalChair, email);

        Assert.That(!string.IsNullOrEmpty(Apprentice.id), $"No member found who '{x} regional chair' and email is not '{email}', use the sql query in test data attachment to debug the test");
    }

    protected static async Task<NetworkDirectoryPage> SendRegionalChairMessage(NetworkDirectoryPage networkDirectoryPage, (string id, string fullname) apprentice, string message)
    {
        return await SendMessage(networkDirectoryPage, true, apprentice, message);
    }

    protected static async Task<NetworkDirectoryPage> SendApprenticeMessage(NetworkDirectoryPage networkDirectoryPage, (string id, string fullname) apprentice, string message)
    {
        return await SendMessage(networkDirectoryPage, false, apprentice, message);
    }

    private static async Task<NetworkDirectoryPage> SendMessage(NetworkDirectoryPage networkDirectoryPage, bool isRegionalChair, (string id, string fullname) apprentice, string message)
    {
        var page = await networkDirectoryPage.GoToApprenticeMessagePage(isRegionalChair);

        var page1 = await page.GoToApprenticeMessagePage(apprentice);

        var page2 = await page1.SendMessage(message);

        return await page2.AccessNetworkDirectory();
    }

    protected static async Task VerifyYourAmbassadorProfile(NetworkHubPage networkHubPage, string value)
    {
        var page = await AccessYourAmbassadorProfile(networkHubPage);

        await page.VerifyYourAmbassadorProfile(value);
    }

    protected static async Task<YourAmbassadorProfilePage> AccessYourAmbassadorProfile(NetworkHubPage networkHubPage)
    {
        var page = await networkHubPage.AccessProfileSettings();

        return await page.AccessYourAmbassadorProfile();
    }

    protected static async Task<LeavingTheNetworkPage> AccessLeavingNetwork(NetworkHubPage networkHubPage)
    {
        var page = await networkHubPage.AccessProfileSettings();

        return await page.AccessLeaveTheNetwork();
    }

    protected static async Task UpdateAmbassadorProfile(YourAmbassadorProfilePage yourAmbassadorProfilePage)
    {
        await yourAmbassadorProfilePage.AccessChangeForPersonalDetails();
    }

    protected static async Task VerifyContactUs(NetworkHubPage networkHubPage)
    {
        await networkHubPage.AccessContactUs();
    }

    protected async Task<EventsHubPage> SignupForAFutureEvent(NetworkHubPage networkHubPage, string email)
    {
        var page = await networkHubPage.AccessEventsHub();

        Event = await _aanSqlHelper.GetNextActiveEventDetails(email);

        var page1 = await page.AccessAllNetworkEvents();

        var page2 = await page1.ClickOnLastEvent();

        await page2.GoToEvent(Event);

        var page4 = await page2.SignupForEvent();

        return await page4.AccessEventsHub();
    }

    protected async Task CancelTheAttendance(EventsHubPage eventsHubPage)
    {
        await eventsHubPage.GoToEventMonth(Event.startdate);

        var NoOfeventsFound = await eventsHubPage.NoOfEventsFoundInCalender();

        var page = await eventsHubPage.AccessFirstEventFromCalendar();

        await page.GoToEvent(Event);

        var page1 = await page.CancelYourAttendance();

        var page2 = await page1.AccessEventsHubFromCancelledAttendancePage();

        await page2.GoToEventMonth(Event.startdate);

        var actual = await page2.NoOfEventsFoundInCalender();

        Assert.That(actual, Is.EqualTo(NoOfeventsFound - 1));
    }

    protected static async Task FilterByDate(NetworkHubPage networkHubPage)
    {
        var page = await networkHubPage.AccessEventsHub();

        var page1 = await page.AccessAllNetworkEvents();

        await page1.FilterEventByOneMonth();

        await page1.ClearAllFilters();
    }

    protected static async Task FilterByEventFormat(SearchNetworkEventsPage searchNetworkEventsPage)
    {
        await searchNetworkEventsPage.FilterEventByEventFormat_InPerson();

        await searchNetworkEventsPage.VerifyEventFormat_Inperson_Filter();

        await searchNetworkEventsPage.ClearAllFilters();

        await searchNetworkEventsPage.FilterEventByEventFormat_Hybrid();

        await searchNetworkEventsPage.VerifyEventFormat_Hybrid_Filter();

        await searchNetworkEventsPage.ClearAllFilters();

        await searchNetworkEventsPage.FilterEventByEventFormat_Online();

        await searchNetworkEventsPage.VerifyEventFormat_Online_Filter();

        await searchNetworkEventsPage.ClearAllFilters();
    }

    protected static async Task FilterByEventType(SearchNetworkEventsPage searchNetworkEventsPage)
    {
        await searchNetworkEventsPage.FilterEventByEventType_TrainingEvent();

        await searchNetworkEventsPage.VerifyEventType_TrainingEvent_Filter();

        await searchNetworkEventsPage.ClearAllFilters();
    }

    protected static async Task FilterByEventRegion(SearchNetworkEventsPage searchNetworkEventsPage)
    {
        await searchNetworkEventsPage.FilterEventByEventRegion_London();

        await searchNetworkEventsPage.VerifyEventRegion_London_Filter();

        await searchNetworkEventsPage.ClearAllFilters();
    }

    protected static async Task FilterByMultipleCombination(SearchNetworkEventsPage searchNetworkEventsPage)
    {
        await searchNetworkEventsPage.FilterEventByOneMonth();

        await searchNetworkEventsPage.FilterEventByEventFormat_InPerson();

        await searchNetworkEventsPage.FilterEventByEventFormat_Hybrid();
        
        await searchNetworkEventsPage.FilterEventByEventFormat_Online();

        await searchNetworkEventsPage.FilterEventByEventType_TrainingEvent();

        await searchNetworkEventsPage.VerifyEventFormat_Inperson_Filter();

        await searchNetworkEventsPage.VerifyEventFormat_Hybrid_Filter();

        await searchNetworkEventsPage.VerifyEventFormat_Online_Filter();

        await searchNetworkEventsPage.VerifyEventType_TrainingEvent_Filter();

        await searchNetworkEventsPage.ClearAllFilters();
    }

    protected static async Task FilterByEventRegionNetworkDirectory(NetworkDirectoryPage networkDirectoryPage)
    {
        await networkDirectoryPage.FilterEventByEventRegion_London();

        await networkDirectoryPage.VerifyEventRegion_London_Filter();

        await networkDirectoryPage.ClearAllFilters();
    }

    protected static async Task FilterByEventRoleNetworkDirectory(NetworkHubPage networkHubPage)
    {
        var page = await networkHubPage.AccessNetworkDirectory();

        await page.FilterByRole_Apprentice();

        await page.VerifyRole_Apprentice_Filter();

        await page.ClearAllFilters();

        await page.FilterByRole_Employer();

        await page.VerifyRole_Employer_Filter();

        await page.ClearAllFilters();

        await page.FilterByRole_Regionalchair();

        await page.VerifyRole_Regionalchair_Filter();

        await page.ClearAllFilters();
    }
    protected static async Task FilterByMultipleCombinationNetworkDirectory(NetworkDirectoryPage networkDirectoryPage)
    {
        await networkDirectoryPage.FilterEventByEventRegion_London();

        await networkDirectoryPage.FilterByRole_Apprentice();

        await networkDirectoryPage.FilterByRole_Regionalchair();

        await networkDirectoryPage.FilterByRole_Employer();

        await networkDirectoryPage.VerifyEventRegion_London_Filter();

        await networkDirectoryPage.VerifyRole_Apprentice_Filter();

        await networkDirectoryPage.VerifyRole_Employer_Filter();

        await networkDirectoryPage.VerifyRole_Regionalchair_Filter();

        await networkDirectoryPage.ClearAllFilters();
    }
}

