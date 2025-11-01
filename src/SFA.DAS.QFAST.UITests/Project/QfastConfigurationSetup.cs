using SFA.DAS.Login.Service.Project;

namespace SFA.DAS.QFAST.UITests.Project;

[Binding]
public class QfastConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 12)]
    public async Task SetUpQfastConfiguration()
    {
        var dfeAdminUsers = context.Get<FrameworkList<DfeAdminUsers>>();

        context.SetNonEasLoginUser(new List<NonEasAccountUser>
        {
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastDfeAdminUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastAOUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastIFATEUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastOFQUALUser()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastDataImporter()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastReviewer()),
            SetDfeAdminCredsHelper.SetDfeAdminCreds(dfeAdminUsers, new QfastFormEditor()),
        });

        await Task.CompletedTask;
    }
}