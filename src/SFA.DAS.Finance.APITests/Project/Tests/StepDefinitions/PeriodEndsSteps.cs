using System.Net;
using RestSharp;
using TechTalk.SpecFlow;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Finance.APITests.Project.Helpers;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class PeriodEndsSteps
    {
        private readonly Inner_EmployerFinanceApiRestClient _innerApiRestClient;
        private readonly ObjectContext _objectContext;

        public PeriodEndsSteps(ScenarioContext context)
        {
            _innerApiRestClient = context.GetRestClient<Inner_EmployerFinanceApiRestClient>();
            _objectContext = context.Get<ObjectContext>();
        }

        [Then(@"endpoint api/period-ends can be accessed")]
        public void ThenEndpointApiPeriodEndsCanBeAccessed()
        {
            _innerApiRestClient.ExecuteEndpoint("/api/period-ends", HttpStatusCode.OK);
        }
    }
}
