//using FluentAssertions;
//using NUnit.Framework;
//using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
//using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;
//using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Employer;
//using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
//using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages.DfeSignPages;
//using SFA.DAS.Login.Service.Project;
//using SFA.DAS.Login.Service.Project.Helpers;
//using TechTalk.SpecFlow.Assist;

//namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Employer;

//[Binding, Scope(Tag = "@aanemployer")]
//public class Employer_Steps(ScenarioContext context) : Employer_BaseSteps(context)
//{
//    private EventsHubPage eventsHubPage;

//    private SearchNetworkEventsPage searchNetworkEventsPage;

//    private AanEmployerOnBoardedUser user;

//    [Given(@"an onboarded employer logs into the AAN portal")]
//    [When(@"an onboarded employer logs into the AAN portal")]
//    public async Task AnOnboardedEmployerLogsIntoTheAANPortal()
//    {
//        await EmployerSign(user = context.GetUser<AanEmployerOnBoardedUser>());

//        networkHubPage = new Employer_NetworkHubPage(context);

//        await networkHubPage.VerifyPage();
//    }

//    [Then(@"the user should be able to successfully verify a regional chair member profile")]
//    public async Task VerifyARegionalChairMemberProfile() => AccessNetworkDirectory(true);

//    [Then(@"the user should be able to successfully verify apprentice member profile")]
//    public async Task VerifyApprenticeMemberProfile() => AccessNetworkDirectory(false);

//    private void AccessNetworkDirectory(bool isRegionalChair) => AccessNetworkDirectory(networkHubPage, isRegionalChair, string.Empty);

//    [Then(@"the user should be able to (ask for industry advice|ask for help with a network activity|request a case study|get in touch after meeting at a network event) to a regional chair member successfully")]
//    public async Task TheUserShouldBeAbleToAskARegionalChairMemberSuccessfully(string message) => SendRegionalChairMessage(networkDirectoryPage, Apprentice, message);

//    [Then(@"the user should be able to (ask for industry advice|ask for help with a network activity|request a case study|get in touch after meeting at a network event) to an apprentice member successfully")]
//    public async Task TheUserShouldBeAbleToAskToTheMemberSuccessfully(string message) => SendApprenticeMessage(networkDirectoryPage, Apprentice, message);

//    [When(@"the user should be able to successfully verify ambassador profile")]
//    public async Task VerifyYourAmbassadorProfile() => VerifyYourAmbassadorProfile(networkHubPage, user.Username);

//    [Then(@"the user should be able to update profile information")]
//    public async Task ThenTheUserShouldBeAbleToUpdateProfileInformation() => new YourAmbassadorProfilePage(context).
//      AccessChangeForPersonalDetails()
//      .ChangePersonalDetailsAndContinue()
//      .AccessChangeForInterestInNetwork()
//      .SelectProjectManagementAndContinue()
//      .AccessChangeForContactDetails()
//      .ChangeLinkedlnUrlAndContinue();

//    [When(@"the user should be able to successfully hide ambassador profile information")]
//    public async Task WhenTheUserShouldBeAbleToSuccessfullyHideAmbassadorProfileInformation() => AccessYourAmbassadorProfile(networkHubPage)
//    .AccessChangeForPersonalDetails()
//    .HideJobtitleAndBiography()
//    .AccessChangeForApprenticeshipInformation()
//    .HideApprenticeshipInformation()
//    .AccessChangeForContactDetails()
//    .HideLinkedlnInformation();

//    [Then(@"the user should be able to successfully display ambassador profile information")]
//    public async Task ThenTheUserShouldBeAbleToSuccessfullyDisplayAmbassadorProfileInformation() => new YourAmbassadorProfilePage(context)
//        .AccessChangeForPersonalDetails()
//        .DisplayJobtitleAndBiography()
//        .AccessChangeForApprenticeshipInformation()
//        .DisplayApprenticeshipInformation()
//        .AccessChangeForContactDetails()
//        .DisplayLinkedlnInformation();

//    [Then(@"the user should be able to successfully signup for a future event")]
//    public async Task SignupForAFutureEvent() => eventsHubPage = SignupForAFutureEvent(networkHubPage, user.Username);

//    [Then(@"the user should be able to successfully Cancel the attendance for a signed up event")]
//    public async Task CancelTheAttendance() => CancelTheAttendance(eventsHubPage);

//    [Then(@"the user should be able to successfully filter events by date")]
//    public async Task FilterByDate() => searchNetworkEventsPage = FilterByDate(networkHubPage);

//    [Then(@"the user should be able to successfully filter events by event format")]
//    public async Task FilterByEventFormat() => FilterByEventFormat(searchNetworkEventsPage);

//    [Then(@"the user should be able to successfully filter events by event type")]
//    public async Task FilterByEventType() => FilterByEventType(searchNetworkEventsPage);

//    [Then(@"the user should be able to successfully filter events by regions")]
//    public async Task FilterByEventRegion() => FilterByEventRegion(searchNetworkEventsPage);

//    [Then(@"the user should be able to successfully filter events by multiple combination of filters")]
//    public async Task FilterByMultipleCombination() => FilterByMultipleCombination(searchNetworkEventsPage);

//    [Then(@"the user should be able to successfully filter events by role Network Directory")]
//    public async Task FilterByRole_NetworkDirectory() => networkDirectoryPage = FilterByEventRoleNetworkDirectory(networkHubPage);

//    [Then(@"the user should be able to successfully filter events by regions Network Directory")]
//    public async Task FilterByEventRegion_NetworkDirectory() => FilterByEventRegionNetworkDirectory(networkDirectoryPage);

//    [Then(@"the user should be able to successfully filter events by multiple combination of filters Network Directory")]
//    public async Task FilterByMultipleCombination_NetworkDirectory() => FilterByMultipleCombinationNetworkDirectory(networkDirectoryPage);

//    [Given(@"the following events have been created:")]
//    public async Task GivenTheFollowingEventsHaveBeenCreated(Table table)
//    {
//        var tabHelper = context.Get<TabHelper>();
//        tabHelper.GoToUrl(UrlConfig.AAN_Admin_BaseUrl);

//        var user = context.GetUser<AanAdminUser>();

//        new DfeSignInPage(context).SubmitValidLoginDetails(user);

//        var stepsHelper = context.Get<AanAdminStepsHelper>();

//        var events = table.CreateSet<NetworkEventWithLocation>().ToList();

//        foreach (var e in events)
//        {
//            var confirmationPage = stepsHelper
//                .CheckYourEvent(EventFormat.InPerson, false, false, e.EventTitle, e.Location).SubmitEvent();
//            confirmationPage.AccessHub();
//        }

//        tabHelper.GoToUrl(UrlConfig.AAN_Employer_BaseUrl);
//    }

//    [When(@"the user filters events within (.*) miles of ""([^""]*)""")]
//    public async Task WhenTheUserFiltersEventsWithinMilesOf(int radius, string location)
//    {
//        var searchNetworkEventsPage = networkHubPage.AccessEventsHub()
//            .AccessAllNetworkEvents();

//        searchNetworkEventsPage.EnterKeywordFilter("Location Filter Employer Test Event");

//        searchNetworkEventsPage.FilterEventsByLocation(location, radius);

//        var stepsHelper = context.Get<EmployerStepsHelper>();
//        stepsHelper.ClearEventTitleCache();
//    }

//    [When(@"the user filters events Across England centered on ""([^""]*)""")]
//    public async Task WhenTheUserFiltersEventsAcrossEnglandCenteredOn(string location)
//    {
//        var searchNetworkEventsPage = new SearchNetworkEventsPage(context);

//        searchNetworkEventsPage.EnterKeywordFilter("Location Filter Employer Test Event");
//        searchNetworkEventsPage.FilterEventsByLocation(location, 0);

//        var stepsHelper = context.Get<EmployerStepsHelper>();
//        stepsHelper.ClearEventTitleCache();
//    }


//    [Then(@"the following events can be found within the search results:")]
//    public async Task ThenTheFollowingEventsCanBeFoundWithinTheSearchResults(Table table)
//    {
//        var stepsHelper = context.Get<EmployerStepsHelper>();
//        var titles = stepsHelper.GetAllSearchResults().Select(x => x.EventTitle).ToList();

//        var expectedEvents = table.CreateSet<NetworkEvent>().ToList();

//        foreach (var expected in expectedEvents)
//        {
//            titles.Should().Contain(expected.EventTitle);
//        }
//    }

//    [Then(@"the following events can not be found within the search results:")]
//    public async Task ThenTheFollowingEventsCanNotBeFoundWithinTheSearchResults(Table table)
//    {
//        var stepsHelper = context.Get<EmployerStepsHelper>();
//        var titles = stepsHelper.GetAllSearchResults().Select(x => x.EventTitle).ToList();

//        var unexpectedEvents = table.CreateSet<NetworkEvent>().ToList();

//        foreach (var unexpected in unexpectedEvents)
//        {
//            titles.Should().NotContain(unexpected.EventTitle);
//        }
//    }

//    [When(@"the user orders the results by Closest")]
//    public async Task WhenTheUserOrdersTheResultsByClosest()
//    {
//        var searchNetworkEventsPage = new SearchNetworkEventsPage(context);
//        searchNetworkEventsPage.SelectOrderByClosest();
//    }

//    [Then(@"the following events can be found within the search results in the given order:")]
//    public async Task ThenTheFollowingEventsCanBeFoundWithinTheSearchResultsInTheGivenOrder(Table table)
//    {
//        var stepsHelper = context.Get<EmployerStepsHelper>();
//        var eventSearchResults = stepsHelper.GetAllSearchResults();
//        var expectedEvents = table.CreateSet<NetworkEventWithOrdinal>().OrderBy(e => e.Order).ToList();

//        var filteredSearchResults = eventSearchResults
//            .Where(x => expectedEvents.Select(y => y.EventTitle).Contains(x.EventTitle))
//            .OrderBy(x => x.Index)
//            .ToList();

//        for (var i = 0; i < expectedEvents.Count; i++)
//        {
//            var expectedEvent = expectedEvents[i];
//            var actualEvent = filteredSearchResults.ElementAtOrDefault(i);

//            Assert.NotNull(actualEvent, $"Expected event with index {i} was not found.");
//            Assert.AreEqual(expectedEvent.EventTitle, actualEvent.EventTitle, $"Event at index {i + 1} does not match the expected title.");
//        }
//    }

//    [Then(@"the heading text ""([^""]*)"" is displayed")]
//    public async Task ThenTheHeadingTextIsDisplayed(string expectedText)
//    {
//        searchNetworkEventsPage = new SearchNetworkEventsPage(context);
//        searchNetworkEventsPage.VerifyHeadingText(expectedText);
//    }

//    [Then(@"the text ""([^""]*)"" is displayed")]
//    public async Task ThenTheTextIsDisplayed(string expectedText)
//    {
//        searchNetworkEventsPage = new SearchNetworkEventsPage(context);
//        searchNetworkEventsPage.VerifyBodyText(expectedText);
//    }

//    [When(@"the user navigates to Network Events")]
//    public async Task WhenTheUserNavigatesToNetworkEvents()
//    {
//        searchNetworkEventsPage = networkHubPage.AccessEventsHub().AccessAllNetworkEvents();
//    }

//    [When(@"the user filters events by Cancelled status")]
//    public async Task WhenTheFiltersEventsByCancelledEventType()
//    {
//        searchNetworkEventsPage = new SearchNetworkEventsPage(context);
//        searchNetworkEventsPage.FilterEventByEventStatus_Cancelled();
//    }

//    [When(@"the user filters events by Training event type")]
//    public async Task WhenTheUserFiltersEventsByTrainingEventType()
//    {
//        searchNetworkEventsPage = new SearchNetworkEventsPage(context);
//        searchNetworkEventsPage.FilterEventByEventType_TrainingEvent();
//    }

//    [When(@"the user filters events so that there are no results")]
//    public async Task WhenTheUserFiltersEventsSoThatThereAreNoResults()
//    {
//        searchNetworkEventsPage = new SearchNetworkEventsPage(context);
//        searchNetworkEventsPage.FilterEventsWithNoResults();
//    }
//}
