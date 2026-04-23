namespace SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers
{
    internal class LearningDbSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.LearningDbConnectionString)
    {
        private readonly DbConfig _dbConfig = dbConfig;

        internal async Task<string> CheckIfApprenticeshipRecordCreatedInLearningDb(int ApprenticeshipId, string ULN)
        {
            string query = $@"SELECT TOP(1) l.[Key] FROM [dbo].[Learner] l
                                JOIN [dbo].[ApprenticeshipLearning] al 
                                ON l.[key] = al.LearnerKey
                                WHERE l.uln = '{ULN}'
                                AND al.ApprovalsApprenticeshipId = {ApprenticeshipId}";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }

        internal async Task<string> CheckIfShortCourseLearnerRecordUpdatedInLearningDb(int ApprenticeshipId, string ULN)
        {
            string query = $@"  SELECT TOP(1) l.[Key] 
                                  FROM [dbo].[Learner] l
                                  INNER JOIN [dbo].[ShortCourseLearning] scl
                                  ON l.[Key] = scl.[LearnerKey]
                                  INNER JOIN [dbo].[ShortCourseEpisode] scp
                                  ON scl.[Key] = scp.[LearningKey]
                                  WHERE l.Uln = '{ULN}'
                                AND scp.ApprovalsApprenticeshipId = {ApprenticeshipId}
                                  AND scp.IsApproved = 1";
            var result = await GetData(query);
            return result.FirstOrDefault() ?? string.Empty;
        }
    }
}
