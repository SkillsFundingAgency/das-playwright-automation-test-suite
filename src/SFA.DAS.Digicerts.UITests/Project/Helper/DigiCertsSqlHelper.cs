
using NUnit.Framework.Internal;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SFA.DAS.ProvideFeedback.UITests.Project.Helpers;


public class DigiCertsSqlHelper(ObjectContext objectContext, DbConfig config) : SqlDbHelper(objectContext, config.DigiCertDbConnectionString)
{

    

    public async Task RemoveAuthentication(string govUkIdentifier)
    {
        var query = $"SET XACT_ABORT ON;" +

            $"BEGIN TRY " +
            $"BEGIN TRANSACTION;" +

            $"DECLARE @UserId UNIQUEIDENTIFIER;" +

            $"SELECT @UserId = Id " +
            $"FROM [dbo].[User] " +
            $"WHERE GovUkIdentifier = '{govUkIdentifier}';" +

            $"IF @UserId IS NULL " +
            $"THROW 50000, '@UserId must be set.', 1;" +

            $"DECLARE @SharingIds TABLE (Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY);" +

            $"DECLARE @SharingEmailIds TABLE (Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY);" +

            $"INSERT INTO @SharingIds (Id) " +
            $"SELECT Id " +
            $"FROM [dbo].[Sharing] " +
            $"WHERE UserId = @UserId;" +

            $"INSERT INTO @SharingEmailIds (Id) " +
            $"SELECT Id " +
            $"FROM [dbo].[SharingEmail] " +
            $"WHERE SharingId IN (SELECT Id FROM @SharingIds);" +

            $"DELETE FROM [dbo].[SharingEmailAccess] " +
            $"WHERE SharingEmailId IN (SELECT Id FROM @SharingEmailIds);" +

            $"DELETE FROM [dbo].[SharingEmail] " +
            $"WHERE Id IN (SELECT Id FROM @SharingEmailIds);" +

            $"DELETE FROM [dbo].[SharingAccess] " +
            $"WHERE SharingId IN (SELECT Id FROM @SharingIds);" +

            $"DELETE FROM [dbo].[Sharing] " +
            $"WHERE Id IN (SELECT Id FROM @SharingIds);" +

            $"DELETE FROM [dbo].[AdminActions] " +
            $"WHERE UserActionId IN (SELECT Id FROM [dbo].[UserActions] WHERE UserId = @UserId);" +

            $"DELETE FROM [dbo].[UserActions] " +
            $"WHERE UserId = @UserId;" +

            $"DELETE FROM [dbo].[UserAuthorisation] " +
            $"WHERE UserId = @UserId;" +

            $"DELETE FROM [dbo].[UserIdentity] " +
            $"WHERE UserId = @UserId;" +

            $"DELETE FROM [dbo].[UserMatch] " +
            $"WHERE UserId = @UserId;" +

            $"COMMIT TRANSACTION;" +

            $"END TRY " +

            $"BEGIN CATCH " +

            $"IF @@TRANCOUNT > 0 " +
            $"BEGIN " +
            $"ROLLBACK TRANSACTION; " +
            $"END; " +

            $"DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();" +
            $"DECLARE @ErrorSeverity INT = ERROR_SEVERITY();" +
            $"DECLARE @ErrorState INT = ERROR_STATE();" +

            $"RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);" +

            $"END CATCH;";

        await ExecuteSqlCommand(query);
    }

    public async Task<(string Uln, string StandardName, DateTime AchievementDate, string ProviderName)> SingleCertificateAuthorisationdetailsfromuser(string firstname, string lastname)
    {
        var data = await GetData(
            $"SELECT TOP (1) " +
            $"uln, " +
            $"StandardName, " +
            $"achievementDate, " +
            $"ProviderName " +
            $"FROM [dbo].[Certificates] " +
            $"WHERE LearnerGivenNames = '{firstname}' " +
            $"AND LearnerFamilyName = '{lastname}'");

        return (
            data[0],
            data[1],
            DateTime.Parse(data[2]),
            data[3]
        );
    }


}
