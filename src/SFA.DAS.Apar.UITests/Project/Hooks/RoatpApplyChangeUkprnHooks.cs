namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpapplychangeukprn")]
public class RoatpApplyChangeUkprnHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpHelpers() => SetUpApplyDataHelpers();

    [BeforeScenario(Order = 33)]
    public new async Task GetRoatpChangeUkprnAppplyData() => await base.GetRoatpChangeUkprnAppplyData();

    [BeforeScenario(Order = 34)]
    public async Task ClearDownDataUkprnFromApply()
    {
        await ClearDownApplyData();
        await ClearDownDataUkprnFromApply(_objectContext.GetUkprn());
        await ClearDownDataUkprnFromApply(_objectContext.GetNewUkprn());
    }

    [BeforeScenario(Order = 35)]
    public async Task AllowListProviders()
    {
        await AllowListProviders(_objectContext.GetUkprn());
        await AllowListProviders(_objectContext.GetNewUkprn());
    }
}
