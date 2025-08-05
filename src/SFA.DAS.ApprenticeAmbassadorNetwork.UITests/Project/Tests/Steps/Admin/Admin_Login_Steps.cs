using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Admin;

[Binding, Scope(Tag = "@aanadmin")]
public class Admin_Login_Steps(ScenarioContext context) : BaseSteps(context)
{
    [Given(@"an admin logs into the AAN portal")]
    [When(@"an admin logs into the AAN portal")]
    public async Task AnAdminLogsIntoTheAANPortal() => await SubmitValidLoginDetails(context.GetUser<AanAdminUser>());

    [Given(@"a super admin logs into the AAN portal")]
    public async Task ASuperAdminLogsIntoTheAANPortal() => await SubmitValidLoginDetails(context.GetUser<AanSuperAdminUser>());

    private async Task SubmitValidLoginDetails(DfeAdminUser user) => await new DfeSignInPage(context).SubmitValidLoginDetails(user);
}
