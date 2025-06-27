using SFA.DAS.DfeAdmin.Service.Project.Helpers;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project;

[Binding]
public class EPAOConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 12)]
    public async Task SetUpEPAOProjectConfiguration()
    {
        var configSection = context.Get<ConfigSection>();

        await context.SetEPAOAssessorPortalUser(
        [
            configSection.GetConfigSection<EPAOStandardApplyUser>(),
            configSection.GetConfigSection<EPAOAssessorUser>(),
            configSection.GetConfigSection<EPAODeleteAssessorUser>(),
            configSection.GetConfigSection<EPAOManageUser>(),
            configSection.GetConfigSection<EPAOApplyUser>(),
            configSection.GetConfigSection<EPAOE2EApplyUser>(),
            configSection.GetConfigSection<EPAOWithdrawalUser>(),
            configSection.GetConfigSection<EPAOStageTwoStandardCancelUser>(),
        ]);

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(context.Get<FrameworkList<DfeAdminUsers>>(), new AsAdminUser())
        });
    }
}