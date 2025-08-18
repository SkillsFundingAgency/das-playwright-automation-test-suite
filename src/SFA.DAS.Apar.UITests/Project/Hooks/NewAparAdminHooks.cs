using SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "newroatpadmin")]
public class NewAparAdminHooks : AparBaseHooks
{
    private readonly string[] _tags;
    private readonly AparApplySqlDbHelper _roatpApplyClearDownDataHelpers;

    public NewAparAdminHooks(ScenarioContext context) : base(context)
    {
        _tags = context.ScenarioInfo.Tags;
        _roatpApplyClearDownDataHelpers = new AparApplySqlDbHelper(_objectContext, _dbConfig);
    }

    [BeforeScenario(Order = 33)]
    public new async Task GetNewRoatpAdminData() { if (!_tags.Contains("newroatpadminreporting")) await base.GetNewRoatpAdminData(); }

    [BeforeScenario(Order = 35)]
    public async Task ClearDownGateWayAdminData()
    {
        if (_tags.Contains("resetApplicationToNew"))
            await _roatpApplyClearDownDataHelpers.GateWayClearDownDataFromApply(GetUkprn());
    }

    [BeforeScenario(Order = 36)]
    public async Task ClearDownFHAAdminData()
    {
        if (_tags.Contains("resetFhaApplicationToNew"))
            await _roatpApplyClearDownDataHelpers.FHAClearDownDataFromApply(GetUkprn());
    }

    [BeforeScenario(Order = 37)]
    public async Task ClearDownAssessorAdminData()
    {
        if (_tags.Contains("roatpassessoradmin"))
            await _roatpApplyClearDownDataHelpers.AssessorClearDownDataFromApply(GetUkprn());
    }

    [BeforeScenario(Order = 38)]
    public async Task ClearDownModeratorAdminData()
    {
        if (_tags.Contains("roatpmoderator"))
            await _roatpApplyClearDownDataHelpers.ModeratorClearDownDataFromApply(GetUkprn());
    }

    [BeforeScenario(Order = 39)]
    public async Task ClearDownClarificationAdminData()
    {
        if (_tags.Contains("roatpclarification"))
            await _roatpApplyClearDownDataHelpers.ClarificationClearDownFromApply(GetUkprn());
    }
    [BeforeScenario(Order = 40)]
    public async Task ClearDown_UKPRN_Allowlisttable()
    {
        if (_tags.Contains("rpallowlist"))
            await _roatpApplyClearDownDataHelpers.Delete_AllowListProviders(GetUkprn());
    }
}
