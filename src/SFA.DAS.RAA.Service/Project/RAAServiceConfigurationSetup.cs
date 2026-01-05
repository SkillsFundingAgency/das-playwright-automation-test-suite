using SFA.DAS.DfeAdmin.Service.Project.Helpers;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project;

[Binding]
public class RAAServiceConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 12)]
    public async Task SetUpRaaServiceConfigConfiguration()
    {
        var dfeAdminUsers = context.Get<FrameworkList<DfeAdminUsers>>();

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new VacancyQaUser()),
        });

        await Task.CompletedTask;
    }
}
