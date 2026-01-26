using SFA.DAS.EmployerAccounts.APITests.Project.Helpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlHelpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Models;
using SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.HashingService;


namespace SFA.DAS.EmployerAccounts.APITests.Project.Hooks;

[Binding]
public class BeforeScenarioHooks(ScenarioContext context)
{
    private readonly ConfigSection _configSection = context.Get<ConfigSection>();


    [BeforeScenario(Order = 2)]
    public void SetUpEmployerAccountsApiConfig() => context.SetEmployerAccountsApiConfig(_configSection.GetConfigSection<EmployerAccountsApiConfig>());
    
    [BeforeScenario(Order = 45)]
    public async Task SetUpHelpers()
    {
        var _config = context.GetEmployerAccountsApiConfig<EmployerAccountsApiConfig>();
        var hashingService = new HashingService.HashingService(_config.HashCharacters, _config.HashString);
        // store hashing service in ScenarioContext so step classes can retrieve it
        context.Set<global::SFA.DAS.HashingService.IHashingService>(hashingService);

        var dbConfig = context.Get<DbConfig>();

        var objectContext = context.Get<ObjectContext>();
        
        context.Set(new EmployerAccountsSqlDbHelper(context.Get<ObjectContext>(), dbConfig));

        context.SetRestClient(new Inner_EmployerAccountsApiRestClient(objectContext, context.Get<Inner_ApiFrameworkConfig>()));

        context.SetRestClient(new Inner_EmployerAccountsLegacyApiRestClient(objectContext, context.Get<Inner_ApiFrameworkConfig>()));

        var _employerAccountsSqlDbHelper = new EmployerAccountsSqlDbHelper(context.Get<ObjectContext>(), dbConfig);


        await _employerAccountsSqlDbHelper.SetHashedAccountId();
        await _employerAccountsSqlDbHelper.SetAccountId();
        await _employerAccountsSqlDbHelper.SetLegalEntityId();
        await _employerAccountsSqlDbHelper.SetPayeSchemeRef();

        _config = context.GetEmployerAccountsApiConfig<EmployerAccountsApiConfig>();
        // refresh hashing service in context in case config changed
        hashingService = new HashingService.HashingService(_config.HashCharacters, _config.HashString);
        context.Set<global::SFA.DAS.HashingService.IHashingService>(hashingService);

        await _employerAccountsSqlDbHelper.SetHashedAccountId();
        await _employerAccountsSqlDbHelper.SetAccountId();
        await _employerAccountsSqlDbHelper.SetLegalEntityId();
        await _employerAccountsSqlDbHelper.SetPayeSchemeRef();
    }
}
