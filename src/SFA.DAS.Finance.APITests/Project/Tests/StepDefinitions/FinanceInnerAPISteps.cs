using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Finance.APITests.Project.Models;
using System;
using System.Globalization;
using System.Net;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions;

[Binding]
public class FinanceInnerAPISteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly Inner_EmployerFinanceApiRestClient _innerApiRestClient;
    private readonly Outer_EmployerFinanceApiHelper _outerApiHelper;
    private readonly ObjectContext _objectContext;
    private readonly StepHelper _stepHelper;

    public FinanceInnerAPISteps(ScenarioContext context)
    {
        _scenarioContext = context;
        _innerApiRestClient = context.GetRestClient<Inner_EmployerFinanceApiRestClient>();
        _outerApiHelper = new Outer_EmployerFinanceApiHelper(context);
        _objectContext = context.Get<ObjectContext>();
        _stepHelper = new StepHelper(_scenarioContext, _objectContext, _outerApiHelper);
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
        var first = result.First();

        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/levy/{first.PayrollYear}/{first.PayrollMonth}", HttpStatusCode.OK);
    }

    [Then(@"endpoint \/api/accounts/\{accountId}/transactions can be accessed")]
    public async Task ThenEndpointApiAccountsAccountIdTransactionsEndpointCanBeAccessed()
    {
        var hashedAccountId = GetHashedAccountId();
        await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
    }

    [When(@"send an api request GET api/accounts/\{hashedAccountId\}/transactions")]
    public async Task WhenSendAnApiRequestGETApiAccountsAccountIdTransactions()
    {
        var hashedAccountId = GetHashedAccountId();
       await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{hashedAccountId}/transactions", HttpStatusCode.OK);
    }

    [Then(@"endpoint api/accounts/\{accountId}/transactions can be accessed")]
    public async Task ThenEndpointApiAccountsAccountIdTransactionsEndpointCanBeAccessed1()
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

    [Given(@"post new transfers to TransferStaging table via api")]
    public async Task GivenPostNewTransfersToTransferStagingTableViaApi()
    {
        var payloadContent = await _stepHelper.PrepareTransferStagingPayload("TransferStagingTemplate.json");
        await _innerApiRestClient.PostTransferStaging(payloadContent);
    }

    [When(@"find record in TransferStaging table")]
    public async Task WhenExecuteTheDBForNewlyInsertedRecord()
    {
        var transferId = Convert.ToInt32(_scenarioContext["transferId"], CultureInfo.InvariantCulture);
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        
        var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(
            "getTransferStagingByTransferId.sql",
            new Dictionary<string, string> { { "transferId", transferId.ToString(CultureInfo.InvariantCulture) } });

        try { _scenarioContext.Set(sqlResult, "transferStagingDbRecord"); } catch { _scenarioContext["transferStagingDbRecord"] = sqlResult; }
    }

    [Then(@"Verify the record in TransferStaging table with the data posted via api")]
    public async Task ThenVerifyTheRecordInTransferStagingTableWithTheDataPostedViaApi() => await _stepHelper.CompareTransferStagingDataAgainstDb();

    [Given(@"post new payments to PaymentStaging table via api")]
    public async Task GivenPostNewPaymentsToPaymentStagingTableViaApi()
    {
        var payloadContent = await _stepHelper.PreparePaymentStagingPayload("PaymentStagingTemplate.json");
        await _innerApiRestClient.PostPaymentsStaging(payloadContent);
    }

    [When(@"find record in PaymentStaging table")]
    public async Task WhenFindRecordInPaymentStagingTable()
    {
        var paymentId = _scenarioContext.Get<string>("paymentId");
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();

        var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(
            "getPaymentStagingByPaymentId.sql",
            new Dictionary<string, string> { { "paymentId", paymentId } });

        try { _scenarioContext.Set(sqlResult, "paymentStagingDbRecord"); } catch { _scenarioContext["paymentStagingDbRecord"] = sqlResult; }
    }

    [Then(@"Verify the record in PaymentStaging table with the data posted via api")]
    public async Task ThenVerifyTheRecordInPaymentStagingTableWithTheDataPostedViaApi() => await _stepHelper.ComparePaymentStagingDataAgainstDb();

    [Given(@"post new period end to PeriodEnd table via api")]
    [Given(@"a new period end is submitted")]
    public async Task GivenPostNewPeriodEndViaApi()
    {
        var payloadContent = await _stepHelper.PreparePeriodEndPayload("PeriodEndTemplate.json");
        await _innerApiRestClient.PostPeriodEnds(payloadContent);
    }

    [When(@"find record in PeriodEnd table")]
    [When(@"the saved period end details are checked")]
    public async Task WhenFindRecordInPeriodEndTable()
    {
        var periodEndId = _scenarioContext.Get<string>("periodEndId");
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();

        var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(
            "getPeriodEndByPeriodEndId.sql",
            new Dictionary<string, string> { { "periodEndId", periodEndId } });

        try { _scenarioContext.Set(sqlResult, "periodEndDbRecord"); } catch { _scenarioContext["periodEndDbRecord"] = sqlResult; }
    }

    [Then(@"Verify the record in PeriodEnd table with the data posted via api")]
    [Then(@"the period end details are correct")]
    public async Task ThenVerifyTheRecordInPeriodEndTableWithTheDataPostedViaApi() => await _stepHelper.ComparePeriodEndDataAgainstDb();

    [When(@"get period ends list via api and save response in context")]
    [When(@"the period end list is requested")]
    public async Task WhenGetPeriodEndsListViaApiAndSaveResponseInContext()
    {
        var response = await _innerApiRestClient.ExecuteEndpoint("/api/period-ends", HttpStatusCode.OK);
        try { _scenarioContext.Set(response.Content, "periodEndsResponseContent"); } catch { _scenarioContext["periodEndsResponseContent"] = response.Content; }
    }

    [When(@"get period end by id via api and save response in context")]
    [When(@"the period end is requested by its id")]
    public async Task WhenGetPeriodEndByIdViaApiAndSaveResponseInContext()
    {
        var periodEndId = _scenarioContext.Get<string>("periodEndId");
        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/period-ends/{periodEndId}", HttpStatusCode.OK);

        var parsed = JToken.Parse(response.Content);
        var actualResult = parsed as JObject ?? parsed["data"] as JObject ?? parsed["periodEnd"] as JObject;

        try { _scenarioContext.Set(actualResult ?? parsed, "actualResult"); } catch { _scenarioContext["actualResult"] = actualResult ?? parsed; }
    }

    [Then(@"verify period ends response contains posted period end id")]
    [Then(@"the new period end is included in the period end list")]
    public void ThenVerifyPeriodEndsResponseContainsPostedPeriodEndId()
    {
        var expectedPeriodEndId = _scenarioContext.Get<string>("periodEndId");
        var responseContent = _scenarioContext.Get<string>("periodEndsResponseContent");

        Assert.IsFalse(string.IsNullOrWhiteSpace(responseContent), "Period-ends response content is empty.");

        var parsed = JToken.Parse(responseContent);
        var periodEnds = parsed as JArray
            ?? parsed["periodEnds"] as JArray
            ?? parsed["data"] as JArray;

        Assert.IsNotNull(periodEnds, "Period-ends response does not contain an array payload.");

        var found = periodEnds
            .Children<JObject>()
            .Any(item => item.Properties().Any(prop =>
                string.Equals(prop.Name, "periodEndId", StringComparison.OrdinalIgnoreCase)
                && string.Equals(prop.Value?.ToString(), expectedPeriodEndId, StringComparison.OrdinalIgnoreCase)));

        Assert.IsTrue(found, $"Period-ends response does not include periodEndId '{expectedPeriodEndId}'.");
    }


    [When(@"put payment metadata in PaymentMetaDataStaging table via api")]
    public async Task WhenPutPaymentMetadataInPaymentMetaDataStagingTableViaApi()
    {
        var paymentId = _scenarioContext.Get<string>("paymentId");
        var payloadContent = await _stepHelper.PreparePaymentMetaDataStagingPayload("PaymentMetaDataStagingTemplate.json");
        await _innerApiRestClient.PutPaymentMetaDataStaging(paymentId, payloadContent);
    }

    [When(@"find record in PaymentMetaDataStaging table")]
    public async Task WhenFindRecordInPaymentMetaDataStagingTable()
    {
        var paymentId = _scenarioContext.Get<string>("paymentId");
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();

        var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(
            "getPaymentMetaDataStagingByPaymentId.sql",
            new Dictionary<string, string> { { "paymentId", paymentId } });

        try { _scenarioContext.Set(sqlResult, "paymentMetaDataStagingDbRecord"); } catch { _scenarioContext["paymentMetaDataStagingDbRecord"] = sqlResult; }
    }

    [Then(@"Verify the record in PaymentMetaDataStaging table with the data posted via api")]
    public async Task ThenVerifyTheRecordInPaymentMetaDataStagingTableWithTheDataPostedViaApi() => await _stepHelper.ComparePaymentMetaDataStagingDataAgainstDb();

    [Given(@"post english fractions via api")]
    public async Task GivenPostEnglishFractionsViaApi()
    {
        // Generate a unique EmpRef for this scenario to avoid parallel test conflicts
        var uniqueEmpRef = $"777/GDS{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        var payloadContent = await _stepHelper.PrepareEnglishFractionsPayload("EnglishFractionsTemplate.json", empRefOverride: uniqueEmpRef);
        var response = await _innerApiRestClient.PostEnglishFractions(payloadContent);

        AssertEnglishFractionsPostResponse(response, expectedStored: 1, expectedIgnored: 0);
    }

    [Given(@"post english fractions via api with update required false")]
    public async Task GivenPostEnglishFractionsViaApiWithUpdateRequiredFalse()
    {
        // Generate a unique EmpRef for this scenario to avoid parallel interference
        var uniqueEmpRef = $"777/GDS{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

        // Seed the record so it already exists in DB (updateRequired=true)
        var seedPayload = await _stepHelper.PrepareEnglishFractionsPayload("EnglishFractionsTemplate.json", empRefOverride: uniqueEmpRef);
        await _innerApiRestClient.PostEnglishFractions(seedPayload);

        // Post again with updateRequired=false — API should ignore it as the record already exists
        // Build the ignored payload inline to avoid overwriting the context set by the seed call above
        var ignoredPayload = JObject.Parse(seedPayload);
        ignoredPayload["updateRequired"] = false;
        var response = await _innerApiRestClient.PostEnglishFractions(ignoredPayload.ToString(Newtonsoft.Json.Formatting.None));

        AssertEnglishFractionsPostResponse(response, expectedStored: 0, expectedIgnored: 1);
    }

    [When(@"find record in EnglishFraction table")]
    public async Task WhenFindRecordInEnglishFractionTable()
    {
        var empRef = _scenarioContext.Get<string>("englishFractionsEmpRef");
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();

        var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(
            "getEnglishFractionByEmpRef.sql",
            new Dictionary<string, string> { { "empRef", empRef } });

        try { _scenarioContext.Set(sqlResult, "englishFractionsDbRecord"); } catch { _scenarioContext["englishFractionsDbRecord"] = sqlResult; }
    }

    [Then(@"Verify the record in EnglishFraction table with the data posted via api")]
    public async Task ThenVerifyTheRecordInEnglishFractionTableWithTheDataPostedViaApi() => await _stepHelper.CompareEnglishFractionsDataAgainstDb();

    [Given(@"post english fraction calculation date via api")]
    public async Task GivenPostEnglishFractionCalculationDateViaApi()
    {
        var payloadContent = await _stepHelper.PrepareEnglishFractionCalculationDatePayload("EnglishFractionCalculationDateTemplate.json");
        await _innerApiRestClient.PostEnglishFractionCalculationDate(payloadContent);
    }

    [When(@"find records in EnglishFractionCalculationDate table")]
    public async Task WhenFindRecordsInEnglishFractionCalculationDateTable()
    {
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        var expectedDate = _scenarioContext.Get<string>("englishFractionCalculationDate");
        var sql = @"SELECT TOP (1000) [DateCalculated]
FROM [employer_financial].[EnglishFractionCalculationDate]";

        List<string[]> sqlResults = null;

        for (var attempt = 0; attempt < 5; attempt++)
        {
            sqlResults = await accountsHelper.ExecuteMultipleSql(sql, useFinanceDb: true);

            var found = sqlResults
                .Where(row => row != null && row.Length > 0 && !string.IsNullOrWhiteSpace(row[0]))
                .Select(row => DateTime.Parse(row[0], new CultureInfo("en-GB"), DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Contains(expectedDate, StringComparer.OrdinalIgnoreCase);

            if (found)
            {
                break;
            }

            await Task.Delay(2000);
        }

        try { _scenarioContext.Set(sqlResults, "englishFractionCalculationDateDbRecords"); } catch { _scenarioContext["englishFractionCalculationDateDbRecords"] = sqlResults; }
    }

    [Then(@"Verify the records in EnglishFractionCalculationDate table contains the data posted via api")]
    public async Task ThenVerifyTheRecordsInEnglishFractionCalculationDateTableContainsTheDataPostedViaApi() => await _stepHelper.VerifyEnglishFractionCalculationDateExistsInDb();

    private static void AssertEnglishFractionsPostResponse(RestResponse response, int expectedStored, int expectedIgnored)
    {
        var responseBody = JObject.Parse(response.Content);
        Assert.AreEqual(expectedStored, responseBody["stored"]?.Value<int>() ?? -1, "stored mismatch");
        Assert.AreEqual(expectedIgnored, responseBody["ignored"]?.Value<int>() ?? -1, "ignored mismatch");
    }

    
    [Given(@"a valid employer account with a PAYE reference")]
    public async Task GivenQueryAccountPayeAndSaveAccountIdAndEmpRefInContext()
    {
        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        var result = await accountsHelper.ExecuteSql(
            "SELECT TOP (1) [AccountId], [EmpRef] FROM [employer_financial].[AccountPaye] WHERE Aorn IS NULL",
            useFinanceDb: true);
        
        if (result == null || result.Count < 2)
            throw new Exception("No AccountPaye records found");

        var accountId = result[0]?.Trim();
        var empRef = result[1]?.Trim();
        
        if (string.IsNullOrWhiteSpace(accountId))
            throw new Exception("Account ID is empty");
        if (string.IsNullOrWhiteSpace(empRef))
            throw new Exception("EmpRef is empty");
            
        _scenarioContext["accountId"] = accountId;
        _scenarioContext["empRef"] = empRef;
    }

    [When(@"PAYE schemes are requested for that employer account")]
    public async Task WhenCallTheGetPayeSchemesEndpointAndSaveResponse()
    {
        var accountId = _scenarioContext["accountId"].ToString();
        var response = await _innerApiRestClient.ExecuteEndpoint($"/api/accounts/{accountId}/paye-schemes?source=government-gateway", HttpStatusCode.OK);
        
        _scenarioContext["payeSchemesResponse"] = response.Content;
    }

    [Then(@"the returned PAYE schemes include that PAYE reference")]
    public async Task ThenVerifyThePayeSchemesResponseContainsTheEmpRef()
    {
        var empRef = _scenarioContext["empRef"].ToString();
        var responseContent = _scenarioContext["payeSchemesResponse"].ToString();
        
        await _stepHelper.VerifyEmpRefInPayeSchemesResponse(responseContent, empRef);
    }

    private string GetHashedAccountId() => _objectContext.GetHashedAccountId();

    private string GetAccountId() => _objectContext.GetAccountId();
}
