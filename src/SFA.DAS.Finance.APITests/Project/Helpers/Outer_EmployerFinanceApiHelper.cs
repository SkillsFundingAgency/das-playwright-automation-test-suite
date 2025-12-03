using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public class Outer_EmployerFinanceApiHelper
    {
        private readonly Outer_EmployerFinanceApiRestClient _outerEmployerFinanceApiRestClient;
        private readonly Outer_EmployerFinanceHealthApiRestClient _outerEmployerFinanceHealthApiRestClient;
        private readonly ObjectContext _objectContext;
        protected readonly FrameworkHelpers.RetryHelper _assertHelper;

        internal Outer_EmployerFinanceApiHelper(ScenarioContext context)
        {
            _objectContext = context.Get<ObjectContext>();
            _assertHelper = context.Get<FrameworkHelpers.RetryHelper>();
            _outerEmployerFinanceApiRestClient = new Outer_EmployerFinanceApiRestClient(_objectContext, context.GetOuter_ApiAuthTokenConfig());
            _outerEmployerFinanceHealthApiRestClient = new Outer_EmployerFinanceHealthApiRestClient(_objectContext);
        }

        public Task<RestResponse> Ping() => _outerEmployerFinanceHealthApiRestClient.Ping(HttpStatusCode.OK);

        public Task<RestResponse> CheckHealth() => _outerEmployerFinanceHealthApiRestClient.CheckHealth(HttpStatusCode.OK);

        public RestResponse GetAccountMinimumSignedAgreementVersion(long accountId)
        {
            return _outerEmployerFinanceApiRestClient.GetAccountMinimumSignedAgreementVersion(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetAccountUserWhichCanReceiveNotifications(long accountId)
        {
            return _outerEmployerFinanceApiRestClient.GetAccountUserWhichCanReceiveNotifications(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetPledges(long accountId)
        {
            return _outerEmployerFinanceApiRestClient.GetPledges(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetTrainingCoursesFrameworks()
        {
            return _outerEmployerFinanceApiRestClient.GetTrainingCoursesFrameworks(HttpStatusCode.OK);
        }

        public RestResponse GetTrainingCoursesStandards()
        {
            return _outerEmployerFinanceApiRestClient.GetTrainingCoursesStandards(HttpStatusCode.OK);
        }

        public RestResponse GetTransfersCounts(long accountId)
        {
            return _outerEmployerFinanceApiRestClient.GetTransfersCounts(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetTransfersFinancialBreakdown(long accountId)
        {
            return _outerEmployerFinanceApiRestClient.GetTransfersFinancialBreakdown(accountId, HttpStatusCode.OK);
        }
    }
}