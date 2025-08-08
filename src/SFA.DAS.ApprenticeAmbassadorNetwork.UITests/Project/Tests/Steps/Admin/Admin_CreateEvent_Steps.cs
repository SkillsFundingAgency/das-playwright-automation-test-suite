namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;


[Binding, Scope(Tag = "@aanadmin")]
public class Admin_CreateEvent_Steps(ScenarioContext context) : AdminCreateEventBaseSteps(context)
{
    [When(@"the user should be able to successfully create hybrid school event")]
    public async Task TheUserShouldBeAbleToSuccessfullyCreateHybridSchoolEvent() => await SubmitHybridEvent(true, true);

    [When(@"the user should be able to successfully create hybrid event")]
    public async Task TheUserShouldBeAbleToSuccessfullyCreateHybridEvent() => await SubmitHybridEvent(false, false);

    [When(@"the user should be able to successfully create inperson event")]
    public async Task TheUserShouldBeAbleToSuccessfullyCreateInpersonEvent() => await SubmitInPersonEvent(true, false);

    [When(@"the user should be able to successfully create inperson school event")]
    public async Task TheUserShouldBeAbleToSuccessfullyCreateInpersonSchoolEvent() => await SubmitInPersonEvent(false, true);

    [When(@"the user should be able to successfully create online event")]
    public async Task TheUserShouldBeAbleToSuccessfullyCreateOnlineEvent() => await SubmitOnlineEvent(true);

    [Then(@"the user should be able to successfully cancel event")]
    public async Task TheUserShouldBeAbleToSuccessfullyCancelEvent()
    {
        var page = await sucessfullyPublisedEventPage.AccessManageEvents();

        await page.FilterEventBy(aanAdminDatahelper);

        var page1 = await page.CancelEvent();

        await page1.CancelEvent();
    }
}
