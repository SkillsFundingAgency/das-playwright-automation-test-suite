namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpapply")]
public class RoatpApplyHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    private readonly ScenarioContext _context = context;

    [BeforeScenario(Order = 32)]
    public void SetUpHelpers() => SetUpApplyDataHelpers();

    [BeforeScenario(Order = 33)]
    public new async Task GetRoatpAppplyData() => await base.GetRoatpAppplyData();

    [BeforeScenario(Order = 34)]
    public async Task ClearDownDataUkprnFromApply()
    {
        await ClearDownApplyData();
        await ClearDownDataUkprnFromApply(_objectContext.GetUkprn());
    }

    [BeforeScenario(Order = 35)]
    public async Task AllowListProviders() => await base.AllowListProviders();

    [BeforeScenario(Order = 36)]
    public async Task ClearDownTrainingProvider()
    {
        if (_context.ScenarioInfo.Tags.Contains("roatpapplye2e"))
        {
            await DeleteTrainingProvider();
        }
    }
}
