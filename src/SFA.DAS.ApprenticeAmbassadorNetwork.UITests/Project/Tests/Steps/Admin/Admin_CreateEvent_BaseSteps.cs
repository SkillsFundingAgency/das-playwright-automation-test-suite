using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;

public class AdminCreateEventBaseSteps(ScenarioContext context) : AanBaseSteps(context)
{
    protected SucessfullyPublisedEventPage sucessfullyPublisedEventPage;

    protected AanAdminCreateEventDatahelper aanAdminDatahelper = context.Get<AanAdminCreateEventDatahelper>();
    protected AanAdminStepsHelper stepsHelper = context.Get<AanAdminStepsHelper>();

    protected async Task<string> AssertEventStatus(bool status)
    {
        var eventTitle = objectContext.GetAanEventTitle();

        var expected = status ? "True" : "False";

        var (id, isActive) = await context.Get<AANSqlHelper>().GetEventId(eventTitle);

        Assert.That(isActive, Is.EqualTo(expected), $"'{id}', '{eventTitle}' - event Active status is not set as '{expected}' - Actual : '{isActive}'");

        return id;
    }

    protected async Task<SucessfullyPublisedEventPage> SubmitInPersonEvent(bool guestSpeakers, bool isSchoolEvent) => await SubmitEvent(EventFormat.InPerson, guestSpeakers, isSchoolEvent);

    protected async Task<SucessfullyPublisedEventPage> SubmitOnlineEvent(bool guestSpeakers) => await SubmitEvent(EventFormat.Online, guestSpeakers, false);

    protected async Task<SucessfullyPublisedEventPage> SubmitHybridEvent(bool guestSpeakers, bool isSchoolEvent) => await SubmitEvent(EventFormat.Hybrid, guestSpeakers, isSchoolEvent);

    protected async Task<CheckYourEventPage> CheckYourEvent(EventFormat eventFormat, bool guestSpeakers, bool isSchoolEvent)
    {
        return await stepsHelper.CheckYourEvent(eventFormat, guestSpeakers, isSchoolEvent);
    }

    private async Task<SucessfullyPublisedEventPage> SubmitEvent(EventFormat eventFormat, bool guestSpeakers, bool isSchoolEvent)
    {
        var page = await stepsHelper.CheckYourEvent(eventFormat, guestSpeakers, isSchoolEvent);

        var page1 = await page.GoToEventPreviewPage(eventFormat);

        var page2 = await page1.GoToCheckYourEventPage();

        return sucessfullyPublisedEventPage = await page2.SubmitEvent();
    }
}
