using SFA.DAS.EmployerAccounts.APITests.Project.Helpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Models;


namespace SFA.DAS.EmployerAccounts.APITests.Project.Hooks;

[Binding]
public class BeforeScenarioHooks(ScenarioContext context)
{
    private readonly ConfigSection configSection = context.Get<ConfigSection>();


    [BeforeScenario(Order = 2)]
    public void SetUpEmployerAccountsApiConfig() => context.SetEmployerAccountsApiConfig(configSection.GetConfigSection<EmployerAccountsApiConfig>());
    
    [BeforeScenario(Order = 45)]
    public async Task SetUpHelpers()
    {
        var config = context.GetEmployerAccountsApiConfig<EmployerAccountsApiConfig>();
        var hashingService = new HashingService.HashingService(config.HashCharacters, config.HashString);
        context.Set<global::SFA.DAS.HashingService.IHashingService>(hashingService);

        var dbConfig = context.Get<DbConfig>();
        var objectContext = context.Get<ObjectContext>();
    
        context.Set(new EmployerAccountsSqlDbHelper(context.Get<ObjectContext>(), dbConfig));
        context.SetRestClient(new Inner_EmployerAccountsApiRestClient(objectContext, context.Get<Inner_ApiFrameworkConfig>()));
        context.SetRestClient(new Inner_EmployerAccountsLegacyApiRestClient(objectContext, context.Get<Inner_ApiFrameworkConfig>()));
        var employerAccountsSqlDbHelper = new EmployerAccountsSqlDbHelper(context.Get<ObjectContext>(), dbConfig);

        await employerAccountsSqlDbHelper.SetUserRef();
        await employerAccountsSqlDbHelper.SetHashedAccountId();
        await employerAccountsSqlDbHelper.SetAccountId();
        await employerAccountsSqlDbHelper.SetLegalEntityId();
        await employerAccountsSqlDbHelper.SetPayeSchemeRef();
    }
}
