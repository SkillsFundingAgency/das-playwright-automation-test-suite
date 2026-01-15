namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class AparAdminSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.RoatpDatabaseConnectionString)
{
    public async Task DeleteTrainingProvider(string ukprn)
    {
        await ExecuteSqlCommand($@"
            DECLARE @OrganisationId UNIQUEIDENTIFIER;

            SELECT @OrganisationId = Id
            FROM Organisations
            WHERE UKPRN = '{ukprn}';

            IF @OrganisationId IS NOT NULL
            BEGIN
                DELETE FROM OrganisationCourseTypes
                WHERE OrganisationId = @OrganisationId;

                DELETE FROM Organisations
                WHERE Id = @OrganisationId;
            END
        ");
    }

    public async Task ResetProviderDetails(string ukprn) =>
await ExecuteSqlCommand($@"
        UPDATE Organisations
        SET StatusId = 0,
            ProviderTypeId = 3,
            OrganisationTypeId = 0,
            RemovedReasonId = NULL
        WHERE ukprn = '{ukprn}';
    ");
}
