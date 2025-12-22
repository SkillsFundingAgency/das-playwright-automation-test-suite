using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class MovedFinanceOuterApiSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ObjectContext _objectContext;
        private readonly Outer_EmployerFinanceApiHelper _apiHelper;
        private readonly StepHelper _stepHelper;

        public MovedFinanceOuterApiSteps(ScenarioContext context)
        {
            _scenarioContext = context;
            _objectContext = context.Get<ObjectContext>();
            _apiHelper = new Outer_EmployerFinanceApiHelper(context);
            _stepHelper = new StepHelper(_scenarioContext, _objectContext, _apiHelper);
        }



        [Given(@"an employer account (can|cannot) receive notifications")]
        public async Task GivenAnEmployerAccountCanOrCannotReceiveNotifications(string canOrCannot) => await _stepHelper.PopulateExpectedUserNotification(canOrCannot);

        [When(@"endpoint /Accounts/\{accountId\}/users/which-receive-notifications is called")]
        public Task WhenEndpointAccountsAccountIdUsersWhichReceiveNotificationsIsCalled() => _stepHelper.CallGetNotificationRequestSaveResponse();

        [Then(@"the response body should contain valid account details")]
        public Task ThenTheResponseBodyShouldContainValidAccountDetails() => Task.Run(() => _stepHelper.AssertApiMatchesSqlRow());


        [Given(@"get the user with linked accounts")]
        public async System.Threading.Tasks.Task GetUserWithLinkedAccounts()
        {
            var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
            Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure BeforeScenario hooks ran.");

            var sql = @"SELECT TOP 1 mv.UserRef as userRef, mv.Email as Email,
            mv.UserRef as employerUserId,
            mv.HashedAccountId as encodedAccountId,
            mv.AccountName as dasAccountName,
            CASE WHEN mv.Role = 1 THEN 'Owner' WHEN mv.Role = 0 THEN 'User' ELSE 'Unknown' END AS role,
            CASE WHEN acc.ApprenticeshipEmployerType = 1 THEN 'Levy' WHEN acc.ApprenticeshipEmployerType = 0 THEN 'NonLevy' ELSE 'Unknown' END AS apprenticeshipEmployerType
        FROM employer_account.MembershipView mv
        JOIN employer_account.Account acc ON mv.HashedAccountId = acc.HashedId";

            var row = await accountsHelper.ExecuteSql(sql);
            if (row == null || row.Count == 0) Assert.Fail("No database row found for linked accounts test.");

            var aliases = new List<string> { "userRef", "Email", "employerUserId", "encodedAccountId", "dasAccountName", "role", "apprenticeshipEmployerType" };
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < aliases.Count; i++) dict[aliases[i]] = i < row.Count ? (row[i] ?? string.Empty) : string.Empty;

            try { _scenarioContext.Set(dict["userRef"], "UserRef"); } catch { _scenarioContext["UserRef"] = dict["userRef"]; }
            try { _scenarioContext.Set(dict["Email"], "email"); } catch { _scenarioContext["email"] = dict["Email"]; }
            try { _scenarioContext.Set(dict["encodedAccountId"], "encodedAccountId"); } catch { _scenarioContext["encodedAccountId"] = dict["encodedAccountId"]; }
        }

        [When(@"endpoint /Accounts/\{accountId\}/users/minimum-signed-agreement-version is called")]
        public async Task WhenEndpointAccountsAccountIdUsersMinimumSignedAgreementVersionIsCalled()
        {
            string accountIdentifier = null;
            try { accountIdentifier = _scenarioContext.ContainsKey("accAgreementSqlRow") ? _scenarioContext.Get<Dictionary<string, string>>("accAgreementSqlRow")["AccountId"] : null; } catch { accountIdentifier = null; }
            if (string.IsNullOrWhiteSpace(accountIdentifier)) accountIdentifier = GetAccountId();

            Assert.IsFalse(string.IsNullOrEmpty(accountIdentifier), "AccountId was not set in context for agreement test");

            // Use the outer API helper which exposes the minimum-signed-agreement-version endpoint for numeric account ids
            if (!long.TryParse(accountIdentifier, out var acctNumeric))
            {
                Assert.Fail("AccountId from SQL was not a valid numeric value for the outer API call.");
                return;
            }

            var response = await _apiHelper.GetAccountMinimumSignedAgreementVersion(acctNumeric);
            var content = response?.Content ?? string.Empty;

            try { _scenarioContext.Set(content, "accAgreementApiResponse"); } catch { _scenarioContext["accAgreementApiResponse"] = content; }
        }

        [Then(@"the response body should contain valid account details signed aggrement details")]
        public void ThenTheResponseBodyShouldContainValidAccountDetailsSignedAggrementDetails()
        {
            // Reuse generic assertion: set expectedResult and actualResult then call AssertApiMatchesSqlRow
            Dictionary<string, string> sqlDict = null;
            try { sqlDict = _scenarioContext.ContainsKey("accAgreementSqlRow") ? _scenarioContext.Get<Dictionary<string, string>>("accAgreementSqlRow") : null; } catch { sqlDict = null; }
            Assert.IsNotNull(sqlDict, "SQL row for agreement was not set in context.");

            // set expectedResult from the agreement SQL row
            try { _scenarioContext.Set(sqlDict, "expectedResult"); } catch { _scenarioContext["expectedResult"] = sqlDict; }

            // get API response content and convert to a JObject for comparison
            string apiContent = null;
            try { apiContent = _scenarioContext.ContainsKey("accAgreementApiResponse") ? _scenarioContext.Get<string>("accAgreementApiResponse") : null; } catch { apiContent = null; }
            Assert.IsFalse(string.IsNullOrWhiteSpace(apiContent), "Agreement API response is empty.");

            Newtonsoft.Json.Linq.JToken parsed = null;
            try { parsed = Newtonsoft.Json.Linq.JToken.Parse(apiContent); } catch { parsed = null; }

            Newtonsoft.Json.Linq.JObject actualObj;
            if (parsed != null && parsed.Type == Newtonsoft.Json.Linq.JTokenType.Object)
            {
                actualObj = (Newtonsoft.Json.Linq.JObject)parsed;
            }
            else
            {
                // wrap scalar or array response into an object with the expected property name
                sqlDict.TryGetValue("minimumSignedAgreementVersion", out var keyVal);
                var propName = "minimumSignedAgreementVersion";
                actualObj = new Newtonsoft.Json.Linq.JObject();
                actualObj[propName] = parsed != null ? parsed : (Newtonsoft.Json.Linq.JToken)apiContent;
            }

            try { _scenarioContext.Set(actualObj, "actualResult"); } catch { _scenarioContext["actualResult"] = actualObj; }

            // delegate assertions to the generic helper
            _stepHelper.AssertApiMatchesSqlRow();
        }


        [Given(@"an employer account with signed version")]
        public async System.Threading.Tasks.Task GivenAnEmployerAccountWithSignedVersion()
            => await _stepHelper.PopulateExpectedSignedAgreementVersion();

        [When(@"endpoint request GET /AccountUsers/\{\{UserRef\}\}/accounts is called")]
        public async System.Threading.Tasks.Task WhenEndpointGetAccountUsersAccountsIsCalled()
        {
            string userRef = null;
            try { userRef = _scenarioContext.ContainsKey("UserRef") ? _scenarioContext.Get<string>("UserRef") : null; } catch { }
            Assert.IsFalse(string.IsNullOrWhiteSpace(userRef), "UserRef was not set in the test setup.");

            var response = await _apiHelper.GetAccountUserAccountsByUserRef(userRef);
            try
            {
                var parsed = JToken.Parse(response.Content);
                JToken ua = null;
                if (parsed.Type == JTokenType.Object)
                {
                    ua = parsed.SelectToken("userAccounts") ?? parsed["userAccounts"];
                }
                else if (parsed.Type == JTokenType.Array)
                {
                    var arr = (JArray)parsed;
                    if (arr.Count > 0 && arr[0].Type == JTokenType.Object)
                    {
                        ua = arr[0].SelectToken("userAccounts") ?? arr[0]["userAccounts"];
                    }
                }

                if (ua == null)
                {
                    _objectContext.Replace("finance_lastResponse", response.Content);
                }
                else
                {
                    _objectContext.Replace("finance_lastResponse", ua.ToString());
                }
            }
            catch (JsonException)
            {
                _objectContext.Replace("finance_lastResponse", response.Content);
            }
        }

        [Then(@"account details should retrun for given user")]
        public async System.Threading.Tasks.Task ThenAccountDetailsShouldRetrunForGivenUser()
        {
            var apiContent = _objectContext.Get<string>("finance_lastResponse") ?? string.Empty;
            Assert.IsNotEmpty(apiContent, "API response content is empty.");

            var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
            Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure BeforeScenario hooks ran.");

            var queryFile = "getAccountUsersByUserId_linked.sql";
            var replacements = new Dictionary<string, string>();
            var propertyOrder = new[] { "userRef", "email", "employerUserId", "encodedAccountId", "dasAccountName", "role", "apprenticeshipEmployerType" };
            var ignoreProps = new[] { "role" };

            await AssertHelper.AssertApiResponseMatchesSql(accountsHelper, apiContent, queryFile, propertyOrder, replacements, ignoreProps);
        }

        private string GetAccountId() => _objectContext.GetAccountId();
    }
}
