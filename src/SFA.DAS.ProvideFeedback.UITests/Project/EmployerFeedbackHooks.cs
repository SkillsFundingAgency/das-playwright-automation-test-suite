using SFA.DAS.ProvideFeedback.UITests.Project.Helpers;

namespace SFA.DAS.ProvideFeedback.UITests.Project;

[Binding, Scope(Tag = "employerfeedback")]
public class EmployerFeedbackHooks(ScenarioContext context) : BaseHooks(context)
{
    private EmployerFeedbackSqlHelper _employerFeedbackSqlHelper;

    [BeforeScenario(Order = 21)]
    public void SetUpHelpers()
    {
        _employerFeedbackSqlHelper = new EmployerFeedbackSqlHelper(_objectContext, _context.Get<DbConfig>());

        _context.Set(_employerFeedbackSqlHelper);
    }

    [AfterScenario(Order = 33)]
    public async Task ClearDownEmployerFeedbackResult() => await _tryCatch.AfterScenarioException(async () => await _employerFeedbackSqlHelper.ClearDownEmployerFeedbackResult(_objectContext.GetUniqueSurveyCode()));
}
