namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpapplytestdataprep")]
public class RoatpApplyTestDataPrepHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpHelpers() => SetUpApplyDataHelpers();

    [BeforeScenario(Order = 33)]
    public new async Task GetRoatpApplyTestDataPrepData() => await base.GetRoatpApplyTestDataPrepData();

    [BeforeScenario(Order = 34)]
    public new async Task ClearDownApplyData() => await base.ClearDownApplyData();

    [BeforeScenario(Order = 35)]
    public async Task AllowListProviders() => await base.AllowListProviders();
}
