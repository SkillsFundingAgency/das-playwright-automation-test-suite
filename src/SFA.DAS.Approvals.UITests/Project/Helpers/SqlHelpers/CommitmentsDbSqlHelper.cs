using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class CommitmentsDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.CommitmentsDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task UpdateCohortLastUpdatedDate(string cohortRef, DateTime lastUpdatedDate)
        {
            string query = $"UPDATE Commitment SET LastUpdatedOn = '{lastUpdatedDate.ToString("yyyy-MM-dd")}' WHERE Reference = '{cohortRef}'";
            await ExecuteSqlCommand(query);
        }

        internal async Task<string> GetWithPartyValueFromCommitmentsDb(string cohortRef)
        {
            string query = $"SELECT WithParty FROM Commitment WHERE Reference = '{cohortRef}'";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

    }
}
