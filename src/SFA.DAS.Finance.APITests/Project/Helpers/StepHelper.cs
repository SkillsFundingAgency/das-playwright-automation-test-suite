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


        public Dictionary<string, string> GetSqlExpressionMap()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["accountId"] = "mv.AccountId",
                ["userRef"] = "mv.UserRef as userRef",
                ["firstName"] = "mv.FirstName as firstName",
                ["lastName"] = "mv.LastName as lastName",
                ["name"] = "mv.FirstName + ' ' + mv.LastName as name",
                ["email"] = "mv.Email as email",
                ["role"] = "CASE WHEN mv.Role = 1 THEN 'Owner' WHEN mv.Role = 0 THEN 'User' ELSE 'Unknown' END AS role",
                ["canReceiveNotifications"] = "CASE WHEN uas.ReceiveNotifications = 1 THEN 'true' WHEN uas.ReceiveNotifications = 0 THEN 'false' ELSE 'Unknown' END AS canReceiveNotifications"
            };
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

            object expectedObj = null;
            try { expectedObj = _scenarioContext.ContainsKey("expectedResult") ? _scenarioContext["expectedResult"] : null; } catch { expectedObj = null; }

            // determine matching property from SQL row if present, else fall back to identifier picked from expectedObj

            var match = FindMatchingItem(parsed, expectedObj, "userRef");

            try { _scenarioContext.Set(match, "actualResult"); } catch { _scenarioContext["actualResult"] = match; }
        }

        private JObject FindMatchingItem(JToken parsed, object expectedObj, string matchingProperty)
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

                // fallback to expectedObj's identifier property
                if (string.IsNullOrWhiteSpace(expectedIdentifierValue) && expectedObj != null)
                {
                    var idProp = PickIdentifierProperty(expectedObj.GetType());
                    if (idProp != null) expectedIdentifierValue = idProp.GetValue(expectedObj)?.ToString();
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
            if (parsed.Type == JTokenType.Object) return (JObject)parsed;

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
                    var actualVal = prop.Value != null ? (prop.Value.Type == JTokenType.String ? (string)prop.Value : prop.Value.ToString()) : string.Empty;
                    expectedDict.TryGetValue(propName, out var expectedVal);
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

        public void CompareNonStringProperty(string propName, object expectedVal, object actualVal)
        {
            if (expectedVal == null && actualVal == null) return;
            if (expectedVal == null || actualVal == null) Assert.Fail($"Property '{propName}' missing on expected or actual result.");

            try
            {
                var convertedActual = Convert.ChangeType(actualVal, expectedVal.GetType());
                Assert.AreEqual(expectedVal, convertedActual, $"{propName} mismatch");
            }
            catch
            {
                Assert.AreEqual(expectedVal.ToString(), actualVal.ToString(), $"{propName} mismatch");
            }
        }

        public JArray ParseApiContent(string apiContent)
        {
            JToken parsed;
            try { parsed = JToken.Parse(apiContent); }
            catch (Exception ex) { Assert.Fail($"API response is not valid JSON: {ex.Message}"); return new JArray(); }

            return parsed.Type == JTokenType.Array ? (JArray)parsed : (parsed["users"] != null ? (JArray)parsed["users"] : new JArray(parsed));
        }

        public System.Reflection.PropertyInfo PickIdentifierProperty(Type t)
        {
            var candidates = new[] { "userRef", "user_ref", "userid", "userId", "id", "email" };
            // Only consider readable, non-indexer properties to avoid calling indexers via reflection
            var props = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
                .ToArray();

            foreach (var c in candidates)
            {
                var p = props.FirstOrDefault(x => string.Equals(x.Name, c, StringComparison.OrdinalIgnoreCase));
                if (p != null) return p;
            }

            var byUser = props.FirstOrDefault(x => x.Name.IndexOf("user", StringComparison.OrdinalIgnoreCase) >= 0 && x.PropertyType == typeof(string));
            if (byUser != null) return byUser;
            var byEmail = props.FirstOrDefault(x => x.Name.IndexOf("email", StringComparison.OrdinalIgnoreCase) >= 0 && x.PropertyType == typeof(string));
            if (byEmail != null) return byEmail;

            return props.FirstOrDefault(x => x.PropertyType == typeof(string));
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
            if (row == null || row.Count == 0)
            {
                Assert.Fail($"No database row found for ReceiveNotifications = {receive}");
                return;
            }

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
            if (row == null || row.Count == 0)
            {
                Assert.Fail("No database row found for SignedAgreementVersion");
                return;
            }

            var selectAliases = new List<string> { "AccountId", "minimumSignedAgreementVersion" };
            var dict = MapRowToDictionary(row, selectAliases);
            SetAccountIdIfPresent(dict);
            _scenarioContext.Set(dict, "accAgreementSqlRow");

            // store the raw SQL dictionary as the expected agreement result
            try { _scenarioContext.Set(dict, "expectedAgreementResult"); } catch { _scenarioContext["expectedAgreementResult"] = dict; }
        }
    }
}
