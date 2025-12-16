using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;

namespace SFA.DAS.Finance.APITests.Project.Helpers
{
    public static class LogHelper
    {
        public static void LogAssertion(string message)
        {
            try
            {
                // Intentionally left blank to avoid printing to console or test progress.
                // Assertions and messages are persisted to file only via LogAssertionResult.
            }
            catch
            {
                // Swallow any logging errors - logging must not break tests
            }
        }

        /// <summary>
        /// Log an assertion result and attempt to persist it into the framework TestAttachments directory (if available).
        /// The optional <paramref name="contextSource"/> is inspected via reflection for an ObjectContext which contains the directory.
        /// </summary>
        public static void LogAssertionResult(string propName, string expected, string actual, bool equal, object contextSource = null, string endpoint = "", string query = "", string sqlRow = "")
        {
            var status = equal ? "PASS" : "FAIL";
            var baseMsg = equal
                ? $"ASSERT {status}: {propName} -> expected '{expected}' == actual '{actual}'"
                : $"ASSERT {status}: {propName} -> expected '{expected}' != actual '{actual}'";

            // Keep assertion lines concise: include only the assertion result and optional query filename.
            var msg = baseMsg;
            if (!string.IsNullOrWhiteSpace(query)) msg += " | QueryFile: " + query;

            // Do not emit to console/TestContext. Persist to file only.

            // Try to write to TestAttachments directory (best-effort)
            try
            {
                var directory = TryGetTestAttachmentsDirectory(contextSource);

                // If framework didn't provide a directory, fallback to a safe TestAttachments path under base directory
                if (string.IsNullOrWhiteSpace(directory))
                {
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
                    var dateFolder = DateTime.UtcNow.ToString("dd-MM-yyyy");
                    directory = Path.Combine(baseDir, "TestAttachments", dateFolder, "UnknownFeature");
                }

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    // Ensure directory exists (DirectorySetupHooks should have created it, but be safe)
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                    var line = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} | {msg}" + Environment.NewLine;

                    try
                    {

                        // Try to reuse a per-test log file path stored in ObjectContext
                        var filePath = TryGetApiLogFilePath(contextSource);
                        string fileName;
                        var needHeader = false;

                        if (string.IsNullOrWhiteSpace(filePath))
                        {
                            // No pre-created file path available - compute deterministic filename (date + test id)
                            var testIdLocal = "unknown";
                            try { testIdLocal = TestContext.CurrentContext.Test.ID ?? TestContext.CurrentContext.Test.Name ?? "unknown"; } catch { }
                            var dateLocal = DateTime.UtcNow.ToString("yyyyMMdd");
                            fileName = $"API_TEST_LOGS_{dateLocal}_{testIdLocal}.txt";
                            filePath = Path.Combine(directory, fileName);
                            needHeader = !File.Exists(filePath);
                        }
                        else
                        {
                            fileName = Path.GetFileName(filePath);
                            // If file doesn't exist yet, we'll need to write a header
                            needHeader = !File.Exists(filePath);
                        }

                        if (needHeader)
                        {
                            // Try to extract response uri and body from ObjectContext debug info
                            string responseUri = string.Empty;
                            string body = string.Empty;

                            try
                            {
                                // Try to extract concise responseUri and body from ObjectContext if available
                                try
                                {
                                    var oc = TryGetObjectContext(contextSource);
                                    if (oc != null)
                                    {
                                        // ObjectContext stores combined debug information; try to extract last RequestUri and Body lines
                                        var respList = oc.GetDebugInformations("RequestUri");
                                        var resp = respList?.LastOrDefault() ?? string.Empty;
                                        if (!string.IsNullOrWhiteSpace(resp))
                                        {
                                            // keep only the RequestUri/ResponseUri line (short)
                                            responseUri = resp.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? resp;
                                        }

                                        var bodyList = oc.GetDebugInformations("Body");
                                        var b = bodyList?.LastOrDefault() ?? string.Empty;
                                        if (!string.IsNullOrWhiteSpace(b))
                                        {
                                            // keep only first line of body to avoid huge header
                                            body = b.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? b;
                                        }
                                    }
                                }
                                catch
                                {
                                    // ignore extraction failures
                                }

                                    var header = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} | RESPONSE_URI: {responseUri}{Environment.NewLine}{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} | BODY: {body}{Environment.NewLine}";

                                    if (!string.IsNullOrWhiteSpace(sqlRow))
                                    {
                                        header += $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} | SQL_RESULT: {sqlRow}{Environment.NewLine}{Environment.NewLine}";
                                    }

                                // Write header + first assertion line
                                File.WriteAllText(filePath, header + line);
                                try { TestContext.Progress.WriteLine($"API_LOG_CREATED: {filePath}"); } catch { }

                                // Register the file as a test attachment using the framework helper to ensure consistent registration
                                try
                                {
                                    TestAttachmentHelper.AddTestAttachment(directory, fileName, (p) => { /* no-op - file already created */ });
                                    try { TestContext.Progress.WriteLine($"API_LOG_REGISTER_REQUESTED: {filePath}"); } catch { }
                                }
                                catch
                                {
                                    // ignore registration failures
                                }
                            }
                            catch
                            {
                                // If header write fails, try to append line only
                                File.AppendAllText(filePath, line);
                            }
                        }
                        else
                        {
                            // If SQL result provided and file does not yet contain SQL_RESULT, insert it after the header
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(sqlRow) && File.Exists(filePath))
                                {
                                    var contents = File.ReadAllText(filePath);
                                    if (!contents.Contains("SQL_RESULT:"))
                                    {
                                        var insertPos = contents.IndexOf(Environment.NewLine + Environment.NewLine, StringComparison.Ordinal);
                                        if (insertPos >= 0)
                                        {
                                            insertPos += (Environment.NewLine + Environment.NewLine).Length;
                                            var sqlLine = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} | SQL_RESULT: {sqlRow}{Environment.NewLine}{Environment.NewLine}";
                                            contents = contents.Insert(insertPos, sqlLine);
                                            File.WriteAllText(filePath, contents);
                                        }
                                        else
                                        {
                                            // fallback: append SQL result at end
                                            File.AppendAllText(filePath, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} | SQL_RESULT: {sqlRow}{Environment.NewLine}{Environment.NewLine}");
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                // ignore insertion failures
                            }

                            File.AppendAllText(filePath, line);
                            try { TestContext.Progress.WriteLine($"API_LOG_APPENDED: {filePath}"); } catch { }

                            // Ensure the API log file is registered for this test run even if it existed prior
                            try
                            {
                                TestAttachmentHelper.AddTestAttachment(directory, fileName, (p) => { /* no-op - file already exists */ });
                                try { TestContext.Progress.WriteLine($"API_LOG_REGISTER_REQUESTED: {filePath}"); } catch { }
                            }
                            catch
                            {
                                // ignore registration failures
                            }
                        }
                    }
                    catch
                    {
                        // Do not throw on logging failures
                    }
                }
            }
            catch
            {
                // Do not throw on logging failures
            }
        }

        public static void LogHighLevel(string message, object contextSource = null, string endpoint = "", string query = "", string sqlRow = "")
        {
            LogAssertionResult("HIGHLEVEL", message, string.Empty, true, contextSource, endpoint, query, sqlRow);
        }

        private static string TryGetTestAttachmentsDirectory(object contextSource)
        {
            try
            {
                var oc = TryGetObjectContext(contextSource);
                if (oc != null) return oc.GetDirectory() ?? string.Empty;
            }
            catch
            {
                // ignore
            }

            return string.Empty;
        }

        private static string TryGetApiLogFilePath(object contextSource)
        {
            try
            {
                var oc = TryGetObjectContext(contextSource);
                if (oc != null)
                {
                    var fp = oc.Get("api_test_log_file");
                    if (!string.IsNullOrWhiteSpace(fp)) return fp;
                }
            }
            catch
            {
                // ignore
            }

            return string.Empty;
        }

        private static ObjectContext TryGetObjectContext(object contextSource)
        {
            if (contextSource == null) return null;

            try
            {
                // Walk the type hierarchy looking for an ObjectContext field/property
                var type = contextSource.GetType();

                while (type != null)
                {
                    // common field names used across the codebase
                    var fieldNames = new[] { "_objectContext", "objectContext", "ObjectContext", "_context", "context" };

                    foreach (var fname in fieldNames)
                    {
                        var fi = type.GetField(fname, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        if (fi != null)
                        {
                            var oc = fi.GetValue(contextSource) as ObjectContext;
                            if (oc != null) return oc;
                        }
                    }

                    // try properties as well
                    var propNames = new[] { "ObjectContext", "ObjectCtx", "Context", "ContextObject" };
                    foreach (var pname in propNames)
                    {
                        var pi = type.GetProperty(pname, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (pi != null)
                        {
                            var val = pi.GetValue(contextSource);
                            if (val is ObjectContext oc2) return oc2;
                        }
                    }

                    // Move up the inheritance chain
                    type = type.BaseType;
                }
            }
            catch
            {
                // ignore failures
            }

            return null;
        }
    }
}
