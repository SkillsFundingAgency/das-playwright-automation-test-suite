namespace SFA.DAS.Apar.UITests.Project.Helpers.SqlDbHelpers;

public class LoginInvitationsSqlDbHelper(ObjectContext objectContext, DbConfig dbConfig) : InvitationsSqlDbHelper(objectContext, dbConfig.LoginDatabaseConnectionString)
{
}
