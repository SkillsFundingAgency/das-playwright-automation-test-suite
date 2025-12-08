using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.Finance.APITests.Project.Hooks;

[Binding]
public class BeforeScenarioHooks(ScenarioContext context)
{

    [BeforeScenario(Order = 45)]
    public async Task SetUpHelpers()
    {
        var dbConfig = context.Get<DbConfig>();

        var objectContext = context.Get<ObjectContext>();
        
        context.SetRestClient(new Inner_EmployerFinanceApiRestClient(objectContext, context.Get<Inner_ApiFrameworkConfig>()));

        var employerFinanceSqlDbHelper = new EmployerFinanceSqlHelper(context.Get<ObjectContext>(), dbConfig);

        var employerAccountsSqlDbHelper = new EmployerAccountsSqlHelper(context.Get<ObjectContext>(), dbConfig);

        var accountid = await employerFinanceSqlDbHelper.SetAccountId();

        await employerAccountsSqlDbHelper.SetHashedAccountId(accountid);

        await employerFinanceSqlDbHelper.SetEmpRef();
    }
}