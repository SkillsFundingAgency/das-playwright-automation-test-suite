using System.Threading.Tasks;

namespace SFA.DAS.AparAdmin.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpadmintestdataprep")]
public class RoatpAdminTestDataPrepHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpHelpers() => SetUpApplyDataHelpers();

    [BeforeScenario(Order = 33)]
    public new async Task GetNewRoatpAdminData() => await base.GetNewRoatpAdminData();

    [BeforeScenario(Order = 34)]
    public new async Task ClearDownApplyDataAndTrainingProvider() => await base.ClearDownApplyDataAndTrainingProvider();

    [BeforeScenario(Order = 35)]
    public async Task AllowListProviders() => await base.AllowListProviders();
}
