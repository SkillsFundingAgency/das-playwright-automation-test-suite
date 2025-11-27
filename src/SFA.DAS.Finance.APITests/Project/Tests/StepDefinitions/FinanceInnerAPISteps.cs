using Newtonsoft.Json;
using SFA.DAS.EmployerFinance.APITests.Project.Models;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions
{
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
    public void ThenEndpointDas_Employer_Finance_ApiPingCanBeAccessed()
    {
        _innerApiRestClient.ExecuteEndpoint("/ping", HttpStatusCode.OK);
    }

    [Then(@"endpoint /service/keepalive can be accessed")]
    public void ThenEndpointServiceKeepAliveCanBeAccessed()
    {
        _innerApiRestClient.ExecuteEndpoint("/service/keepalive", HttpStatusCode.NoContent);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdLevyEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/GetLevyForPeriod can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdLevyGetLevyForPeriodEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        var response = _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy");
        var result = JsonConvert.DeserializeObject<ICollection<LevyDeclaration>>(response.Content);
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/{result.FirstOrDefault().PayrollYear}/{result.FirstOrDefault().PayrollMonth}", HttpStatusCode.OK);

    }

    [Then(@"endpoint api/accounts/\{accountId}/transactions can be accessed")]
    public void ThenEndpointApiAccountsAccountIdTransactionsEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/transactions/GetTransactions can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdTransactionsGetTransactionsEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        var response = _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
        var result = JsonConvert.DeserializeObject<ICollection<TransactionSummary>>(response.Content);
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions/{result.FirstOrDefault().Year}/{result.FirstOrDefault().Month}", HttpStatusCode.OK);
    }

    [Then(@"endpoint GetFinanceStatistics can be accessed")]
    public void ThenEndpointGetFinanceStatisticsCanBeAccessed()
    {
        _innerApiRestClient.ExecuteEndpoint("/api/financestatistics", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/transferAllowance can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdTransferAllowanceCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transferAllowance", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/english-fraction-current can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdLevyEnglishFractionCurrentCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/english-fraction-current", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{hashedAccountId}/levy/english-fraction-history can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdLevyEnglishFractionHistoryCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        var empRef = _objectContext.GetEmpRef();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/english-fraction-history?empRef={empRef}", HttpStatusCode.OK);
    }

    [Then(@"endpoint /api/accounts/\{hashedAccountId}/transfers/connections can be accessed")]
    public void ThenEndpointApiAccountsHashedAccountIdTransfersConnectionsCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transfers/connections", HttpStatusCode.OK);
    }

    [Then(@"endpoint /api/accounts/internal/\{accountId}/transfers/connections can be accessed")]
    public void ThenEndpointApiAccountsInternalAccountIdTransfersConnectionsCanBeAccessed()
    {
        var accountId = GetAccountId();
        _innerApiRestClient.ExecuteEndpoint($"/api/accounts/internal/{accountId}/transfers/connections", HttpStatusCode.OK);
    }


    [Then(@"endpoint api/period-ends can be accessed")]
    public void ThenEndpointApiPeriodEndsCanBeAccessed()
    {
        _innerApiRestClient.ExecuteEndpoint("/api/period-ends", HttpStatusCode.OK);
    }

    private string GetHashedAccountId() => _objectContext.GetHashedAccountId();

    private string GetAccountId() => _objectContext.GetAccountId();
}
}
