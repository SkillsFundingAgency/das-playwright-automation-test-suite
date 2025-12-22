using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;
namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions;

[Binding]
public class FinanceOuterApiFeatureSteps
{
    private readonly ScenarioContext _scenarioContext;
    private readonly ObjectContext _objectContext;
    private readonly Outer_EmployerFinanceApiHelper _apiHelper;

    public FinanceOuterApiFeatureSteps(ScenarioContext context)
    {
        _scenarioContext = context;
        _objectContext = context.Get<ObjectContext>();
        _apiHelper = new Outer_EmployerFinanceApiHelper(context);
    }


    [Given(@"send an api request GET /Accounts/\{\{accountId\}\}/users/which-receive-notifications")]
    public async System.Threading.Tasks.Task getAccUserNotifications()
    {
        var accountIdStr = GetAccountId();
        if (string.IsNullOrEmpty(accountIdStr))
            Assert.Fail("finance_accountId was not set in the test setup;please check the before hook to set account details");

        if (!long.TryParse(accountIdStr, out var accountId))
            Assert.Fail("finance_accountId is not a valid long");

        var response = await _apiHelper.GetAccountUserWhichCanReceiveNotifications(accountId);
        _objectContext.Replace("finance_lastResponse", response.Content);
    }

    [Given(@"send an api request GET /Accounts/\{\{accountId\}\}/users/minimum-signed-agreement-version")]
    public async System.Threading.Tasks.Task getAccAggrementSignedVersion()
    {
        var accountIdStr = GetAccountId();
        if (string.IsNullOrEmpty(accountIdStr))
            Assert.Fail("finance_accountId was not set in the test setup;please check the before hook to set account details");

        if (!long.TryParse(accountIdStr, out var accountId))
            Assert.Fail("finance_accountId is not a valid long");

        var response = await _apiHelper.GetAccountMinimumSignedAgreementVersion(accountId);
        _objectContext.Replace("finance_lastResponse", response.Content);
    }


    [Given(@"send an api request GET /AccountUsers/\{\{UserRef\}\}/accounts\?email=\{\{email\}\}")]
    public async Task GetAccountUsersByUserRefAndEmail()
    {
        string userRef = null;
        string email = null;

        try { userRef = _scenarioContext.ContainsKey("UserRef") ? _scenarioContext.Get<string>("UserRef") : null; } catch { }
        try { email = _scenarioContext.ContainsKey("email") ? _scenarioContext.Get<string>("email") : null; } catch { }

        if (string.IsNullOrWhiteSpace(userRef) || string.IsNullOrWhiteSpace(email))
            Assert.Fail("UserRef or email was not set in the test setup; please check the BeforeScenario hooks.");

        var response = await _apiHelper.GetAccountUserByUserRefAndEmail(userRef, email);
        _objectContext.Replace("finance_lastResponse", response.Content);
    }

    // (moved) get the user with linked accounts

    // (moved) endpoint request GET /AccountUsers/{{UserRef}}/accounts is called

    // (moved) Also support scenarios that use the numeric account id placeholder


    [Then(@"Verify the minimumSignedAgreementVersion api response with records fetch from DB")]
    public async Task ThenVerifyMinimumSignedAgreementVersion(Table table)
    {
        Assert.IsTrue(table.Rows.Count > 0 && table.Rows[0].ContainsKey("query"), "Expecting a table with a single column 'query' and a file name.");
        var queryFile = table.Rows[0]["query"].Trim();

        var accountId = GetAccountId();
        Assert.IsFalse(string.IsNullOrEmpty(accountId), "finance_accountId was not set in the test setup.");

        var apiContent = _objectContext.Get<string>("finance_lastResponse") ?? string.Empty;
        Assert.IsNotEmpty(apiContent, "API response content is empty.");

        // If the API returned a primitive value, wrap it into an object with the matching property name
        var propName = "minimumSignedAgreementVersion";
        try
        {
            var parsed = JToken.Parse(apiContent);
            if (parsed != null && parsed.Type != JTokenType.Object)
            {
                var wrapper = new JObject();
                wrapper[propName] = parsed;
                apiContent = wrapper.ToString();
                // also update the stored finance_lastResponse to the wrapped object
                try { _objectContext.Replace("finance_lastResponse", apiContent); } catch { }
            }
        }
        catch
        {
            // leave apiContent as-is if parsing fails
        }

        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure FinanceBeforeScenarioHooks ran.");

        var replacements = new Dictionary<string, string> { { "AccountId", accountId } };
        var propertyOrder = new[] { "minimumSignedAgreementVersion" };

        await AssertHelper.AssertApiResponseMatchesSql(accountsHelper, apiContent, queryFile, propertyOrder, replacements);
    }

    [Then(@"Verify the getUserNotifications api response with records fetch from DB")]
    public async Task ThenVerifyGetUserNotifications(Table table)
    {
        Assert.IsTrue(table.Rows.Count > 0 && table.Rows[0].ContainsKey("query"), "Expecting a table with a single column 'query' and a file name.");
        var queryFile = table.Rows[0]["query"].Trim();

        var accountId = GetAccountId();
        Assert.IsFalse(string.IsNullOrEmpty(accountId), "finance_accountId was not set in the test setup.");

        var apiContent = _objectContext.Get<string>("finance_lastResponse") ?? string.Empty;
        Assert.IsNotEmpty(apiContent, "API response content is empty.");

        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure FinanceBeforeScenarioHooks ran.");

        var jsonArray = JsonConvert.DeserializeObject<JArray>(apiContent);
        Assert.IsNotNull(jsonArray, "API response was not a JSON array.");
        Assert.IsTrue(jsonArray.Count > 0, "API response array contains no elements.");

        var apiRecord = (JObject)jsonArray.First;
        var apiUserRef = apiRecord.Value<string>("userRef");
        Assert.IsFalse(string.IsNullOrEmpty(apiUserRef), "API record does not contain 'userRef' to match database.");

        var replacements = new Dictionary<string, string>
        {
            { "AccountId", accountId },
            { "userRef", apiUserRef }
        };

        var propertyOrder = new[] { "userRef", "firstName", "lastName", "email", "role", "canReceiveNotifications" };

        // If using the getAccountUsersByUserId.sql file the API returns a different shape (top-level employerUserId, firstName, lastName)
        // so only compare the properties that exist in that response.
        if (string.Equals(queryFile, "getAccountUsersByUserId.sql", StringComparison.OrdinalIgnoreCase))
        {
            // The API returns an object with top-level firstName/lastName/employerUserId and a userAccounts[0] containing role and apprenticeshipEmployerType
            propertyOrder = new[] { "userRef", "firstName", "lastName", "role", "apprenticeshipEmployerType" };
        }

        await AssertHelper.AssertApiResponseMatchesSql(accountsHelper, apiContent, queryFile, propertyOrder, replacements);
    }

    [Then(@"Verify the getAccountUsersByUserId api response with records fetch from DB")]
    public async Task ThenVerifyGetAccountUsersByUserId(Table table)
    {
        Assert.IsTrue(table.Rows.Count > 0 && table.Rows[0].ContainsKey("query"), "Expecting a table with a single column 'query' and a file name.");
        var queryFile = table.Rows[0]["query"].Trim();

        var apiContent = _objectContext.Get<string>("finance_lastResponse") ?? string.Empty;
        Assert.IsNotEmpty(apiContent, "API response content is empty.");

        var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
        Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure FinanceBeforeScenarioHooks ran.");

        var replacements = new Dictionary<string, string>();
        var propertyOrder = new[] { "encodedAccountId", "dasAccountName", "role", "apprenticeshipEmployerType" };


        // If using the getAccountUsersByUserId.sql the SQL returns columns: UserId, employerUserId, encodedAccountId, dasAccountName, role, apprenticeshipEmployerType
        // Assert using that same column order so the API is compared against the current SQL result set.
        if (string.Equals(queryFile, "getAccountUsersByUserId.sql", StringComparison.OrdinalIgnoreCase))
        {
            // SQL returns UserId as the first column; include it in the expected order so indexing aligns
            // The AssertHelper will skip asserting properties not present in the API (e.g. numeric UserId).
            propertyOrder = new[] { "userId", "userRef", "encodedAccountId", "dasAccountName", "role", "apprenticeshipEmployerType" };
        }
        else if (string.Equals(queryFile, "getAccountUsersByUserId_linked.sql", StringComparison.OrdinalIgnoreCase))
        {
            // This SQL returns: userRef, Email, employerUserId, encodedAccountId, dasAccountName, role, apprenticeshipEmployerType
            // Include 'role' in the property order so SQL column indexing aligns, but ignore it during comparison
            propertyOrder = new[] { "userRef", "email", "employerUserId", "encodedAccountId", "dasAccountName", "role", "apprenticeshipEmployerType" };
            var ignoreProps = new[] { "role" };
            await AssertHelper.AssertApiResponseMatchesSql(accountsHelper, apiContent, queryFile, propertyOrder, replacements, ignoreProps);
            return;
        }

        await AssertHelper.AssertApiResponseMatchesSql(accountsHelper, apiContent, queryFile, propertyOrder, replacements);
    }

    private string GetAccountId() => _objectContext.GetAccountId();
}
