using System.Net;
using RestSharp;
using SFA.DAS.API.Framework.Configs;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.API.Framework.RestClients;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public class Outer_EmployerFinanceApiRestClient(ObjectContext objectContext, Outer_ApiAuthTokenConfig config) : Outer_BaseApiRestClient(objectContext, config)
    {
        protected override string ApiName => "/employerfinance";

        public RestResponse GetAccountMinimumSignedAgreementVersion(long accountId, HttpStatusCode expectedResponse)
        {
            return Execute($"/Accounts/{accountId}/minimum-signed-agreement-version", expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse GetAccountUserWhichCanReceiveNotifications(long accountId, HttpStatusCode expectedResponse)
        {
            return Execute($"/Accounts/{accountId}/users/which-receive-notifications", expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse GetPledges(long accountId, HttpStatusCode expectedResponse)
        {
            return Execute($"/Pledges?accountId={accountId}", expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse GetTrainingCoursesFrameworks(HttpStatusCode expectedResponse)
        {
            return Execute($"/TrainingCourses/frameworks", expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse GetTrainingCoursesStandards(HttpStatusCode expectedResponse)
        {
            return Execute($"/TrainingCourses/standards", expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse GetTransfersCounts(long accountId, HttpStatusCode expectedResponse)
        {
            return Execute($"/Transfers/{accountId}/counts", expectedResponse).GetAwaiter().GetResult();
        }

        public RestResponse GetTransfersFinancialBreakdown(long accountId, HttpStatusCode expectedResponse)
        {
            return Execute($"/Transfers/{accountId}/financial-breakdown", expectedResponse).GetAwaiter().GetResult();
        }
    }
}
