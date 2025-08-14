

namespace SFA.DAS.Apar.UITests.Project;

[Binding]
public class RoatpConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 2)]
    public async Task SetUpRoatpConfigConfiguration()
    {
        var dfeAdminUsers = context.Get<FrameworkList<DfeAdminUsers>>();

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new AsAdminUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new AsAssessor1User()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new AsAssessor2User())
        });

        await Task.CompletedTask;
    }
}
