namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpfulle2e")]
public class RoatpFullE2EHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpHelpers() => SetUpApplyDataHelpers();

    [BeforeScenario(Order = 33)]
    public new async Task GetRoatpFullData() => await base.GetRoatpFullData();

    [BeforeScenario(Order = 34)]
    public new async Task ClearDownApplyDataAndTrainingProvider() => await base.ClearDownApplyDataAndTrainingProvider();

    [BeforeScenario(Order = 35)]
    public async Task AllowListProviders() => await base.AllowListProviders();
}
