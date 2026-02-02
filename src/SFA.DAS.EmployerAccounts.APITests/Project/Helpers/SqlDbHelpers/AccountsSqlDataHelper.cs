using System;
using System.IO;

namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers.SqlHelpers
{
    public class AccountsSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AccountsDbConnectionString)
    {

        // Public wrapper to execute an arbitrary SQL string from test step definitions.
        public async Task<List<string>> ExecuteSql(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentException("sql must be provided", nameof(sql));

            return await GetData(sql);
        }
    }
}
