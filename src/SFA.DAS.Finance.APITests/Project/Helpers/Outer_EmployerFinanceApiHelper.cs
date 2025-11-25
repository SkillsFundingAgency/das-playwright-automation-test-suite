using System.Net;
using RestSharp;
using TechTalk.SpecFlow;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.API.Framework.RestClients;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public class Outer_EmployerFinanceApiHelper
    {
        private readonly ObjectContext _objectContext;

        public Outer_EmployerFinanceApiHelper(ScenarioContext context)
        {
            _objectContext = context.Get<ObjectContext>();
        }

        public RestResponse Ping()
        {
            return new Outer_EmployerFinanceHealthApiRestClient(_objectContext).Ping(HttpStatusCode.OK).GetAwaiter().GetResult();
        }

        public RestResponse GetAccountMinimumSignedAgreementVersion(long accountId)
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetAccountMinimumSignedAgreementVersion(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetAccountUserWhichCanReceiveNotifications(long accountId)
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetAccountUserWhichCanReceiveNotifications(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetPledges(long accountId)
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetPledges(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetTrainingCoursesFrameworks()
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetTrainingCoursesFrameworks(HttpStatusCode.OK);
        }

        public RestResponse GetTrainingCoursesStandards()
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetTrainingCoursesStandards(HttpStatusCode.OK);
        }

        public RestResponse GetTransfersCounts(long accountId)
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetTransfersCounts(accountId, HttpStatusCode.OK);
        }

        public RestResponse GetTransfersFinancialBreakdown(long accountId)
        {
            return new Outer_EmployerFinanceApiRestClient(_objectContext, null).GetTransfersFinancialBreakdown(accountId, HttpStatusCode.OK);
        }
    }
}
