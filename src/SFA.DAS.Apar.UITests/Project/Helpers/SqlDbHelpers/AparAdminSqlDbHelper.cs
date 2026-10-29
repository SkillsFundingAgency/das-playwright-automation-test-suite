namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class AparAdminSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.RoatpDatabaseConnectionString)
{
    public async Task DeleteTrainingProvider(string ukprn)
    {   
        await DeleteFromOrganisationCourseTypes(ukprn);
        await ExecuteSqlCommand($"DELETE FROM Organisations WHERE ukprn = '{ukprn}';");
    }

    public async Task ResetProviderDetailsToSupporting(string ukprn)
    {
        await DeleteFromOrganisationCourseTypes(ukprn);
        var query = $"UPDATE Organisations SET StatusId = 0, ProviderTypeId = 3, OrganisationTypeId = 0, RemovedReasonId = NULL WHERE ukprn = '{ukprn}';" ;
            await ExecuteSqlCommand(query);       
    }

    public async Task DeleteFromOrganisationCourseTypes(string ukprn)
    {
        var query = $@" DECLARE @OrganisationId UNIQUEIDENTIFIER;

            SELECT @OrganisationId = Id
            FROM Organisations
            WHERE UKPRN = '{ukprn}';

            IF @OrganisationId IS NOT NULL
            BEGIN
                DELETE FROM OrganisationCourseTypes
                WHERE OrganisationId = @OrganisationId;
            END
        ";
        await ExecuteSqlCommand(query);
    }
}
