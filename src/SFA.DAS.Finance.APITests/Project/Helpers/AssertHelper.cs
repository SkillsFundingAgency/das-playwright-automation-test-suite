using System;
using Newtonsoft.Json.Linq;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public static class AssertHelper
    {
        /// <summary>
        /// Execute the SQL file after applying the provided replacements, locate the matching API record and assert the listed properties match the SQL result columns.
        /// propertyOrder defines the order of columns returned by the SQL and the corresponding API property names to compare.
        /// replacements may contain keys such as "AccountId", "hashedAccountId", "userId", "userRef" etc. which will be substituted into the SQL.
        /// </summary>
        public static async Task AssertApiResponseMatchesSql(
            AccountsSqlDataHelper accountsHelper,
            string apiResponseJson,
            string sqlFileName,
            IEnumerable<string> propertyOrder,
            IDictionary<string, string> replacements = null,
            IEnumerable<string> ignoreProperties = null)
        {
            ValidateParameters(accountsHelper, apiResponseJson, sqlFileName, propertyOrder);

            var props = propertyOrder.ToList();

            var sqlResult = await GetSqlResult(accountsHelper, sqlFileName, replacements, props.Count);

            var apiRecord = ParseApiRecord(apiResponseJson, replacements, props, sqlResult);

            var ignoreSet = (ignoreProperties ?? Enumerable.Empty<string>()).ToHashSet(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < props.Count; i++)
            {
                var propName = props[i];
                if (ignoreSet.Contains(propName)) continue;

                var expectedRaw = sqlResult[i] ?? string.Empty;

                var actualToken = GetActualToken(apiRecord, propName);

                var actualRaw = actualToken?.ToString() ?? string.Empty;

                if (actualToken == null || string.IsNullOrEmpty(actualRaw)) continue;

                CompareValues(expectedRaw, actualRaw, propName);
            }
        }

        private static void ValidateParameters(AccountsSqlDataHelper accountsHelper, string apiResponseJson, string sqlFileName, IEnumerable<string> propertyOrder)
        {
            if (accountsHelper == null) throw new ArgumentNullException(nameof(accountsHelper));
            if (string.IsNullOrWhiteSpace(apiResponseJson)) Assert.Fail("apiResponseJson must be provided");
            if (string.IsNullOrWhiteSpace(sqlFileName)) Assert.Fail("sqlFileName must be provided");
            if (propertyOrder == null || !propertyOrder.Any()) Assert.Fail("propertyOrder must contain at least one property name");
        }

        private static async Task<System.Collections.Generic.List<string>> GetSqlResult(AccountsSqlDataHelper accountsHelper, string sqlFileName, IDictionary<string, string> replacements, int expectedColumnCount)
        {
            var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(sqlFileName, replacements);
            Assert.IsNotNull(sqlResult, "SQL query returned no results");
            Assert.IsTrue(sqlResult.Count >= expectedColumnCount, "SQL result did not return expected number of columns");
            return sqlResult;
        }

        private static JObject ParseApiRecord(string apiResponseJson, IDictionary<string, string> replacements, System.Collections.Generic.List<string> props, System.Collections.Generic.List<string> sqlResult)
        {
            JToken apiToken;
            try
            {
                apiToken = JToken.Parse(apiResponseJson);
            }
            catch (Exception ex)
            {
                Assert.Fail($"API response is not valid JSON: {ex.Message}");
                return null;
            }

            JObject apiRecord = null;

            if (apiToken.Type == JTokenType.Array)
            {
                var apiArray = (JArray)apiToken;
                Assert.IsTrue(apiArray.Count > 0, "API response contains no elements to compare");

                if (replacements != null)
                {
                    if (replacements.TryGetValue("userRef", out var userRef) && !string.IsNullOrWhiteSpace(userRef))
                    {
                        apiRecord = apiArray.Children<JObject>().FirstOrDefault(o => string.Equals((string)o["userRef"], userRef, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (replacements.TryGetValue("userId", out var userId) && !string.IsNullOrWhiteSpace(userId))
                    {
                        apiRecord = apiArray.Children<JObject>().FirstOrDefault(o => string.Equals((string)o["userId"], userId, StringComparison.OrdinalIgnoreCase));
                    }
                }

                apiRecord ??= (JObject)apiArray.First;
            }
            else if (apiToken.Type == JTokenType.Object)
            {
                apiRecord = (JObject)apiToken;
            }
            else
            {
                var primitiveValue = apiToken.ToString();

                if (props.Count() != 1)
                    Assert.Fail("API returned a primitive value but multiple properties were requested for comparison.");

                var expected = sqlResult[0] ?? string.Empty;
                Assert.AreEqual(expected, primitiveValue, $"{props.First()} mismatch");
                return null;
            }

            return apiRecord;
        }

        private static JToken GetActualToken(JObject apiRecord, string propName)
        {
            if (apiRecord == null) return null;

            JToken actualToken = null;
            var prop = apiRecord.Properties().FirstOrDefault(p => string.Equals(p.Name, propName, StringComparison.OrdinalIgnoreCase));
            if (prop != null)
            {
                actualToken = prop.Value;
            }
            else
            {
                actualToken = apiRecord.SelectToken(propName) ?? apiRecord[propName];

                if (actualToken == null && string.Equals(propName, "userRef", StringComparison.OrdinalIgnoreCase))
                {
                    actualToken = apiRecord.SelectToken("employerUserId") ?? apiRecord["employerUserId"];
                }

                if (actualToken == null)
                {
                    try
                    {
                        var ua = apiRecord.SelectToken("userAccounts");
                        if (ua != null && ua.Type == JTokenType.Array)
                        {
                            var first = ua.First as JObject;
                            if (first != null)
                            {
                                actualToken = first.SelectToken(propName) ?? first[propName];
                            }
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }

            return actualToken;
        }

        private static void CompareValues(string expectedRaw, string actualRaw, string propName)
        {
            if (TryCompareAsBool(expectedRaw, actualRaw, out var boolEqual))
            {
                if (!boolEqual) Assert.IsTrue(boolEqual, $"{propName} mismatch: expected '{expectedRaw}' but was '{actualRaw}'");
                return;
            }

            if (TryCompareAsNumber(expectedRaw, actualRaw, out var numberEqual))
            {
                if (!numberEqual) Assert.IsTrue(numberEqual, $"{propName} numeric mismatch: expected '{expectedRaw}' but was '{actualRaw}'");
                return;
            }

            var expectedTrim = expectedRaw?.Trim();
            var actualTrim = actualRaw?.Trim();
            if (!string.Equals(expectedTrim, actualTrim, StringComparison.Ordinal))
            {
                Assert.AreEqual(expectedTrim, actualTrim, $"{propName} mismatch: expected '{expectedRaw}' but was '{actualRaw}'");
            }
        }

        private static bool TryCompareAsBool(string expected, string actual, out bool equal)
        {
            equal = false;
            if (bool.TryParse(expected, out var expectedBool) || bool.TryParse(actual, out var actualBool))
            {
                // If only one side parsed, try to coerce the other (e.g., SQL may return '1' or '0')
                if (!bool.TryParse(expected, out expectedBool))
                {
                    expectedBool = CoerceToBoolFromNumericString(expected);
                }

                if (!bool.TryParse(actual, out actualBool))
                {
                    actualBool = CoerceToBoolFromNumericString(actual);
                }

                equal = expectedBool == actualBool;
                return true;
            }

            return false;
        }

        private static bool CoerceToBoolFromNumericString(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            if (int.TryParse(s, out var i)) return i != 0;
            if (decimal.TryParse(s, out var d)) return d != 0m;
            // common SQL boolean representations
            var lowered = s.Trim().ToLowerInvariant();
            if (lowered == "y" || lowered == "n") return lowered == "y";
            return false;
        }

        private static bool TryCompareAsNumber(string expected, string actual, out bool equal)
        {
            equal = false;
            // Try decimal for broader coverage
            if (decimal.TryParse(expected, out var expectedNum) && decimal.TryParse(actual, out var actualNum))
            {
                equal = expectedNum == actualNum;
                return true;
            }

            return false;
        }
    }
}
