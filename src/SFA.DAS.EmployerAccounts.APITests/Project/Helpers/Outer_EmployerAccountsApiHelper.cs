
using System;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers;

public class Outer_EmployerAccountsApiHelper
{
    private readonly Outer_EmployerAccountsApiRestClient outerEmployerAccountsApiRestClient;
    private readonly Outer_EmployerAccountsHealthApiRestClient outerEmployerAccountsHealthApiRestClient;
    private readonly ObjectContext objectContext;
    protected readonly FrameworkHelpers.RetryHelper assertHelper;

    internal Outer_EmployerAccountsApiHelper(ScenarioContext context)
    {
        objectContext = context.Get<ObjectContext>();
        assertHelper = context.Get<FrameworkHelpers.RetryHelper>();
        outerEmployerAccountsApiRestClient = new Outer_EmployerAccountsApiRestClient(objectContext, context.GetOuter_ApiAuthTokenConfig());
        outerEmployerAccountsHealthApiRestClient = new Outer_EmployerAccountsHealthApiRestClient(objectContext);
    }


    public async Task<RestResponse> Ping() => await outerEmployerAccountsHealthApiRestClient.Ping(HttpStatusCode.OK);

    public async Task<RestResponse> CheckHealth() => await outerEmployerAccountsHealthApiRestClient.CheckHealth(HttpStatusCode.OK);

    public async Task<RestResponse> GetAccountEnglishFractionCurrent(string hashedAccountId)
    {
        return await outerEmployerAccountsApiRestClient.GetAccountEnglishFractionCurrent(hashedAccountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountEnglishFractionHistory(string hashedAccountId)
    {
        return await outerEmployerAccountsApiRestClient.GetAccountEnglishFractionHistory(hashedAccountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountTeams(string accountId)
    {
        return await outerEmployerAccountsApiRestClient.GetAccountTeams(accountId, HttpStatusCode.OK);
    }
    public async Task<RestResponse> GetAccountCreateTaskList(string accountId)
    {
        return await outerEmployerAccountsApiRestClient.GetAccountCreateTaskList(accountId, HttpStatusCode.NoContent);
    }
    public async Task<RestResponse> GetReservation(string accountId)
    {
        return await outerEmployerAccountsApiRestClient.GetReservation(accountId, HttpStatusCode.OK);
    }
    public async Task<RestResponse> GetAccountUsersAccounts(string userId)
    {
        return await outerEmployerAccountsApiRestClient.GetAccountUsersAccounts(userId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> ExecuteEndpoint(string endpoint, HttpStatusCode expectedResponse)
    {
        return await outerEmployerAccountsApiRestClient.ExecuteEndpoint(endpoint, expectedResponse);
    }
}
