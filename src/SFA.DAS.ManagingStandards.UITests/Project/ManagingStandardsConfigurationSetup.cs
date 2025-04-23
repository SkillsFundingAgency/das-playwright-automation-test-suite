

namespace SFA.DAS.ManagingStandards.UITests.Project;

[Binding]
public class ManagingStandardsConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 12)]
    public void SetUpManagingStandardsProjectConfiguration() => context.SetNonEasLoginUser(SetDfeAdminCredsHelper.SetDfeAdminCreds(context.Get<FrameworkList<DfeAdminUsers>>(), new AsAdminUser()));
}