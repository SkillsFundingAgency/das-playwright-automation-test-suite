

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project;

[Binding]
public class AANConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 2)]
    public async Task SetUpAANConfigConfiguration()
    {
        var configSection = context.Get<ConfigSection>();

        await context.SetEasLoginUser(
        [
            configSection.GetConfigSection<AanEmployerUser>(),
            configSection.GetConfigSection<AanEmployerOnBoardedUser>()
        ]);

        var dfeAdminUsers = context.Get<FrameworkList<DfeAdminUsers>>();

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new AanAdminUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new AanSuperAdminUser())
        });

        await context.SetApprenticeAccountsPortalUser(
        [
           configSection.GetConfigSection<AanApprenticeUser>(),
           configSection.GetConfigSection<AanApprenticeNonBetaUser>(),
           configSection.GetConfigSection<AanApprenticeOnBoardedUser>(),
        ]);
    }
}