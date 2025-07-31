using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Apprentice;
using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using TechTalk.SpecFlow.Assist;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.StepDefinitions.Apprentice;

[Binding, Scope(Tag = "@aanaprentice")]
public class Apprentice_Steps(ScenarioContext context) : Apprentice_BaseSteps(context)
{
    private SearchNetworkEventsPage searchNetworkEventsPage;

    private EventsHubPage eventsHubPage;

    private AanApprenticeOnBoardedUser user;

    [Given(@"an onboarded apprentice logs into the AAN portal")]
    [When(@"an onboarded apprentice logs into the AAN portal")]
    public async Task AnOnboardedApprenticeLogsIntoTheAANPortal() => networkHubPage = await
        SubmitUserDetails_OnboardingJourneyComplete(user = context.Get<AanApprenticeOnBoardedUser>());

    [Then(@"the user should be able to successfully verify a regional chair member profile")]
    public async Task VerifyRegionalChairMemberProfile() => await AccessNetworkDirectory(true);

    [Then(@"the user should be able to successfully verify an apprentice member profile")]
    public async Task VerifyApprenticeMemberProfile() => await AccessNetworkDirectory(false);

    private async Task AccessNetworkDirectory(bool isRegionalChair) => await
        AccessNetworkDirectory(networkHubPage, isRegionalChair, user.Username);

    [Then(
        @"the user should be able to (ask for industry advice|ask for help with a network activity|request a case study|get in touch after meeting at a network event) to an apprentice member successfully")]
    public async Task TheUserShouldBeAbleToAskAnApprenticeMemberSuccessfully(string message) => await
        SendMessage(networkDirectoryPage, Apprentice, message);

    [Then(
        @"the user should be able to (ask for industry advice|ask for help with a network activity|request a case study|get in touch after meeting at a network event) to a regional chair member successfully")]
    public async Task TheUserShouldBeAbleToAskARegionalChairMemberSuccessfully(string message) => await
        SendMessage(networkDirectoryPage, Apprentice, message);

    [When(@"the user should be able to successfully verify ambassador profile")]
    public async Task VerifyYourAmbassadorProfile() => await VerifyYourAmbassadorProfile(networkHubPage, user.Username);

    [Then(@"the user should be able to update profile information")]
    public async Task ThenTheUserShouldBeAbleToUpdateProfileInformation()
    {
        var page = await new YourAmbassadorProfilePage(context).AccessChangeForPersonalDetails();

        var page1 = await page.ChangePersonalDetailsAndContinue();

        var page2 = await page1.AccessChangeForInterestInNetwork();

        var page3 = await page2.SelectProjectManagementAndContinue();

        var page4 = await page3.AccessChangeForApprenticeshipInformation();

        var page5 = await page4.ChangeSeconLineAddressAndContinue();

        var page6 = await page5.AccessChangeForContactDetails();

        await page6.ChangeLinkedlnUrlAndContinue();
    }

    [When(@"the user should be able to successfully hide ambassador profile information")]
    public async Task WhenTheUserShouldBeAbleToSuccessfullyHideAmbassadorProfileInformation()
    {
        var page = await AccessYourAmbassadorProfile(networkHubPage);

        var page1 = await page.AccessChangeForPersonalDetails();

        var page2 = await page1.HideJobtitleAndBiography();

        var page3 = await page2.AccessChangeForApprenticeshipInformation();

        var page4 = await page3.HideApprenticeshipInformation();

        var page5 = await page4.AccessChangeForContactDetails();

        await page5.HideLinkedlnInformation();
    }


    [Then(@"the user should be able to successfully display ambassador profile information")]
    public async Task ThenTheUserShouldBeAbleToSuccessfullyDisplayAmbassadorProfileInformation()
    {
        var page = await new YourAmbassadorProfilePage(context).AccessChangeForPersonalDetails();

        var page1 = await page.DisplayJobtitleAndBiography();

        var page2 = await page1.AccessChangeForApprenticeshipInformation();

        var page3 = await page2.DisplayApprenticeshipInformation();

        var page4 = await page3.AccessChangeForContactDetails();

        await page4.DisplayLinkedlnInformation();
    }


    [Then(@"the user should be able to successfully signup for a future event")]
    public async Task SignupForAFutureEvent() => eventsHubPage = await SignupForAFutureEvent(networkHubPage, user.Username);

    [Then(@"the user should be able to successfully Cancel the attendance for a signed up event")]
    public async Task CancelTheAttendance() => await CancelTheAttendance(eventsHubPage);

    [Then(@"the user should be able to successfully filter events by date")]
    public async Task FilterByDate() => await FilterByDate(networkHubPage);

    [Then(@"the user should be able to successfully filter events by event format")]
    public async Task FilterByEventFormat() => await FilterByEventFormat(new SearchNetworkEventsPage(context));

    [Then(@"the user should be able to successfully filter events by event type")]
    public async Task FilterByEventType() => await FilterByEventType(new SearchNetworkEventsPage(context));

    [Then(@"the user should be able to successfully filter events by multiple combination of filters")]
    public async Task FilterByMultipleCombination() => await FilterByMultipleCombination(new SearchNetworkEventsPage(context));

    [Then(@"the user should be able to successfully filter events by role Network Directory")]
    public async Task FilterByRole_NetworkDirectory() => await FilterByEventRoleNetworkDirectory(networkHubPage);

    [Then(@"the user should be able to successfully filter events by regions Network Directory")]
    public async Task FilterByEventRegion_NetworkDirectory() => await FilterByEventRegionNetworkDirectory(new NetworkDirectoryPage(context));

    [Then(@"the user should be able to successfully filter events by multiple combination of filters Network Directory")]
    public async Task FilterByMultipleCombination_NetworkDirectory() => await
        FilterByMultipleCombinationNetworkDirectory(new NetworkDirectoryPage(context));

    //[Given(@"the following events have been created:")]
    //public async Task GivenTheFollowingEventsHaveBeenCreated(Table table)
    //{
    //    await Navigate(UrlConfig.AAN_Admin_BaseUrl);

    //    var user = context.GetUser<AanAdminUser>();

    //    await new DfeSignInPage(context).SubmitValidLoginDetails(user);

    //    var stepsHelper = context.Get<AanAdminStepsHelper>();

    //    var events = table.CreateSet<NetworkEventWithLocation>().ToList();

    //    foreach (var e in events)
    //    {
    //        var confirmationPage = stepsHelper
    //            .CheckYourEvent(EventFormat.InPerson, false, false, e.EventTitle, e.Location).SubmitEvent();
    //        confirmationPage.AccessHub();
    //    }

    //    ;

    //    tabHelper.GoToUrl(UrlConfig.AAN_Apprentice_BaseUrl);
    //}

    [When(@"the user filters events within (.*) miles of ""([^""]*)""")]
    public async Task WhenTheUserFiltersEventsWithinMilesOf(int radius, string location)
    {
        var page = await networkHubPage.AccessEventsHub();

        var searchNetworkEventsPage = await page.AccessAllNetworkEvents();

        await searchNetworkEventsPage.EnterKeywordFilter("Location Filter Apprentice Test Event");

        await searchNetworkEventsPage.FilterEventsByLocation(location, radius);

        var stepsHelper = context.Get<ApprenticeStepsHelper>();

        stepsHelper.ClearEventTitleCache();
    }

    [When(@"the user filters events Across England centered on ""([^""]*)""")]
    public async Task WhenTheUserFiltersEventsAcrossEnglandCenteredOn(string location)
    {
        var searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.EnterKeywordFilter("Location Filter Apprentice Test Event");

        await searchNetworkEventsPage.FilterEventsByLocation(location, 0);

        var stepsHelper = context.Get<ApprenticeStepsHelper>();
        stepsHelper.ClearEventTitleCache();
    }


    [Then(@"the following events can be found within the search results:")]
    public async Task ThenTheFollowingEventsCanBeFoundWithinTheSearchResults(Table table)
    {
        var stepsHelper = context.Get<ApprenticeStepsHelper>();

        var page = await stepsHelper.GetAllSearchResults();

        var titles = page.Select(x => x.EventTitle).ToList();

        var expectedEvents = table.CreateSet<NetworkEvent>().ToList();

        foreach (var expected in expectedEvents)
        {
            titles.Should().Contain(expected.EventTitle);
        }
    }

    [Then(@"the following events can not be found within the search results:")]
    public async Task ThenTheFollowingEventsCanNotBeFoundWithinTheSearchResults(Table table)
    {
        var stepsHelper = context.Get<ApprenticeStepsHelper>();

        var page = await stepsHelper.GetAllSearchResults();

        var titles = page.Select(x => x.EventTitle).ToList();

        var unexpectedEvents = table.CreateSet<NetworkEvent>().ToList();

        foreach (var unexpected in unexpectedEvents)
        {
            titles.Should().NotContain(unexpected.EventTitle);
        }
    }

    [When(@"the user orders the results by Closest")]
    public async Task WhenTheUserOrdersTheResultsByClosest()
    {
        var searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.SelectOrderByClosest();
    }

    [Then(@"the following events can be found within the search results in the given order:")]
    public async Task ThenTheFollowingEventsCanBeFoundWithinTheSearchResultsInTheGivenOrder(Table table)
    {
        var stepsHelper = context.Get<ApprenticeStepsHelper>();

        var eventSearchResults = await stepsHelper.GetAllSearchResults();

        var expectedEvents = table.CreateSet<NetworkEventWithOrdinal>().OrderBy(e => e.Order).ToList();

        var filteredSearchResults = eventSearchResults
            .Where(x => expectedEvents.Select(y => y.EventTitle).Contains(x.EventTitle))
            .OrderBy(x => x.Index)
            .ToList();

        for (var i = 0; i < expectedEvents.Count; i++)
        {
            var expectedEvent = expectedEvents[i];
            var actualEvent = filteredSearchResults.ElementAtOrDefault(i);

            Assert.NotNull(actualEvent, $"Expected event with index {i} was not found.");
            Assert.AreEqual(expectedEvent.EventTitle, actualEvent.EventTitle, $"Event at index {i + 1} does not match the expected title.");
        }
    }

    [Then(@"the heading text ""([^""]*)"" is displayed")]
    public async Task ThenTheHeadingTextIsDisplayed(string expectedText)
    {
        searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.VerifyHeadingText(expectedText);
    }

    [Then(@"the text ""([^""]*)"" is displayed")]
    public async Task ThenTheTextIsDisplayed(string expectedText)
    {
        searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.VerifyBodyText(expectedText);
    }

    [When(@"the user navigates to Network Events")]
    public async Task WhenTheUserNavigatesToNetworkEvents()
    {
        var page = await networkHubPage.AccessEventsHub();

        searchNetworkEventsPage = await page.AccessAllNetworkEvents();
    }

    [When(@"the user filters events by Cancelled status")]
    public async Task WhenTheFiltersEventsByCancelledEventType()
    {
        searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.FilterEventByEventStatus_Cancelled();
    }

    [When(@"the user filters events by Training event type")]
    public async Task WhenTheUserFiltersEventsByTrainingEventType()
    {
        searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.FilterEventByEventType_TrainingEvent();
    }

    [When(@"the user filters events so that there are no results")]
    public async Task WhenTheUserFiltersEventsSoThatThereAreNoResults()
    {
        searchNetworkEventsPage = new SearchNetworkEventsPage(context);

        await searchNetworkEventsPage.FilterEventsWithNoResults();
    }
}