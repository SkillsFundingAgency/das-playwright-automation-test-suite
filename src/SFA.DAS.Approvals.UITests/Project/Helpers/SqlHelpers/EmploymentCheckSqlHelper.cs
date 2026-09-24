using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class EmploymentCheckSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.EmploymentCheckDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task SetEmploymentChecksData(int ApprenticeshipId, int? Employed, int RequestCompletionStatus, string? Error)
        {
            var date = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            var employed = Employed.HasValue ? Employed.Value.ToString() : "NULL";
            string query = $@"UPDATE [Business].[EmploymentCheck]
                                SET 
                                    MinDate = '{date}', 
                                    MaxDate = '{date}', 
                                    Employed = {employed}, 
                                    RequestCompletionStatus = {RequestCompletionStatus}, 
                                    ErrorType = '{Error}', 
                                    MessageSentDate = '{date}',  
                                    CreatedOn = '{date}',  
                                    LastUpdatedOn = '{date}'
                            WHERE apprenticeshipid = {ApprenticeshipId}";

            await ExecuteSqlCommand(query);
        }
    }
}
