namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class ManagingStandardsSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ManagingStandardsDbConnectionString)
{
    public async Task ResetLastStartDate() => await ExecuteSqlCommand
        ($"Update ProviderAllowedCourse set LastDateStarts = null where larscode = '272' and ukprn = '10061102'");
    
    public async Task ResetTrainingProviderFromCourse() => await ExecuteSqlCommand
        ($"Delete from ProviderAllowedCourse where ukprn = '10043565'");
    
    public async Task ClearProviderRestrictedCourses()
    {
        var query = $"DELETE FROM ProviderAllowedCourse WHERE UKPRN = '10001259' AND  larscode = '287';" +
        $"DELETE FROM ProviderAllowedCourse WHERE UKPRN = '10001259' AND  larscode = '535';";
        await ExecuteSqlCommand(query);
    }

    public async Task ClearProviderAllowedCourse()
    {
        var query = $"DELETE FROM ProviderAllowedCourse WHERE UKPRN = '10004692' AND  larscode = '483';" +
        $"DELETE FROM ProviderAllowedCourse WHERE UKPRN = '10004692' AND  larscode = 'ZSC00001';";
        await ExecuteSqlCommand(query);
    }


    public async Task AddLastStartDateRestrictProvider() => await ExecuteSqlCommand
        ($"UPDATE ProviderAllowedCourse set LastDateStarts = '2026-08-06' where larscode = '828' and ukprn = '10001259'");

    public async Task UnrestrictProvider()
    {
       var query = $" DECLARE @Ukprn VARCHAR(20) = '10057068';" +
       $"DELETE pac FROM ProviderAllowedCourse pac WHERE pac.Ukprn = @Ukprn AND pac.LastDateStarts IS NULL AND EXISTS(SELECT 1 FROM ProviderCourse pc WHERE pc.Ukprn = pac.Ukprn AND pc.LarsCode = pac.LarsCode);" +
       $"UPDATE ProviderCourseType SET IsRestrictedProvider = 0 WHERE Ukprn = @Ukprn;" +
       $"DELETE FROM Audit WHERE Id = (SELECT TOP (1) Id FROM Audit WHERE EntityId = @Ukprn AND UserAction = 'UpdateProviderAllowedCourse' ORDER BY AuditDate DESC);" +
       $"DELETE FROM Audit WHERE Id = (SELECT TOP (1) Id FROM Audit WHERE EntityId = @Ukprn AND UserAction = 'UpdateProviderCourseType' ORDER BY AuditDate DESC);";
       await ExecuteSqlCommand(query);
    }

    public async Task ClearProviderCourseType(string ukprn)
    {
        await ExecuteSqlCommand($"DELETE FROM ProviderCourseType where ukprn = '{ukprn}';");
    }
}
