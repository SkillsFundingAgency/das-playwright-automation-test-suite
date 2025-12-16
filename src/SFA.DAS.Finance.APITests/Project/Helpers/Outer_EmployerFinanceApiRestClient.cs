namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    using System.Net;
    using System.Threading.Tasks;

    public class Outer_EmployerFinanceApiRestClient : Outer_BaseApiRestClient
    {
        public Outer_EmployerFinanceApiRestClient(ObjectContext objectContext, Outer_ApiAuthTokenConfig config)
            : base(objectContext, config)
        {
        }

        protected override string ApiName => "/employerfinance";

        public async Task<RestResponse> GetAccountMinimumSignedAgreementVersion(long accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/{accountId}/minimum-signed-agreement-version", expectedResponse);
        }

        public async Task<RestResponse> GetAccountUserWhichCanReceiveNotifications(long accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/{accountId}/users/which-receive-notifications", expectedResponse);
        }

        public async Task<RestResponse> GetAccountUserWhichCanReceiveNotificationsByHashedId(string hashedAccountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/hashed/{hashedAccountId}/users/which-receive-notifications", expectedResponse);
        }

        public async Task<RestResponse> GetAccountUserByUserRefAndEmail(string userRef, string email, HttpStatusCode expectedResponse)
        {
            return await Execute($"/AccountUsers/{userRef}/accounts?email={email}", expectedResponse);
        }

        public async Task<RestResponse> GetPledges(long accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Pledges?accountId={accountId}", expectedResponse);
        }

        public async Task<RestResponse> GetTrainingCoursesFrameworks(HttpStatusCode expectedResponse)
        {
            return await Execute($"/TrainingCourses/frameworks", expectedResponse);
        }

        public async Task<RestResponse> GetTrainingCoursesStandards(HttpStatusCode expectedResponse)
        {
            return await Execute($"/TrainingCourses/standards", expectedResponse);
        }

        public async Task<RestResponse> GetTransfersCounts(long accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Transfers/{accountId}/counts", expectedResponse);
        }

        public async Task<RestResponse> GetTransfersFinancialBreakdown(long accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Transfers/{accountId}/financial-breakdown", expectedResponse);
        }
    }
}
