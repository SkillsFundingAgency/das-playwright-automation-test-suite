using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;

namespace SFA.DAS.DfeAdmin.Service.Project;

[Binding]
public class DfeAdminsConfigurationSetup(ScenarioContext context)
{
    private const string DfeAdminsConfig = "DfeAdminsConfig";

    [BeforeScenario(Order = 10)]
    public void SetUpDfeAdminsConfiguration()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpDfeAdminsConfiguration Order = 10 hook");

        new MultiConfigurationSetupHelper(context).SetMultiConfiguration<DfeAdminUsers>(DfeAdminsConfig);
    }
}
