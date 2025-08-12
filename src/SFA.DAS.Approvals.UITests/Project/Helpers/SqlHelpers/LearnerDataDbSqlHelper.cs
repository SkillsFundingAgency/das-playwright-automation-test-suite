using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class LearnerDataDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.LearnerDataDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task<string> GetLearnerDataId(string ULN)
        {
            objectContext.SetDebugInformation($"LearnerDataDbSqlHelper: ConnectionString: {_dbConfig.LearnerDataDbConnectionString}");
            
            string query = $"SELECT Id FROM LearnerData WHERE ULN = '{ULN}'";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

        internal async Task<string> GetApprenticeshipIdLinkedWithLearnerData(int LearnerDataId)
        {
            string query = $"SELECT ApprenticeshipId FROM LearnerData WHERE Id = {LearnerDataId}";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

    }

}
