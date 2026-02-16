
using System.Linq;

namespace SFA.DAS.ProvideFeedback.UITests.Project.Helpers;


public class EmployerFeedbackSqlHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.EmployerFeedbackDbConnectionString)
{
    public async Task<(string uniqueSurveycode, string ukprn)> GetTestData(string email)
    {
        var data = await GetData($"select TOP (1) [UniqueSurveyCode] , [Ukprn] FROM [dbo].[vw_EmployerSurveyHistoryComplete] where EmailAddress = '{email}'");

        return (data[0], data[1]);
    }

    public async Task ClearDownEmployerFeedbackResult(string uniqueSurveyCode)
    {
        var query = $"DECLARE @varaccountid bigint, @varukprn bigint, @varfeedbackid bigint;" +
            $"SELECT TOP(1) @varaccountid = [AccountId], @varukprn = [Ukprn] FROM [dbo].[vw_EmployerSurveyHistoryComplete] WHERE UniqueSurveyCode = '{uniqueSurveyCode}';" +
            $"SELECT @varfeedbackid = FeedbackId FROM EmployerFeedback WHERE AccountId = @varaccountid AND ukprn = @varukprn;" +
            $"DELETE FROM [dbo].[ProviderAttributes] WHERE EmployerFeedbackResultId in (SELECT Id FROM EmployerFeedbackResult WHERE FeedbackId = @varfeedbackid);" +
            $"DELETE FROM [dbo].[EmployerFeedbackResult] WHERE FeedbackId = @varfeedbackid;" +
            $"UPDATE EmployerSurveyCodes SET BurnDate = null WHERE FeedbackId = @varfeedbackid;" +
            $"UPDATE [dbo].[vw_EmployerSurveyHistoryComplete] SET CodeBurntDate = NULL WHERE UniqueSurveyCode = '{uniqueSurveyCode}'";

        await ExecuteSqlCommand(query);
    }

    public async Task CreateEmployerFeedback(string ukprn, List<ProviderRating> data)
    {
        var userReference = "21ABB8EE-D5C3-46E3-A3BA-00039E3F3584";
        var accountId = 1;

        var sql =
            $"delete from ProviderStarsSummary where Ukprn = {ukprn};" +
            $"delete from ProviderRatingSummary where Ukprn = {ukprn}; " +
            $"delete from ProviderAttributes where EmployerFeedbackResultId in (select Id from EmployerFeedbackResult where FeedbackId in (select FeedbackId from EmployerFeedback where Ukprn = {ukprn}))" +
            $"delete from EmployerFeedbackResult where FeedbackId in (select FeedbackId from EmployerFeedback where Ukprn = {ukprn})" +
            $"delete from EmployerFeedback where Ukprn = {ukprn}";

        foreach (var row in data)
        {
            sql += $"INSERT INTO [dbo].[EmployerFeedback]([UserRef], [Ukprn], [AccountId])" +
                  $"VALUES ('{userReference}', {ukprn}, {accountId});";

            accountId++;
        }

        await ExecuteSqlCommand(sql);
    }

    public async Task CreateEmployerFeedbackResults(string ukprn, List<ProviderRating> data)
    {
        if (data.Count == 0) return;

        var currentAcademicYearDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var previousAcademicYearDate = DateTime.UtcNow.AddYears(-1).ToString("yyyy-MM-dd");

        var count = data.Count;
        var feedbackIds = await GetMultipleData($"SELECT TOP {count} FeedbackId FROM [EmployerFeedback] WHERE Ukprn = {ukprn} ORDER BY FeedbackId DESC");

        var sql = new StringBuilder();

        var i = 0;
        foreach (var row in data)
        {
            var feedbackId = feedbackIds[i].First();
            var resultDate = row.AcademicYear == "Previous" ? previousAcademicYearDate : currentAcademicYearDate;
            var resultId = Guid.NewGuid();

            sql.Append($@"
            INSERT INTO [dbo].[EmployerFeedbackResult]
            ([Id],[FeedbackId],[DateTimeCompleted],[ProviderRating],[FeedbackSource])
            VALUES 
            ('{resultId}',{feedbackId},'{resultDate}','{row.Rating}',1);");

            sql.Append($@"
            INSERT INTO [dbo].[ProviderAttributes]([EmployerFeedbackResultId],[AttributeId],[AttributeValue]) VALUES 
            ('{resultId}', 1, 1),
            ('{resultId}', 2, 1),
            ('{resultId}', 3, 1);
            ");

            i++;
        }

        await ExecuteSqlCommand(sql.ToString());
    }

    public async Task GenerateFeedbackSummaries()
    {
        var query = $"EXEC [dbo].[GenerateProviderRatingResults]";
        await ExecuteSqlCommand(query);

        query = $"EXEC [dbo].[GenerateProviderAttributeResults]";
        await ExecuteSqlCommand(query);
    }

    public async Task UpdateEmployerFeedbackResult()
    {
        var feedbackId = 20394;

        var sql =
            $"UPDATE [EmployerFeedbackResult] " +
            $"SET datetimecompleted = DATEADD(day, -21, datetimecompleted) " +
            $"WHERE feedbackid = '{feedbackId}' " +
            $"AND datetimecompleted = ( " +
            $"SELECT TOP 1 datetimecompleted " +
            $"FROM [EmployerFeedbackResult] " +
            $"WHERE feedbackid = '{feedbackId}' " +
              $"ORDER BY datetimecompleted DESC" +
            $");";

        await ExecuteSqlCommand(sql);
    }
}
