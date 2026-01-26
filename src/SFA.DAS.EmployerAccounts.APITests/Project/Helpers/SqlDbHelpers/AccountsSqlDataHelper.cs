using System;
using System.IO;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlHelpers
{
    public class AccountsSqlDataHelper : SqlDbHelper
    {
        private readonly DbConfig _dbConfig;

        public AccountsSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig)
            : base(objectContext, dbConfig.AccountsDbConnectionString)
        {
            _dbConfig = dbConfig;
        }

        public async Task<List<string>> GetTestAccountDetailsFromDB()
        {
            // Step 1: fetch distinct levy AccountIds (most-recent first)
            var levyQuery = @"SELECT DISTINCT AccountId FROM [employer_financial].[LevyDeclaration] ORDER BY AccountId DESC";

            var levyIds = new System.Collections.Generic.List<string>();

            try
            {
                // Try to query the Finance DB for a levy AccountId (use the Finance connection string)
                if (!string.IsNullOrWhiteSpace(_dbConfig?.FinanceDbConnectionString))
                {
                    var singleLevyRow = await GetData(levyQuery, _dbConfig.FinanceDbConnectionString);
                    if (singleLevyRow != null && singleLevyRow.Count > 0 && !string.IsNullOrWhiteSpace(singleLevyRow[0]))
                    {
                        levyIds.Add(singleLevyRow[0].Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                // If LevyDeclaration table/schema isn't available on the current connection, fall back
                // to the previous selection strategy. Log the issue for diagnostics but do not fail the test here.
                try { TestContext.Progress.WriteLine($"[WARN] LevyDeclaration lookup failed: {ex.Message}"); } catch { }
                levyIds.Clear();
            }

            // If no levy accounts found, fall back to original selection criteria
            string accountFilterClause;
            if (levyIds.Count == 0)
            {
                accountFilterClause = @"AccountId IN (
                        SELECT TOP 1 AccountId as legalEntities 
                        FROM employer_account.AccountLegalEntity 
                        WHERE Deleted is null 
                        GROUP BY AccountId HAVING count(AccountId) > 1 
                        ORDER BY AccountId DESC
                    )";
            }
            else
            {
                // Build an IN (...) clause. If all ids are numeric, don't quote; otherwise quote safely.
                bool allNumeric = true;
                foreach (var id in levyIds)
                {
                    if (!long.TryParse(id, out _)) { allNumeric = false; break; }
                }

                string inList;
                if (allNumeric)
                {
                    inList = string.Join(",", levyIds);
                }
                else
                {
                    inList = string.Join(",", levyIds.ConvertAll(id => "'" + id.Replace("'", "''") + "'"));
                }

                accountFilterClause = $"mv.AccountId IN ({inList})";
            }

            var query = $@"SELECT TOP 1 
                        mv.UserRef as StubAuthId, 
                        mv.Email as StubAuthEmail, 
                        mv.AccountId as accountId,
                        mv.HashedAccountId as hashedAccountId,
                        mv.UserId,
                        acc.PublicHashedId as FrontendAgreementID
                    FROM employer_account.MembershipView mv
                    JOIN employer_account.Account acc
                        ON acc.HashedId = mv.HashedAccountId
                    WHERE {accountFilterClause}";

            var result = await GetData(query);

            return result;
        }

        public async Task<List<string>> ExecuteSqlFileWithReplacements(string sqlFileName, IDictionary<string, string> replacements)
        {
            if (string.IsNullOrWhiteSpace(sqlFileName)) throw new ArgumentException("sqlFileName must be provided", nameof(sqlFileName));

            string sql;

            // Prefer reading a local SQL file from the project helpers folder to preserve line breaks
            var altPath = Path.Combine(FileHelper.GetLocalProjectRootFilePath(), "Project", "Helpers", "SqlDbHelpers", sqlFileName);

            if (File.Exists(altPath))
            {
                sql = File.ReadAllText(altPath);
            }
            else
            {
                // Fall back to FileHelper which may be used in ADO/CI contexts
                // Ensure we pass the file name without extension to avoid double-extension lookups
                sql = FileHelper.GetSql(Path.GetFileNameWithoutExtension(sqlFileName));
            }

            // Replace known placeholders with provided values (case-insensitive keys)
            if (replacements != null)
            {
                foreach (var kv in replacements)
                {
                    if (string.IsNullOrEmpty(kv.Key)) continue;
                    var value = kv.Value ?? string.Empty;

                    // support several placeholder formats: <Key>, {{Key}}, [Key]
                    sql = sql.Replace($"<{kv.Key}>", value);
                    sql = sql.Replace($"{{{{{kv.Key}}}}}", value);
                    sql = sql.Replace($"[{kv.Key}]", value);
                    // also support lowercase/uppercase variants
                    sql = sql.Replace($"<{kv.Key.ToLowerInvariant()}>", value);
                    sql = sql.Replace($"<{kv.Key.ToUpperInvariant()}>", value);
                }
            }

            try
            {
                // Ensure any inline SQL single-line comments are preserved as separate lines.
                // FileHelper.GetSql flattens newlines which can turn a comment marker "--" into
                // an inline comment that swallows the rest of the query. Restore line breaks
                // before comment markers so the SQL executes correctly.
                sql = sql.Replace(",--", "\r\n--");
                sql = sql.Replace(" --", "\r\n--");

                TestContext.Progress.WriteLine($"[DEBUG SQL after replacements]: {sql}");
            }
            catch
            {
                // ignore if progress logging fails
            }

            // Decide which DB connection to use. Some SQL files (e.g., finance-related)
            // reference the employer_financial schema which exists on the Finance DB.
            // Prefer Finance DB when the SQL references that schema or the file name
            // strongly indicates finance data (getTransactions.sql).
            string execConnection = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(_dbConfig?.FinanceDbConnectionString) &&
                    (sql.IndexOf("employer_financial", StringComparison.OrdinalIgnoreCase) >= 0 ||
                     string.Equals(sqlFileName, "getTransactions.sql", StringComparison.OrdinalIgnoreCase)))
                {
                    execConnection = _dbConfig.FinanceDbConnectionString;
                }
            }
            catch
            {
                execConnection = null;
            }

            var result = execConnection == null ? await GetData(sql) : await GetData(sql, execConnection);

            return result;
        }

        // Public wrapper to execute an arbitrary SQL string from test step definitions.
        public async Task<List<string>> ExecuteSql(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentException("sql must be provided", nameof(sql));

            return await GetData(sql);
        }
    }
}
