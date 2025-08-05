using FluentAssertions;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Models;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;
using TechTalk.SpecFlow.Assist;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;


[Binding, Scope(Tag = "@aanadmin")]
public class Admin_Events_Filter_Steps(ScenarioContext context) : BaseSteps(context)
{
    private ManageEventsPage manageEventsPage;

    [Then(@"the user should be able to successfully filter events by date")]
    public async Task TheUserShouldBeAbleToSuccessfullyFilterEventsByDate()
    {
        manageEventsPage = await new AdminAdministratorHubPage(context).AccessManageEvents();

        await manageEventsPage.FilterEventByOneMonth();

        await manageEventsPage.ClearAllFilters();
    }

    [Then(@"the user should be able to successfully filter events by event status")]
    public async Task ThenTheUserShouldBeAbleToSuccessfullyFilterEventsByEventStatus()
    {
        await manageEventsPage.FilterEventByEventStatus_Published();

        await manageEventsPage.VerifyEventStatus_Published_Filter();

        await manageEventsPage.ClearAllFilters();

        await manageEventsPage.FilterEventByEventStatus_Cancelled();

        await manageEventsPage.VerifyEventStatus_Cancelled_Filter();

        await manageEventsPage.ClearAllFilters();
    }

    [Then(@"the user should be able to successfully filter events by event type")]
    public async Task ThenTheUserShouldBeAbleTosuccessfullyFilterEventsByEventType()
    {
        await manageEventsPage.FilterEventByEventType_TrainingEvent();

        await manageEventsPage.VerifyEventType_TrainingEvent_Filter();

        await manageEventsPage.ClearAllFilters();
    }

    [Then(@"the user should be able to successfully filter events by regions")]
    public async Task ThenTheUserShouldBeAbleToSuccessfullyFilterEventsByRegions()
    {
        await manageEventsPage.FilterEventByEventRegion_London();

        await manageEventsPage.VerifyEventRegion_London_Filter();

        await manageEventsPage.ClearAllFilters();
    }

    [Then(@"the user should be able to successfully filter events by multiple combination of filters")]
    public async Task ThenTheUserShouldBeAbleTosuccessfullyFilterEventsByMultipleCombinationOfFilters()
    {
        await manageEventsPage.FilterEventByOneMonth();
        await manageEventsPage.FilterEventByEventStatus_Published();
        await manageEventsPage.FilterEventByEventType_TrainingEvent();
        await manageEventsPage.FilterEventByEventRegion_London();
        await manageEventsPage.VerifyEventStatus_Published_Filter();
        await manageEventsPage.VerifyEventType_TrainingEvent_Filter();
        await manageEventsPage.VerifyEventRegion_London_Filter();
        await manageEventsPage.ClearAllFilters();

    }

    [Given(@"the following events have been created:")]
    public async Task GivenTheFollowingEventsHaveBeenCreated(Table table)
    {
        var stepsHelper = context.Get<AanAdminStepsHelper>();

        var events = table.CreateSet<NetworkEventWithLocation>().ToList();

        foreach (var e in events)
        {
            var page = await stepsHelper.CheckYourEvent(EventFormat.InPerson, false, false, e.EventTitle, e.Location);

            var confirmationPage = await page.SubmitEvent();

            await confirmationPage.AccessHub();
        }
        ;
    }

    [When(@"the user filters events within (.*) miles of ""([^""]*)""")]
    public async Task WhenTheUserFiltersEventsWithinMilesOf(int radius, string location)
    {
        var hub = new AdminAdministratorHubPage(context);

        var manageEvents = await hub.AccessManageEvents();

        await manageEvents.FilterEventsByLocation(location, radius);

        var stepsHelper = context.Get<AanAdminStepsHelper>();

        stepsHelper.ClearEventTitleCache();
    }

    [Then(@"the following events can be found within the search results:")]
    public async Task ThenTheFollowingEventsCanBeFoundWithinTheSearchResults(Table table)
    {
        var stepsHelper = context.Get<AanAdminStepsHelper>();

        var results = await stepsHelper.GetAllSearchResults();

        var titles = results.Select(x => x.EventTitle).ToList();

        var expectedEvents = table.CreateSet<NetworkEvent>().ToList();

        foreach (var expected in expectedEvents)
        {
            titles.Should().Contain(expected.EventTitle);
        }
    }

    [Then(@"the following events can not be found within the search results:")]
    public async Task ThenTheFollowingEventsCanNotBeFoundWithingTheSearchResults(Table table)
    {
        var stepsHelper = context.Get<AanAdminStepsHelper>();

        var results = await stepsHelper.GetAllSearchResults();

        var titles = results.Select(x => x.EventTitle).ToList();

        var unexpectedEvents = table.CreateSet<NetworkEvent>().ToList();

        foreach (var unexpected in unexpectedEvents)
        {
            titles.Should().NotContain(unexpected.EventTitle);
        }
    }

    [Then(@"the heading text ""([^""]*)"" is displayed")]
    public async Task ThenTheHeadingTextIsDisplayed(string expectedText)
    {
        manageEventsPage = new ManageEventsPage(context);

        await manageEventsPage.VerifyHeadingText(expectedText);
    }

    [Then(@"the text ""([^""]*)"" is displayed")]
    public async Task ThenTheTextIsDisplayed(string expectedText)
    {
        manageEventsPage = new ManageEventsPage(context);

        await manageEventsPage.VerifyBodyText(expectedText);
    }

    [When(@"the user navigates to Manage Events")]
    public async Task WhenTheUserNavigatesToManageEvents()
    {
        manageEventsPage = await new AdminAdministratorHubPage(context).AccessManageEvents();
    }

    [When(@"the user filters events by Cancelled status")]
    public async Task WhenTheFiltersEventsByCancelledEventType()
    {
        manageEventsPage = new ManageEventsPage(context);

        await manageEventsPage.FilterEventByEventStatus_Cancelled();
    }

    [When(@"the user filters events by Training event type")]
    public async Task WhenTheUserFiltersEventsByTrainingEventType()
    {
        manageEventsPage = new ManageEventsPage(context);

        await manageEventsPage.FilterEventByEventType_TrainingEvent();
    }
}