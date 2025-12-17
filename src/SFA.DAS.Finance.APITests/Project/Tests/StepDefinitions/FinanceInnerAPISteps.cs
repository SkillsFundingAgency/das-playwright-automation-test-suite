using Newtonsoft.Json;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Models;
using TechTalk.SpecFlow;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions;

[Binding]
public class FinanceInnerAPISteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly Inner_EmployerFinanceApiRestClient _innerApiRestClient;
    private readonly ObjectContext _objectContext;

    public FinanceInnerAPISteps(ScenarioContext context)
    {
        _scenarioContext = context;
        _innerApiRestClient = context.GetRestClient<Inner_EmployerFinanceApiRestClient>();
        _objectContext = context.Get<ObjectContext>();
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
        if (string.IsNullOrWhiteSpace(hashedAccountId))
        {
            Assert.Fail("hashedAccountId was not set in the test context; check DB setup or BeforeScenario hooks.");
            return;
        }

        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy");
        if (response == null || string.IsNullOrWhiteSpace(response.Content))
        {
            Assert.Fail($"Levy endpoint returned no content for hashedAccountId '{hashedAccountId}'.");
            return;
        }

        var result = JsonConvert.DeserializeObject<ICollection<LevyDeclaration>>(response.Content);
        if (result == null || !result.Any() || result.FirstOrDefault() == null)
        {
            Assert.Fail($"Levy endpoint returned an empty or invalid collection for hashedAccountId '{hashedAccountId}'. Content: {response.Content}");
            return;
        }

        var first = result.First();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/{first.PayrollYear}/{first.PayrollMonth}", HttpStatusCode.OK);

    }

    [Then(@"endpoint api/accounts/\{accountId}/transactions can be accessed")]
    public async Task ThenEndpointApiAccountsAccountIdTransactionsEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
    }

    [When(@"send an api request GET api/accounts/\{hashedAccountId\}/transactions")]
    public async Task WhenSendAnApiRequestGETApiAccountsAccountIdTransactions()
    {
        var hashedAccountId = GetHashedAccountId();
        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
        try
        {
            _objectContext.Replace("finance_lastResponse", response.Content);
        }
        catch
        {
            // ignore if replacing context fails
        }
    }

    [Then(@"Verify the transactions api response with records fetch from DB")]
    public async Task ThenVerifyTransactionsApiResponseWithRecordsFetchFromDB(Table table)
    {
        Assert.IsTrue(table.Rows.Count > 0 && table.Rows[0].ContainsKey("query"), "Expecting a table with a single column 'query' and a file name.");
        var queryFile = table.Rows[0]["query"].Trim();

        var accountId = GetAccountId();
        Assert.IsFalse(string.IsNullOrEmpty(accountId), "finance_accountId was not set in the test setup.");

        // Execute the transactions endpoint and capture the response to compare with SQL
        var hashedAccountId = GetHashedAccountId();
        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions");
        var apiContent = response?.Content ?? string.Empty;
        Assert.IsNotEmpty(apiContent, "API response content is empty.");

        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure FinanceBeforeScenarioHooks ran.");

        var replacements = new Dictionary<string, string> { { "AccountId", accountId } };
        var propertyOrder = new[] { "year", "month", "amount" };

        await AssertHelper.AssertApiResponseMatchesSql(accountsHelper, apiContent, queryFile, propertyOrder, replacements);
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
