namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;

[Binding, Scope(Tag = "@aanadmin")]
public class Admin_CreateEvent_DBSteps(ScenarioContext context) : AdminCreateEventBaseSteps(context)
{
    [Then(@"the system should confirm the event creation")]
    public async Task TheSystemShouldConfirmTheEventCreation() => objectContext.SetAanAdminEventId(await AssertEventStatus(true));

    [Then(@"the system should confirm the event cancellation")]
    public async Task TheSystemShouldConfirmTheEventCancellation() => await AssertEventStatus(false);

}