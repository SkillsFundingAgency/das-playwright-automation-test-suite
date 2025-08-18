namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpapplyinprogressapplication")]
public class AparApplyInProgressHooks(ScenarioContext context) : AparBaseHooks(context)
{
    [BeforeScenario(Order = 33)]
    public new async Task GetRoatpFullData() => await base.GetRoatpFullData();
}
