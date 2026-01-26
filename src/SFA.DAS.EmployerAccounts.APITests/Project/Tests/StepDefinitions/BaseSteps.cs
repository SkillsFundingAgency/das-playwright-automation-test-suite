using SFA.DAS.API.Framework;
using SFA.DAS.EmployerAccounts.APITests.Project.Helpers;
using SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;
using System;
using SFA.DAS.HashingService;
namespace SFA.DAS.EmployerAccounts.APITests.Project.Tests.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected readonly Inner_EmployerAccountsApiRestClient _innerApiRestClient;
        protected readonly Outer_EmployerAccountsApiHelper _employerAccountsOuterApiHelper;
        protected readonly Inner_EmployerAccountsLegacyApiRestClient _innerApiLegacyRestClient;
        protected readonly EmployerAccountsSqlDbHelper _employerAccountsSqlDbHelper;
        protected readonly ObjectContext _objectContext;
        protected readonly IHashingService _hashingService;

        public BaseSteps(ScenarioContext context)
        {
            _innerApiRestClient = context.GetRestClient<Inner_EmployerAccountsApiRestClient>();
            _employerAccountsOuterApiHelper = new Outer_EmployerAccountsApiHelper(context);
            _innerApiLegacyRestClient = context.GetRestClient<Inner_EmployerAccountsLegacyApiRestClient>();
            _employerAccountsSqlDbHelper = context.Get<EmployerAccountsSqlDbHelper>();
            _objectContext = context.Get<ObjectContext>();
            _hashingService = context.ContainsKey(typeof(global::SFA.DAS.HashingService.IHashingService).FullName) ? context.Get<global::SFA.DAS.HashingService.IHashingService>() : null;
        }
    }
}
