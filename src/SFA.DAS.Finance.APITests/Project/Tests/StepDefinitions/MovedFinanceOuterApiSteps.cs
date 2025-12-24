using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SFA.DAS.Finance.APITests.Project.Helpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class MovedFinanceOuterApiSteps
    {
        private readonly Inner_EmployerFinanceApiRestClient _innerApiRestClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly ObjectContext _objectContext;
        private readonly Outer_EmployerFinanceApiHelper _apiHelper;
        private readonly StepHelper _stepHelper;

        public MovedFinanceOuterApiSteps(ScenarioContext context)
        {
            _scenarioContext = context;
            _innerApiRestClient = context.GetRestClient<Inner_EmployerFinanceApiRestClient>();
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




        [Given(@"an employer account with signed version")]
        public async System.Threading.Tasks.Task GivenAnEmployerAccountWithSignedVersion() => await _stepHelper.PopulateExpectedSignedAgreementVersion();

        [When(@"endpoint /Accounts/\{accountId\}/users/minimum-signed-agreement-version is called")]
        public async Task WhenEndpointAccountsAccountIdUsersMinimumSignedAgreementVersionIsCalled() => await _stepHelper.CallAccountIdUsersMinimumSignedAgreement();

        [Then(@"the response body should contain valid account details signed aggrement details")]
        public void ThenTheResponseBodyShouldContainValidAccountDetailsSignedAggrementDetails() => Task.Run(() => _stepHelper.AssertApiMatchesSqlRow());
        



        [Given(@"get the user with linked accounts")]
        public async System.Threading.Tasks.Task GetUserWithLinkedAccounts()
        {
            await _stepHelper.PopulateExpectedUserWithLinkedAccounts();
        }

        [When(@"endpoint request GET /AccountUsers/\{\{UserRef\}\}/accounts is called")]
        public async System.Threading.Tasks.Task WhenEndpointGetAccountUsersAccountsIsCalled()  => await _stepHelper.CallGetAccountUserAccountsByUserRef();
        

        [Then(@"account details should retrun for given user")]
        public void ThenAccountDetailsShouldRetrunForGivenUser() => Task.Run(() => _stepHelper.AssertApiMatchesSqlRow());




        [Given(@"remove the existing period end data from DB")]
        public async Task GivenRemoveTheExistingPeriodEndDataFromDB()
        {
            var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();

            try
            {
                var result = await accountsHelper.ExecuteSqlFileWithReplacements("removePeriodEnds.sql", new Dictionary<string, string>());
                try { TestContext.Progress.WriteLine($"[DEBUG] PeriodEnds cleanup executed, returned {result?.Count ?? 0} rows."); } catch { }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to execute period-ends cleanup SQL: {ex.Message}");
            }
        }

        [When(@"period end details are posted via endpoint: api/period-ends")]
        public async System.Threading.Tasks.Task WhenSendAnApiRequestPOSTApiPeriodEnds()
        {
            // var periodendId = "1819-R017654";
            var innerApiClient = _scenarioContext.GetRestClient<Inner_EmployerFinanceApiRestClient>();

            // Read the period end payload template file and pass content directly
            var payloadRelative = Path.Combine("Project", "Tests", "Payload", "PeriodEndTemplate.json");
            var payloadFull = Path.Combine(FileHelper.GetLocalProjectRootFilePath(), payloadRelative);
            string payloadContent;
            try
            {
                payloadContent = File.ReadAllText(payloadFull);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to read payload file '{payloadFull}': {ex.Message}");
                return;
            }

            var response = await innerApiClient.PostPeriodEnds(payloadContent);
            try { _objectContext.Replace("finance_lastResponse", response.Content); } catch { _objectContext.SetDebugInformation("Failed to save finance_lastResponse"); }
        }

        [Then(@"its details can be accessed via endpoint: api/period-ends")]
        public async Task ThenItsDetailsCanBeAccessedViaEndpointApiPeriodEnds()
        {
            var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
            try
            {
                await accountsHelper.ExecuteSqlFileWithReplacements("getPeriodEnds.sql", new Dictionary<string, string>());
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to execute SQL file getPeriodEnds.sql: {ex.Message}");
                return;
            }

            var response = await _innerApiRestClient.ExecuteEndpoint("/api/period-ends");
            var content = response?.Content ?? string.Empty;

            JArray arr;
            try
            {
                arr = JArray.Parse(content);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to parse /api/period-ends response as JSON array: {ex.Message}");
                return;
            }

            var match = arr.FirstOrDefault(x => string.Equals((string)x["periodEndId"], "1819-R017654", StringComparison.OrdinalIgnoreCase));
            if (match == null)
            {
                Assert.Fail("No period end with PeriodEndId '1819-R017654' was found in the /api/period-ends response.");
                return;
            }

            try
            {
                _objectContext.Replace("finance_lastResponse", match.ToString());
            }
            catch
            {
                // ignore context replace failures
            }
        }

        [Then(@"its details can be accessed via endpoint: api/period-ends/{periodEndId}")]
        public async Task ThenItsDetailsCanBeAccessedViaEndpointApiPeriodEndById()
        {
            var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
            try
            {
                await accountsHelper.ExecuteSqlFileWithReplacements("getPeriodEnd.sql", new Dictionary<string, string>());
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to execute SQL file getPeriodEnd.sql: {ex.Message}");
                return;
            }

            var response = await _innerApiRestClient.ExecuteEndpoint("/api/period-ends/1819-R017654");
            var content = response?.Content ?? string.Empty;


            if (content == null)
            {
                Assert.Fail("No period end with PeriodEndId '1819-R017654' was found in the /api/period-ends response.");
                return;
            }

            try
            {
                _objectContext.Replace("finance_lastResponse", content);
            }
            catch
            {
                // ignore context replace failures
            }
        }

        [Then(@"the posted period-end matches the payload")]
        public void ThenThePostedPeriodEndMatchesThePayload()
        {
            var payloadPath = Path.Combine(FileHelper.GetLocalProjectRootFilePath(), "Project", "Tests", "Payload", "PeriodEndTemplate.json");
            string payloadContent;
            payloadContent = File.ReadAllText(payloadPath);
            JArray payloadArr = JArray.Parse(payloadContent);
            var expected = payloadArr.Count > 0 ? payloadArr[0] : (JToken)payloadArr;

            var actualStr = string.Empty;
            try { actualStr = _objectContext.Get<string>("finance_lastResponse"); } catch { }
            if (string.IsNullOrWhiteSpace(actualStr))
            {
                Assert.Fail("finance_lastResponse was not set or was empty when comparing to payload.");
                return;
            }

            JToken actual;
            try
            {
                actual = JToken.Parse(actualStr);
                if (actual.Type == JTokenType.Array && actual is JArray actualArr && actualArr.Count == 1)
                {
                    actual = actualArr[0];
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"finance_lastResponse is not valid JSON: {ex.Message}");
                return;
            }

            if (!JToken.DeepEquals(expected, actual))
            {
                Assert.Fail($"Posted period-end does not match payload.\nExpected: {expected}\nActual: {actual}");
            }
        }

        private string GetAccountId() => _objectContext.GetAccountId();
    }
}
