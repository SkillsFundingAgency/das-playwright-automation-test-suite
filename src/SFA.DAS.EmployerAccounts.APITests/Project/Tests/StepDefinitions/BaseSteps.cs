using SFA.DAS.EmployerAccounts.APITests.Project.Helpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.HashingService;
namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected readonly Inner_EmployerAccountsApiRestClient innerApiRestClient;
        protected readonly Outer_EmployerAccountsApiHelper employerAccountsOuterApiHelper;
        protected readonly Inner_EmployerAccountsLegacyApiRestClient innerApiLegacyRestClient;
        protected readonly EmployerAccountsSqlDbHelper employerAccountsSqlDbHelper;
        protected readonly ObjectContext objectContext;
        protected readonly IHashingService hashingService;

        public BaseSteps(ScenarioContext context)
        {
            innerApiRestClient = context.GetRestClient<Inner_EmployerAccountsApiRestClient>();
            employerAccountsOuterApiHelper = new Outer_EmployerAccountsApiHelper(context);
            innerApiLegacyRestClient = context.GetRestClient<Inner_EmployerAccountsLegacyApiRestClient>();
            employerAccountsSqlDbHelper = context.Get<EmployerAccountsSqlDbHelper>();
            objectContext = context.Get<ObjectContext>();
            hashingService = context.ContainsKey(typeof(global::SFA.DAS.HashingService.IHashingService).FullName) ? context.Get<global::SFA.DAS.HashingService.IHashingService>() : null;
        }
    }
}
