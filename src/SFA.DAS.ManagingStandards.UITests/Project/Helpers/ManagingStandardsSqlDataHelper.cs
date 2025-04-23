﻿using SFA.DAS.ConfigurationBuilder;
using System.Threading.Tasks;

namespace SFA.DAS.ManagingStandards.UITests.Project.Helpers;

public class ManagingStandardsSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ManagingStandardsDbConnectionString)
{
    public async Task ClearRegulation(string ukprn, string larsCode) => await ExecuteSqlCommand($"update providercourse set IsApprovedByRegulator = NULL " +
        $"where LarsCode = '{larsCode}' and providerid = (select Id from provider where ukprn = {ukprn})");

    public async Task AddSingleProviderCourseLocation(string ukprn, string larsCode) => await ExecuteSqlCommand(
        $"DECLARE @providerId int; select @providerId = id from provider where ukprn = {ukprn};" +
        $"DECLARE @CentralHairHarlowLocationId int; select @CentralHairHarlowLocationId = id from ProviderLocation where LocationName ='CENTRAL HAIR HARLOW' and ProviderId = @providerId;" +
        $"DECLARE @ProviderCourseIdLarsCode int; select @ProviderCourseIdLarsCode = id from providerCourse where providerId = @providerId and larsCode={larsCode};" +
        $"Delete from ProviderCourseLocation where ProviderCourseId = @ProviderCourseIdLarsCode;" +
        $"if (not exists (select * from providerCourseLocation where ProviderCourseId = @ProviderCourseIdLarsCode))" +
        $"BEGIN INSERT INTO [dbo].[ProviderCourseLocation] " +
        $"([NavigationId],[ProviderCourseId],[ProviderLocationId],[HasDayReleaseDeliveryOption],[HasBlockReleaseDeliveryOption],[IsImported])" +
        $"VALUES (newId(), @ProviderCourseIdLarsCode, @CentralHairHarlowLocationId,1, 0, 0) END;");

    public async Task ClearProviderLocation(string ukprn, string venueName) => await ExecuteSqlCommand
        ($"DECLARE @providerId int;" +
        $"Select @providerId = id from provider where ukprn = {ukprn};" +
        $"Delete from ProviderLocation where providerid = @providerId and locationname = '{venueName}';");
}
