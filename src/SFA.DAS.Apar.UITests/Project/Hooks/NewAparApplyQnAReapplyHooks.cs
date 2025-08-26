using SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "rpadgatewayrejectreapplications01")]
public class NewAparApplyQnAReapplyHooks : AparBaseHooks
{
    private readonly AparApplySqlDbHelper _roatpApplyClearDownDataHelpers;

    private readonly TryCatchExceptionHelper _tryCatch;

    public NewAparApplyQnAReapplyHooks(ScenarioContext context) : base(context)
    {
        _tryCatch = context.Get<TryCatchExceptionHelper>();

        _roatpApplyClearDownDataHelpers = new AparApplySqlDbHelper(_objectContext, _dbConfig);
    }

    [BeforeScenario(Order = 33)]
    public async Task OversightReviewClearDownFromApply() => await _roatpApplyClearDownDataHelpers.OversightReviewClearDownFromApply_GatewayReject(GetUkprn());

    [AfterScenario(Order = 34)]
    public async Task ClearDownTrainingProviderFromRegister() => await _tryCatch.AfterScenarioException(DeleteTrainingProvider);

    [AfterScenario(Order = 35)]
    public async Task ClearDownReappliedTrainingProviderData_Apply()
    {
        await _tryCatch.AfterScenarioException(async () => await ClearDownApplyAndQnAData_ReappliedApplication(GetUkprn()));
    }
}
