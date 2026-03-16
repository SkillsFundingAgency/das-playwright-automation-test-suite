namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class LearningDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.LearningDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task<string> CheckIfApprenticeshipRecordCreatedInLearningDb(int ApprenticeshipId, string ULN)
        {
            string query = $@"SELECT * FROM [dbo].[Learner] l
                                JOIN [dbo].[ApprenticeshipLearning] al 
                                ON l.[key] = al.LearnerKey
                                WHERE l.uln = {ULN}
                                AND al.ApprovalsApprenticeshipId = {ApprenticeshipId}";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }
    }
}
