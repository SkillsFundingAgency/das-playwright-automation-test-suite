using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;

[Binding, Scope(Tag = "@aanadmin")]
public class Admin_ChangeEvent_Steps(ScenarioContext context) : AdminCreateEventBaseSteps(context)
{
    private CheckYourEventPage checkYourEventPage;

    [When(@"the user should be able to successfully enters all the details for InPerson event")]
    public async Task TheUserShouldBeAbleToSuccessfullyEntersAllTheDetailsForInPersonEvent()
    {
        checkYourEventPage = await CheckYourEvent(EventFormat.InPerson, false, false);
    }

    [When(@"the user should be able to successfully enters all the details for hybrid event")]
    public async Task TheUserShouldBeAbleToSuccessfullyEntersAllTheDetailsForHybridEvent()
    {
        checkYourEventPage = await CheckYourEvent(EventFormat.Hybrid, false, false);
    }

    [When(@"the user should be able to successfully enters all the details for an Online event")]
    public async Task TheUserShouldBeAbleToSuccessfullyEntersAllTheDetailsForAnOnlineEvent()
    {
        checkYourEventPage = await CheckYourEvent(EventFormat.Online, false, false);
    }

    [When(@"changes the event to a in person event")]
    public async Task ChangesTheEventToAInPersonEvent() => await ChangesTheEventTo(EventFormat.InPerson);

    [When(@"changes the event to a hybrid event")]
    public async Task ChangesTheEventToAHybridEvent() => await ChangesTheEventTo(EventFormat.Hybrid);

    [When(@"changes the event to an online event")]
    public async Task ChangesTheEventToAnOnlineEvent() => await ChangesTheEventTo(EventFormat.Online);

    [When(@"changes all the event details")]
    public async Task ChangesAllTheEventDetails()
    {
        var page = await checkYourEventPage.ChangeEventType();

        var page1 = await page.UpdateEventTitle();

        var page2 = await page1.ChangeEventDateandTime();

        var page3 = await page2.UpdateEventDate();

        var page4 = await page3.ChangeEventDescription();

        var page5 = await page4.UpdateEventOutline();

        var page6 = await page5.ChangeGuestSpeakers();

        var page7 = await page6.SubmitGuestSpeakerAsYes();

        var page8 = await page7.UpdateGuestSpeakers(3);

        var page9 = await page8.ChangeEventOrganiser();

        var page10 = await page9.UpdateOrganiserName();

        var page11 = await page10.ChangeEventSchool();

        var page12 = await page11.SubmitIsEventAtSchoolAsYes();

        var page13 = await page12.UpdateSchoolName();

        var page14 = await page13.ChangeEventAttendees();

        await page14.UpdateEventAttendees();
    }

    private async Task ChangesTheEventTo(EventFormat eventFormat)
    {
        var page = await checkYourEventPage.ChangeEventFormat();

        await page.ChangeEventFormat(eventFormat);

        var page1 = await new InPersonOrOnlinePage(context, eventFormat).ContinueToCheckYourEventPage(eventFormat);

        var page2 = await page1.GoToEventPreviewPage(eventFormat);

        var page3 = await page2.GoToCheckYourEventPage();

        sucessfullyPublisedEventPage = await page3.SubmitEvent();
    }
}