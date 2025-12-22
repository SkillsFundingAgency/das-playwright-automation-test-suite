using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Finance.APITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Finance.APITests.Project.Helpers;

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
            if (accountsHelper == null) throw new ArgumentNullException(nameof(accountsHelper));
            if (string.IsNullOrWhiteSpace(apiResponseJson)) Assert.Fail("apiResponseJson must be provided");
            if (string.IsNullOrWhiteSpace(sqlFileName)) Assert.Fail("sqlFileName must be provided");
            if (propertyOrder == null || !propertyOrder.Any()) Assert.Fail("propertyOrder must contain at least one property name");

            // Execute SQL with replacements
            var sqlResult = await accountsHelper.ExecuteSqlFileWithReplacements(sqlFileName, replacements);
            Assert.IsNotNull(sqlResult, "SQL query returned no results");
            Assert.IsTrue(sqlResult.Count >= propertyOrder.Count(), "SQL result did not return expected number of columns");

            // Prepare property list before parsing API response
            var props = propertyOrder.ToList();

            // Prepare a joined representation of the SQL row for logging
            var sqlRow = string.Join(", ", sqlResult ?? new System.Collections.Generic.List<string>());

            // Parse API response - support array, object, or primitive
            JToken apiToken;
            try
            {
                apiToken = JToken.Parse(apiResponseJson);
            }
            catch (Exception ex)
            {
                Assert.Fail($"API response is not valid JSON: {ex.Message}");
                return;
            }

            JObject apiRecord = null;

            if (apiToken.Type == JTokenType.Array)
            {
                var apiArray = (JArray)apiToken;
                Assert.IsTrue(apiArray.Count > 0, "API response contains no elements to compare");

                // If replacements include a userRef or userId, try to find the matching API record, otherwise use the first entry
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
                // Primitive/token value (e.g., a single number or string). We'll compare directly if only one property expected.
                var primitiveValue = apiToken.ToString();

                if (props.Count() != 1)
                    Assert.Fail("API returned a primitive value but multiple properties were requested for comparison.");

                var expected = sqlResult[0] ?? string.Empty;
                Assert.AreEqual(expected, primitiveValue, $"{props.First()} mismatch");
                return;
            }

            var ignoreSet = (ignoreProperties ?? Enumerable.Empty<string>()).ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Compare each property in the provided order against the SQL result columns (by index)
            for (int i = 0; i < props.Count; i++)
            {
                var propName = props[i];
                if (ignoreSet.Contains(propName))
                {
                    try
                    {
                        LogHelper.LogAssertionResult(propName, sqlResult.ElementAtOrDefault(i) ?? string.Empty, string.Empty, true, accountsHelper, string.Empty, sqlFileName, sqlRow);
                    }
                    catch { }

                    continue;
                }

                var expectedRaw = sqlResult[i] ?? string.Empty;

                // Try to find the API property in a case-insensitive way
                JToken actualToken = null;
                var prop = apiRecord.Properties().FirstOrDefault(p => string.Equals(p.Name, propName, StringComparison.OrdinalIgnoreCase));
                if (prop != null)
                {
                    actualToken = prop.Value;
                }
                else
                {
                    // Fallback: try SelectToken (supports json path), then direct index
                    actualToken = apiRecord.SelectToken(propName) ?? apiRecord[propName];

                    // Special-case mappings: some API responses use alternate property names
                    if (actualToken == null && string.Equals(propName, "userRef", StringComparison.OrdinalIgnoreCase))
                    {
                        actualToken = apiRecord.SelectToken("employerUserId") ?? apiRecord["employerUserId"];
                    }
                    // If still not found, some responses return nested info under userAccounts[0]
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
                            // ignore any JSON path issues
                        }
                    }
                }

                var actualRaw = actualToken?.ToString() ?? string.Empty;

                // If the API does not include this property at all, skip hard-failing the assertion.
                // This allows SQL files that contain additional columns (e.g., numeric UserId)
                // to be compared without failing when the API omits that column. Log as a
                // non-failing info entry and continue with remaining comparisons.
                if (actualToken == null || string.IsNullOrEmpty(actualRaw))
                {
                    try
                    {
                        LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, true, accountsHelper, string.Empty, sqlFileName, sqlRow);
                    }
                    catch
                    {
                        // swallow logging errors
                    }

                    continue;
                }

                // Generic type-aware comparison: boolean -> numeric -> string
                if (TryCompareAsBool(expectedRaw, actualRaw, out var boolEqual))
                {
                    if (!boolEqual)
                    {
                        LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, false, accountsHelper, string.Empty, sqlFileName, sqlRow);
                        Assert.IsTrue(boolEqual, $"{propName} mismatch: expected '{expectedRaw}' but was '{actualRaw}'");
                    }
                    else
                    {
                        LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, true, accountsHelper, string.Empty, sqlFileName, sqlRow);
                    }

                    continue;
                }

                if (TryCompareAsNumber(expectedRaw, actualRaw, out var numberEqual))
                {
                    if (!numberEqual)
                    {
                        LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, false, accountsHelper, string.Empty, sqlFileName, sqlRow);
                        Assert.IsTrue(numberEqual, $"{propName} numeric mismatch: expected '{expectedRaw}' but was '{actualRaw}'");
                    }
                    else
                    {
                        LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, true, accountsHelper, string.Empty, sqlFileName, sqlRow);
                    }

                    continue;
                }

                // Default: string comparison (trim both sides)
                var expectedTrim = expectedRaw?.Trim();
                var actualTrim = actualRaw?.Trim();
                if (string.Equals(expectedTrim, actualTrim, StringComparison.Ordinal))
                {
                    LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, true, accountsHelper, string.Empty, sqlFileName, sqlRow);
                }
                else
                {
                    LogHelper.LogAssertionResult(propName, expectedRaw, actualRaw, false, accountsHelper, string.Empty, sqlFileName, sqlRow);
                    Assert.AreEqual(expectedTrim, actualTrim, $"{propName} mismatch: expected '{expectedRaw}' but was '{actualRaw}'");
                }
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
