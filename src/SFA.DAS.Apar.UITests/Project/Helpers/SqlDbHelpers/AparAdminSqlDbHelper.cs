namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class AparAdminSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.RoatpDatabaseConnectionString)
{
    public async Task DeleteTrainingProvider(string ukprn) => await ExecuteSqlCommand($"DELETE FROM Organisations WHERE UKPRN ='{ukprn}'");
}
