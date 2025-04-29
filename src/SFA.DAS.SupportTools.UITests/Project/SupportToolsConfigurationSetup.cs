using SFA.DAS.DfeAdmin.Service.Project.Helpers;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;

namespace SFA.DAS.SupportTools.UITests.Project;

[Binding]
public class SupportToolsConfigurationSetup(ScenarioContext context)
{
    private readonly ConfigSection _configSection = context.Get<ConfigSection>();

    [BeforeScenario(Order = 12)]
    public async Task SetUpSupportConsoleProjectConfiguration()
    {
        var dfeAdminUsers = context.Get<FrameworkList<DfeAdminUsers>>();

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new SupportToolScsUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new SupportToolScpUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new SupportToolTier1User()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new SupportToolTier2User())
        });

        await context.SetEasLoginUser(
        [
            _configSection.GetConfigSection<LevyUser>()
        ]);

        context.SetSupportConsoleConfig(_configSection.GetConfigSection<SupportToolsConfig>());
    }
}