using Newtonsoft.Json;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Finance.APITests.Project.Models;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions;

[Binding]
public class FinanceInnerAPISteps
{
    private readonly Inner_EmployerFinanceApiRestClient _innerApiRestClient;
    private readonly EmployerFinanceSqlHelper _employerFinanceSqlDbHelper;
    private readonly EmployerAccountsSqlHelper _employerAccountsSqlDbHelper;
    private readonly ObjectContext _objectContext;

    public FinanceInnerAPISteps(ScenarioContext context)
    {
        _innerApiRestClient = context.GetRestClient<Inner_EmployerFinanceApiRestClient>();
        _employerFinanceSqlDbHelper = context.Get<EmployerFinanceSqlHelper>();
        _employerAccountsSqlDbHelper = context.Get<EmployerAccountsSqlHelper>();
        _objectContext = context.Get<ObjectContext>();
        var accountid = _employerFinanceSqlDbHelper.SetAccountId();
        _employerAccountsSqlDbHelper.SetHashedAccountId(accountid);
        _employerFinanceSqlDbHelper.SetEmpRef();
    }

    [Then(@"endpoint das-employer-finance-api /ping can be accessed")]
    public async Task ThenEndpointDas_Employer_Finance_ApiPingCanBeAccessed()
    {
        await _innerApiRestClient.ExecuteEndpoint("/ping", HttpStatusCode.OK);
    }

    [Then(@"endpoint /service/keepalive can be accessed")]
    public async Task ThenEndpointServiceKeepAliveCanBeAccessed()
    {
        await _innerApiRestClient.ExecuteEndpoint("/service/keepalive", HttpStatusCode.NoContent);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdLevyEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/GetLevyForPeriod can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdLevyGetLevyForPeriodEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy");
        var result = JsonConvert.DeserializeObject<ICollection<LevyDeclaration>>(response.Content);
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/{result.FirstOrDefault().PayrollYear}/{result.FirstOrDefault().PayrollMonth}", HttpStatusCode.OK);

    }

    [Then(@"endpoint api/accounts/\{accountId}/transactions can be accessed")]
    public async Task ThenEndpointApiAccountsAccountIdTransactionsEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/transactions/GetTransactions can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdTransactionsGetTransactionsEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
        var result = JsonConvert.DeserializeObject<ICollection<TransactionSummary>>(response.Content);
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions/{result.FirstOrDefault().Year}/{result.FirstOrDefault().Month}", HttpStatusCode.OK);
    }

    [Then(@"endpoint GetFinanceStatistics can be accessed")]
    public async Task ThenEndpointGetFinanceStatisticsCanBeAccessed()
    {
        await _innerApiRestClient.ExecuteEndpoint("/api/financestatistics", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/transferAllowance can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdTransferAllowanceCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transferAllowance", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/english-fraction-current can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdLevyEnglishFractionCurrentCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/english-fraction-current", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/english-fraction-history can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdLevyEnglishFractionHistoryCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        var empRef = _objectContext.GetEmpRef();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/english-fraction-history?empRef={empRef}", HttpStatusCode.OK);
    }

    [Then(@"endpoint /api/accounts/\{hashedAccountId}/transfers/connections can be accessed")]
    public async Task ThenEndpointApiAccountsHashedAccountIdTransfersConnectionsCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transfers/connections", HttpStatusCode.OK);
    }

    [Then(@"endpoint /api/accounts/internal/\{accountId}/transfers/connections can be accessed")]
    public async Task ThenEndpointApiAccountsInternalAccountIdTransfersConnectionsCanBeAccessed()
    {
        var accountId = GetAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/transfers/connections", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/period-ends can be accessed")]
    public async Task ThenEndpointApiPeriodEndsCanBeAccessed()
    {
        await _innerApiRestClient.ExecuteEndpoint("/api/period-ends", HttpStatusCode.OK);
    }

    private string GetHashedAccountId() => _objectContext.GetHashedAccountId();

    private string GetAccountId() => _objectContext.GetAccountId();
}
