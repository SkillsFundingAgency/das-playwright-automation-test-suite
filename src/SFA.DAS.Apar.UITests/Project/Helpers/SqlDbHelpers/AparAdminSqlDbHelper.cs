namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class AparAdminSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.RoatpDatabaseConnectionString)
{
    public async Task DeleteTrainingProvider(string ukprn) => await ExecuteSqlCommand($"DELETE FROM Organisations WHERE UKPRN ='{ukprn}'");

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
