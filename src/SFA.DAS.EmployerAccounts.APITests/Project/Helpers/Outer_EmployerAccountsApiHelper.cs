
using System;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers;

public class Outer_EmployerAccountsApiHelper
{
    private readonly Outer_EmployerAccountsApiRestClient _outerEmployerAccountsApiRestClient;
    private readonly Outer_EmployerAccountsHealthApiRestClient _outer_EmployerAccountsHealthApiRestClient;
    private readonly ObjectContext _objectContext;
    protected readonly FrameworkHelpers.RetryHelper _assertHelper;

    internal Outer_EmployerAccountsApiHelper(ScenarioContext context)
    {
        _objectContext = context.Get<ObjectContext>();
        _assertHelper = context.Get<FrameworkHelpers.RetryHelper>();
        _outerEmployerAccountsApiRestClient = new Outer_EmployerAccountsApiRestClient(_objectContext, context.GetOuter_ApiAuthTokenConfig());
        _outer_EmployerAccountsHealthApiRestClient = new Outer_EmployerAccountsHealthApiRestClient(_objectContext);
    }


    public async Task<RestResponse> Ping() => await _outer_EmployerAccountsHealthApiRestClient.Ping(HttpStatusCode.OK);

    public async Task<RestResponse> CheckHealth() => await _outer_EmployerAccountsHealthApiRestClient.CheckHealth(HttpStatusCode.OK);

    public async Task<RestResponse> GetAccountEnglishFractionCurrent(string hashedAccountId)
    {
        return await _outerEmployerAccountsApiRestClient.GetAccountEnglishFractionCurrent(hashedAccountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountEnglishFractionHistory(string hashedAccountId)
    {
        return await _outerEmployerAccountsApiRestClient.GetAccountEnglishFractionHistory(hashedAccountId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> GetAccountTeams(string accountId)
    {
        return await _outerEmployerAccountsApiRestClient.GetAccountTeams(accountId, HttpStatusCode.OK);
    }
    public async Task<RestResponse> GetAccountCreateTaskList(string accountId)
    {
        return await _outerEmployerAccountsApiRestClient.GetAccountCreateTaskList(accountId, HttpStatusCode.NoContent);
    }
    public async Task<RestResponse> GetReservation(string accountId)
    {
        return await _outerEmployerAccountsApiRestClient.GetReservation(accountId, HttpStatusCode.OK);
    }
    public async Task<RestResponse> GetAccountUsersAccounts(string userId)
    {
        return await _outerEmployerAccountsApiRestClient.GetAccountUsersAccounts(userId, HttpStatusCode.OK);
    }

    public async Task<RestResponse> ExecuteEndpoint(string endpoint, HttpStatusCode expectedResponse)
    {
        return await _outerEmployerAccountsApiRestClient.ExecuteEndpoint(endpoint, expectedResponse);
    }
}
