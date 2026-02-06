namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers
{


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

        public async Task<RestResponse> GetAccountTeams(string accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/{accountId}/teams", expectedResponse);
        }
        public async Task<RestResponse> GetAccountCreateTaskList(string accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Accounts/{accountId}/create-task-list", expectedResponse);
        }
        public async Task<RestResponse> GetReservation(string accountId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/Reservation/{accountId}", expectedResponse);
        }
        public async Task<RestResponse> GetAccountUsersAccounts(string userId, HttpStatusCode expectedResponse)
        {
            return await Execute($"/AccountUsers/{userId}/accounts", expectedResponse);
        }

        public async Task<RestResponse> ExecuteEndpoint(string endpoint, HttpStatusCode expectedResponse)
        {
            return await Execute(Method.Get, endpoint, string.Empty, expectedResponse);
        }
    }
}
