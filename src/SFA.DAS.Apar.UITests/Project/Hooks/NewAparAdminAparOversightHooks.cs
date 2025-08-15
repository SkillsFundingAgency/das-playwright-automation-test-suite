using SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "roatpoutcome")]
public class NewAparAdminAparOversightHooks : AparBaseHooks
{
    private readonly string[] _tags;
    private readonly AparApplySqlDbHelper _roatpApplyClearDownDataHelpers;

    public NewAparAdminAparOversightHooks(ScenarioContext context) : base(context)
    {
        _tags = context.ScenarioInfo.Tags;
        _roatpApplyClearDownDataHelpers = new AparApplySqlDbHelper(_objectContext, _dbConfig);
    }

    [BeforeScenario(Order = 33)]
    public async Task OversightReviewClearDownFromApply() => await _roatpApplyClearDownDataHelpers.OversightReviewClearDownFromApply(GetUkprn());

    [BeforeScenario(Order = 34)]
    public async Task ClearDownTrainingProviderFromRegister()
    {
        if (_tags.Contains("rpadoutcomeappeals01") ||
            _tags.Contains("rpadgatewayfailappeals01") ||
            _tags.Contains("rpadoutcome01"))
        {
            await DeleteTrainingProvider();
        }
    }
}
