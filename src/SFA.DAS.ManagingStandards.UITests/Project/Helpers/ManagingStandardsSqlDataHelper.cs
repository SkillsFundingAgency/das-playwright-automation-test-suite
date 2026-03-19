using SFA.DAS.ConfigurationBuilder;
using System.Threading.Tasks;

namespace SFA.DAS.ManagingStandards.UITests.Project.Helpers;

public class ManagingStandardsSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ManagingStandardsDbConnectionString)
{
    public async Task ClearRegulation(string ukprn, string larsCode) => await ExecuteSqlCommand($"update providercourse set IsApprovedByRegulator = NULL " +
        $"where LarsCode = '{larsCode}' and providerid = (select Id from provider where ukprn = {ukprn})");

    public async Task AddSingleProviderCourseLocation(string ukprn, string larsCode)
     => await ExecuteSqlCommand(
         $"DECLARE @providerId int; " +
         $"SELECT @providerId = id FROM provider WHERE ukprn = {ukprn}; " +

         $"DECLARE @CentralHairHarlowLocationId int; " +
         $"SELECT @CentralHairHarlowLocationId = id FROM ProviderLocation " +
         $"WHERE LocationName = 'CENTRAL HAIR HARLOW' AND ProviderId = @providerId; " +

         $"DECLARE @ProviderCourseIdLarsCode int; " +
         $"SELECT @ProviderCourseIdLarsCode = id FROM providerCourse " +
         $"WHERE providerId = @providerId AND larsCode = '{larsCode}'; " +

         $"DELETE FROM ProviderCourseLocation WHERE ProviderCourseId = @ProviderCourseIdLarsCode; " +

         $"IF (NOT EXISTS (SELECT 1 FROM ProviderCourseLocation WHERE ProviderCourseId = @ProviderCourseIdLarsCode)) " +
         $"BEGIN " +
         $"INSERT INTO [dbo].[ProviderCourseLocation] " +
         $"([NavigationId],[ProviderCourseId],[ProviderLocationId],[HasDayReleaseDeliveryOption],[HasBlockReleaseDeliveryOption],[IsImported]) " +
         $"VALUES (NEWID(), @ProviderCourseIdLarsCode, @CentralHairHarlowLocationId, 1, 0, 0) " +
         $"END;"
     );

    public async Task ClearProviderLocation(string ukprn, string venueName) => await ExecuteSqlCommand
        ($"DECLARE @providerId int;" +
        $"Select @providerId = id from provider where ukprn = {ukprn};" +
        $"Delete from ProviderLocation where providerid = @providerId and locationname = '{venueName}';");

    public async Task ClearStandard(string ukprn, string larsCode) =>
    await ExecuteSqlCommand(
        $"DELETE pcl " +
        $"FROM ProviderCourseLocation pcl " +
        $"INNER JOIN ProviderCourse pc ON pc.Id = pcl.ProviderCourseId " +
        $"INNER JOIN Provider p ON p.Id = pc.ProviderId " +
        $"WHERE p.Ukprn = {ukprn} " +
        $"AND pc.LarsCode = '{larsCode}'; " +

        $"DELETE pc " +
        $"FROM ProviderCourse pc " +
        $"INNER JOIN Provider p ON p.Id = pc.ProviderId " +
        $"WHERE p.Ukprn = {ukprn} " +
        $"AND pc.LarsCode = '{larsCode}';"
    );
}
