namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class LearningDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.LearningDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task<string> CheckIfApprenticeshipRecordCreatedInLearningDb(int ApprenticeshipId, string ULN)
        {
            string query = $"SELECT [Key] FROM [dbo].[Learning] WHERE ApprovalsApprenticeshipId = {ApprenticeshipId} AND Uln = {ULN}";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }
    }
}
