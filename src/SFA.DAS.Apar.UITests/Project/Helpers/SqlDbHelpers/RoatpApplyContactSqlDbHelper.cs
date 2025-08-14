namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class RoatpApplyContactSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.ApplyDatabaseConnectionString)
{
    public async Task DeleteContact(string email) => await ExecuteSqlCommand($"DELETE FROM Contacts WHERE Email ='{email}'");

    public async Task<string> GetSignInId(string email) => await GetNullableData($"select SigninId from dbo.Contacts where email = '{email}'");
}
