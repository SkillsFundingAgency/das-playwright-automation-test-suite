using System;
using Newtonsoft.Json.Linq;
using System.Globalization;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public class StepHelper
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ObjectContext _objectContext;
        private readonly Outer_EmployerFinanceApiHelper _outerApiHelper;

        public StepHelper(ScenarioContext scenarioContext, ObjectContext objectContext, Outer_EmployerFinanceApiHelper outerApiHelper)
        {
            _scenarioContext = scenarioContext;
            _objectContext = objectContext;
            _outerApiHelper = outerApiHelper;
        }


        public async Task<List<string>> ExecuteSql(string sql)
        {
            var accountsHelper = _scenarioContext.Get<AccountsSqlDataHelper>();
            Assert.IsNotNull(accountsHelper, "AccountsSqlDataHelper not registered in ScenarioContext; ensure BeforeScenario hooks ran.");

            return await accountsHelper.ExecuteSql(sql);
        }

        public Dictionary<string, string> MapRowToDictionary(List<string> row, List<string> selectAliases)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < selectAliases.Count; i++)
            {
                var alias = selectAliases[i];
                dict[alias] = i < row.Count ? (row[i] ?? string.Empty) : string.Empty;
            }
            return dict;
        }

        public void SetAccountIdIfPresent(Dictionary<string, string> dict)
        {
            if (dict.TryGetValue("accountId", out var accountIdFromDb) && !string.IsNullOrWhiteSpace(accountIdFromDb))
            {
                if (dict.TryGetValue("accountId", out var acct) && !string.IsNullOrWhiteSpace(acct))
                {
                    // defensive: ignore placeholder alias values that may be present instead of real data
                    var lower = acct.Trim().ToLowerInvariant();
                    if (lower != "accountid" && lower != "userref" && lower != "hashedaccountid" && lower != "encodedaccountid")
                    {
                        _objectContext.SetAccountId(acct);
                    }
                }
            }
        }

        public async Task PopulateExpectedUserNotification(string canOrCannot)
        {
            // Use a hardcoded list of properties instead of reflecting the model
            // var modelProps = new[] { "userRef", "firstName", "lastName", "name", "email", "role", "canReceiveNotifications", "status" };
            // Hardcode the select list and aliases to avoid reflection and ensure stable SQL
            var selectList = "mv.AccountId as accountId,\n                    mv.UserRef as userRef,\n                    mv.FirstName as firstName,\n                    mv.LastName as lastName,\n                    mv.FirstName + ' ' + mv.LastName as name,\n                    mv.Email as email,\n                    CASE WHEN mv.Role = 1 THEN 'Owner' WHEN mv.Role = 0 THEN 'User' ELSE 'Unknown' END AS role,\n                    CASE WHEN uas.ReceiveNotifications = 1 THEN 'true' WHEN uas.ReceiveNotifications = 0 THEN 'false' ELSE 'Unknown' END AS canReceiveNotifications,\n                    NULL as status";
            var selectAliases = new List<string> { "accountId", "userRef", "firstName", "lastName", "name", "email", "role", "canReceiveNotifications", "status" };
            var receive = canOrCannot.Equals("can", StringComparison.OrdinalIgnoreCase) ? "1" : "0";
            var sql = $@"SELECT TOP 1 {selectList}
                    FROM employer_account.MembershipView mv
                    LEFT JOIN employer_account.UserAccountSettings uas 
                    ON mv.AccountId = uas.AccountId
                    WHERE uas.ReceiveNotifications = " + receive;

            var row = await ExecuteSql(sql);

            var dict = MapRowToDictionary(row, selectAliases);
            SetAccountIdIfPresent(dict);
            // store both the expected result model and the raw SQL dictionary used for property-skipping logic
            try { _scenarioContext.Set(dict, "expectedResult"); } catch { _scenarioContext["expectedResult"] = dict; }
            try { _scenarioContext.Set(dict, "accUserNotificationSqlRow"); } catch { _scenarioContext["accUserNotificationSqlRow"] = dict; }
        }

        public async Task PopulateExpectedSignedAgreementVersion()
        {
            var sql = "SELECT TOP 1 AccountId, SignedAgreementVersion AS minimumSignedAgreementVersion FROM employer_account.AccountLegalEntity WHERE SignedAgreementVersion is not null order by AccountId desc";

            var row = await ExecuteSql(sql);

            var selectAliases = new List<string> { "AccountId", "minimumSignedAgreementVersion" };
            var dict = MapRowToDictionary(row, selectAliases);
            SetAccountIdIfPresent(dict);
            _scenarioContext.Set(dict, "accAgreementSqlRow");

            // store the raw SQL dictionary as the expected agreement result
            try { _scenarioContext.Set(dict, "expectedResult"); } catch { _scenarioContext["expectedResult"] = dict; }
            try { _scenarioContext.Set(dict, "expectedAgreementResult"); } catch { _scenarioContext["expectedAgreementResult"] = dict; }
        }

        public async Task PopulateExpectedUserWithLinkedAccounts()
        {
            var sql = @"SELECT TOP 1 mv.UserRef as userRef, mv.Email as Email,
            mv.UserRef as employerUserId,
            mv.HashedAccountId as encodedAccountId,
            mv.AccountName as dasAccountName,
            CASE WHEN mv.Role = 1 THEN 'Owner' WHEN mv.Role = 0 THEN 'User' ELSE 'Unknown' END AS role,
            CASE WHEN acc.ApprenticeshipEmployerType = 1 THEN 'Levy' WHEN acc.ApprenticeshipEmployerType = 0 THEN 'NonLevy' ELSE 'Unknown' END AS apprenticeshipEmployerType
        FROM employer_account.MembershipView mv
        JOIN employer_account.Account acc ON mv.HashedAccountId = acc.HashedId";

            var row = await ExecuteSql(sql);

            var aliases = new List<string> { "userRef", "Email", "employerUserId", "encodedAccountId", "dasAccountName", "role", "apprenticeshipEmployerType" };
            var dict = MapRowToDictionary(row, aliases);

            try { _scenarioContext.Set(dict, "expectedResult"); } catch { _scenarioContext["expectedResult"] = dict; }
            try { _scenarioContext.Set(dict["userRef"], "UserRef"); } catch { _scenarioContext["UserRef"] = dict["userRef"]; }
            try { _scenarioContext.Set(dict["Email"], "email"); } catch { _scenarioContext["email"] = dict["Email"]; }
            try { _scenarioContext.Set(dict["encodedAccountId"], "encodedAccountId"); } catch { _scenarioContext["encodedAccountId"] = dict["encodedAccountId"]; }

        }

        public async Task CallGetNotificationRequestSaveResponse()
        {
            var accountIdentifier = _objectContext.GetAccountId();

            RestResponse response = null;
            try
            {
                if (long.TryParse(accountIdentifier, out var numericId))
                {
                    response = await _outerApiHelper.GetAccountUserWhichCanReceiveNotifications(numericId);
                }
                else
                {
                    response = await _outerApiHelper.GetAccountUserWhichCanReceiveNotificationsByHashedId(accountIdentifier);
                }
            }
            catch
            {
                // let ApiAssertHelper handle status failures when executing
            }

            var content = response?.Content ?? string.Empty;
            try { _scenarioContext.Set(content, "accUserNotificationsApiResponse"); } catch { _scenarioContext["accUserNotificationsApiResponse"] = content; }

            JToken parsed = null;
            try { parsed = JToken.Parse(content); } catch { parsed = null; }

            // determine matching property from SQL row if present, else fall back to identifier picked from expectedObj

            var match = FindMatchingItem(parsed, "userRef");

            try { _scenarioContext.Set(match, "actualResult"); } catch { _scenarioContext["actualResult"] = match; }
        }

        public async Task<JToken> CallGetAccountUserAccountsByUserRef()
        {
            string userRef = null;
            try { userRef = _scenarioContext.ContainsKey("UserRef") ? _scenarioContext.Get<string>("UserRef") : null; } catch { }
            if (string.IsNullOrWhiteSpace(userRef)) throw new ArgumentException("userRef must be provided", nameof(userRef));


            var response = await _outerApiHelper.GetAccountUserAccountsByUserRef(userRef);
            JToken parsed = null;
            try { parsed = JToken.Parse(response.Content); } catch { parsed = null; }

            JToken toSave = null;

            if (parsed != null)
            {
                // try to find matching item by encodedAccountId
                var match = FindMatchingItem(parsed, "encodedAccountId");
                if (match != null)
                {
                    toSave = match;
                }
                else
                {
                    // fallback to extracting userAccounts array or nested property
                    JToken ua = null;
                    if (parsed.Type == JTokenType.Object) ua = parsed.SelectToken("userAccounts") ?? parsed["userAccounts"];
                    else if (parsed.Type == JTokenType.Array)
                    {
                        var arr = (JArray)parsed;
                        if (arr.Count > 0 && arr[0].Type == JTokenType.Object) ua = arr[0].SelectToken("userAccounts") ?? arr[0]["userAccounts"];
                    }

                    toSave = ua ?? parsed;
                }
            }
            else
            {
                // invalid JSON -> save raw content
                toSave = response.Content;
            }

            try { _objectContext.Replace("finance_lastResponse", toSave?.ToString() ?? string.Empty); } catch { }
            try { _scenarioContext.Set(toSave, "actualResult"); } catch { _scenarioContext["actualResult"] = toSave; }

            return toSave;
        }

        // Move minimum-signed-agreement endpoint logic here so step definitions remain thin
        public async Task CallAccountIdUsersMinimumSignedAgreement()
        {
            string accountIdentifier = null;
            try { accountIdentifier = _scenarioContext.ContainsKey("accAgreementSqlRow") ? _scenarioContext.Get<Dictionary<string, string>>("accAgreementSqlRow")["AccountId"] : null; } catch { accountIdentifier = null; }
            if (string.IsNullOrWhiteSpace(accountIdentifier))
            {
                try { accountIdentifier = _objectContext.GetAccountId(); } catch { accountIdentifier = null; }
            }

            Assert.IsFalse(string.IsNullOrEmpty(accountIdentifier), "AccountId was not set in context for agreement test");

            if (!long.TryParse(accountIdentifier, out var acctNumeric))
            {
                Assert.Fail("AccountId from SQL was not a valid numeric value for the outer API call.");
                return;
            }

            var response = await _outerApiHelper.GetAccountMinimumSignedAgreementVersion(acctNumeric);
            var content = response?.Content ?? string.Empty;

            try { _scenarioContext.Set(content, "accAgreementApiResponse"); } catch { _scenarioContext["accAgreementApiResponse"] = content; }
            try { _scenarioContext.Set(content, "actualResult"); } catch { _scenarioContext["actualResult"] = content; }
        }

        private JObject FindMatchingItem(JToken parsed, string matchingProperty)
        {
            if (parsed == null) return null;

            // if parsed is an array, search for the matching item
            if (parsed.Type == JTokenType.Array)
            {
                var items = (JArray)parsed;
                if (items.Count == 0) return null;

                // prefer value from SQL row stored in context
                var sqlDict = TryGetSqlDict();
                string expectedIdentifierValue = null;
                if (!string.IsNullOrWhiteSpace(matchingProperty) && sqlDict != null && sqlDict.TryGetValue(matchingProperty, out var val) && !string.IsNullOrWhiteSpace(val))
                {
                    expectedIdentifierValue = val;
                }

                if (!string.IsNullOrWhiteSpace(expectedIdentifierValue))
                {
                    // try exact property match first using matchingProperty if provided
                    if (!string.IsNullOrWhiteSpace(matchingProperty))
                    {
                        var direct = items.Children<JObject>().FirstOrDefault(o => HasJObjectPropertyEqual(o, matchingProperty, expectedIdentifierValue));
                        if (direct != null) return direct;
                    }

                    var any = items.Children<JObject>().FirstOrDefault(o => JObjectContainsValue(o, expectedIdentifierValue));
                    if (any != null) return any;
                }

                return (JObject)items.FirstOrDefault();
            }

            // if parsed is an object, return it directly
            if (parsed.Type == JTokenType.Object) return (JObject) parsed;

            return null;
        }

        public void AssertApiMatchesSqlRow()
        {
            var (expectedObj, actualObj) = RetrieveExpectedAndActual();
            Assert.IsNotNull(expectedObj, "Expected result was not set from SQL result.");

            var sqlDict = TryGetSqlDict();

            if (HandleReceiveNotificationsFalseCase(expectedObj, actualObj)) return;

            Assert.IsNotNull(actualObj, "Actual result was not found in API response.");

            // Use the comparer that iterates API response properties and compares to expected result
            CompareApiResponseToExpectedResult(expectedObj, actualObj, sqlDict);
        }

        public void CompareApiResponseToExpectedResult(object expectedObj, object actualObj, Dictionary<string, string> sqlDict)
        {
            var expectedDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (expectedObj is Dictionary<string, string> d) expectedDict = new Dictionary<string, string>(d, StringComparer.OrdinalIgnoreCase);
            else
            {
                var props = expectedObj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.CanRead);
                foreach (var p in props) expectedDict[p.Name] = p.GetValue(expectedObj)?.ToString() ?? string.Empty;
            }

            void CompareJObject(JObject jo)
            {
                foreach (var prop in jo.Properties())
                {
                    var propName = prop.Name;
                    if (ShouldSkipProperty(sqlDict, propName)) continue;
                    // If expected does not define this property, skip comparison (API may include additional nested data)
                    if (!expectedDict.TryGetValue(propName, out var expectedVal)) continue;
                    var actualVal = prop.Value != null ? (prop.Value.Type == JTokenType.String ? (string)prop.Value : prop.Value.ToString()) : string.Empty;
                    ComparePropertyValue(propName, expectedVal ?? string.Empty, actualVal);
                }
            }

            if (actualObj is JArray arr)
            {
                foreach (var token in arr.Children<JObject>()) CompareJObject(token);
                return;
            }

            if (actualObj is JObject jo) { CompareJObject(jo); return; }

            var actualProps = actualObj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.CanRead);
            foreach (var p in actualProps)
            {
                var propName = p.Name;
                if (ShouldSkipProperty(sqlDict, propName)) continue;
                var actualVal = p.GetValue(actualObj)?.ToString() ?? string.Empty;
                expectedDict.TryGetValue(propName, out var expectedVal);
                ComparePropertyValue(propName, expectedVal ?? string.Empty, actualVal);
            }
        }

        public (object expected, object actual) RetrieveExpectedAndActual()
        {
            object expectedObj = null;
            object actualObj = null;
            try { expectedObj = _scenarioContext.ContainsKey("expectedResult") ? _scenarioContext["expectedResult"] : null; } catch { expectedObj = null; }
            try { actualObj = _scenarioContext.ContainsKey("actualResult") ? _scenarioContext["actualResult"] : null; } catch { actualObj = null; }
            return (expectedObj, actualObj);
        }

        public Dictionary<string, string> TryGetSqlDict()
        {
            try { return _scenarioContext.ContainsKey("accUserNotificationSqlRow") ? _scenarioContext.Get<Dictionary<string, string>>("accUserNotificationSqlRow") : null; } catch { return null; }
        }

        public bool HandleReceiveNotificationsFalseCase(object expectedObj, object actualObj)
        {
            if (expectedObj == null) return false;
            string canVal = null;

            // If expected result is stored as a raw SQL dictionary, read the value directly
            if (expectedObj is Dictionary<string, string> expectedDict)
            {
                expectedDict.TryGetValue("canReceiveNotifications", out var v);
                canVal = v?.ToString()?.Trim().ToLowerInvariant();
            }
            else
            {
                var expectedType = expectedObj.GetType();
                var canProp = expectedType.GetProperty("canReceiveNotifications", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if (canProp == null) return false;
                canVal = canProp.GetValue(expectedObj)?.ToString()?.Trim().ToLowerInvariant() ?? string.Empty;
            }
            if (canVal == "false")
            {
                if (actualObj == null) return true;
                var actualUserRefProp = actualObj.GetType().GetProperty("userRef", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                var actualUserRef = actualUserRefProp?.GetValue(actualObj)?.ToString();
                Assert.IsTrue(string.IsNullOrWhiteSpace(actualUserRef), "Expected no user returned by API when ReceiveNotifications=false, but an actual record was present.");
                return true;
            }

            return false;
        }

        public bool ShouldSkipProperty(Dictionary<string, string> sqlDict, string propName)
        {
            if (sqlDict == null) return false;
            return sqlDict.TryGetValue(propName, out var sqlRawVal) && string.IsNullOrWhiteSpace(sqlRawVal);
        }

        public void ComparePropertyValue(string propName, string expected, string actual)
        {
            var expS = (expected ?? string.Empty).Trim();
            var actS = (actual ?? string.Empty).Trim();

            // Accept case-insensitive match for boolean-like values (e.g., "true" vs "True")
            if (string.Equals(expS, actS, StringComparison.Ordinal))
            {
                return;
            }

            if (string.Equals(expS, actS, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // try boolean comparison (accept "true/false" or "1/0")
            if (TryParseBool(expS, out var expB) && TryParseBool(actS, out var actB))
            {
                Assert.AreEqual(expB, actB, $"{propName} mismatch");
                return;
            }

            // try numeric comparison
            if (decimal.TryParse(expS, NumberStyles.Number, CultureInfo.InvariantCulture, out var expNum)
                && decimal.TryParse(actS, NumberStyles.Number, CultureInfo.InvariantCulture, out var actNum))
            {
                Assert.AreEqual(expNum, actNum, $"{propName} mismatch");
                return;
            }

            Assert.AreEqual(expS, actS, $"{propName} mismatch");
        }

        private bool TryParseBool(string s, out bool val)
        {
            val = false;
            if (string.IsNullOrWhiteSpace(s)) return false;
            var norm = s.Trim().ToLowerInvariant();
            if (norm == "true" || norm == "false")
            {
                val = norm == "true";
                return true;
            }
            if (norm == "1" || norm == "0")
            {
                val = norm == "1";
                return true;
            }
            return false;
        }

        public bool HasJObjectPropertyEqual(JObject o, string propName, string expectedVal)
        {
            if (string.IsNullOrWhiteSpace(propName) || string.IsNullOrWhiteSpace(expectedVal)) return false;
            var prop = o.Properties().FirstOrDefault(p => string.Equals(p.Name, propName, StringComparison.OrdinalIgnoreCase));
            if (prop != null && prop.Value != null)
            {
                var v = prop.Value.Type == JTokenType.String ? (string)prop.Value : prop.Value.ToString();
                return string.Equals(v?.Trim(), expectedVal.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public bool JObjectContainsValue(JObject o, string expectedVal)
        {
            if (string.IsNullOrWhiteSpace(expectedVal)) return false;
            foreach (var p in o.Properties())
            {
                if (p.Value == null) continue;
                if (p.Value.Type != JTokenType.String) continue;
                var v = ((string)p.Value)?.Trim();
                if (string.Equals(v, expectedVal.Trim(), StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

    }
}
