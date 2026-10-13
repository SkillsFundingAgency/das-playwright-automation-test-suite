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
}
