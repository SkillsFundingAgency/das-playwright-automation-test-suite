namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers
{
    using System.Net;
    using System.Threading.Tasks;

    public class Outer_EmployerAccountsApiRestClient : Outer_BaseApiRestClient
    {
        public Outer_EmployerAccountsApiRestClient(ObjectContext objectContext, Outer_ApiAuthTokenConfig config)
            : base(objectContext, config)
        {
        }

        protected override string ApiName => "/employeraccounts";

         public async Task<RestResponse> GetAccountEnglishFractionCurrent(string hashedAccountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/{hashedAccountId}/levy/english-fraction-current", expectedResponse);
        }

        public async Task<RestResponse> GetAccountEnglishFractionHistory(string hashedAccountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/{hashedAccountId}/levy/english-fraction-history", expectedResponse);
        }
    }
}
