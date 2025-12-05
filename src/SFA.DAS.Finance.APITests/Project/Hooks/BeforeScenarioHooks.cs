using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.Finance.APITests.Project.Hooks;

[Binding]
public class BeforeScenarioHooks(ScenarioContext context)
{
    private readonly DbConfig _dbConfig = context.Get<DbConfig>();
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 45)]
    public void SetUpHelpers()
    {
        context.Set(new EmployerFinanceSqlHelper(context.Get<ObjectContext>(), _dbConfig));

        context.Set(new EmployerAccountsSqlHelper(context.Get<ObjectContext>(), _dbConfig));

        context.SetRestClient(new Inner_EmployerFinanceApiRestClient(_objectContext, context.Get<Inner_ApiFrameworkConfig>()));
    }
}