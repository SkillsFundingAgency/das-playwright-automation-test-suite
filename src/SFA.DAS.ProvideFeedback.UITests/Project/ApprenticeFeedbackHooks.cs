namespace SFA.DAS.ProvideFeedback.UITests.Project;

[Binding, Scope(Tag = "apprenticefeedback")]
public class ApprenticeFeedbackHooks(ScenarioContext context) : BaseHooks(context)
{
    private readonly DbConfig _dbConfig = context.Get<DbConfig>();

    [BeforeScenario(Order = 31)]
    public async Task NavigateToApprenticePortal()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered NavigateToApprenticePortal Order = 31 hook");

        await Navigate(UrlConfig.Apprentice_BaseUrl);
    }

    //[BeforeScenario(Order = 32)]
    //public void SetUpHelpers()
    //{
    //    _context.Set(new ApprenticeCommitmentsSqlDbHelper(_objectContext, _dbConfig));
    //    _context.Set(new ApprenticeCommitmentsAccountsSqlDbHelper(_objectContext, _dbConfig));
    //}
}
