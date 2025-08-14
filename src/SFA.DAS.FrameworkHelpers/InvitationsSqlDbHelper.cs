using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public abstract class InvitationsSqlDbHelper(ObjectContext objectContext, string connectionString) : SqlDbHelper(objectContext, connectionString)
{
    public async Task DeleteAspNetUsersTableDataForCMAD(string apprenticeId) => await ExecuteSqlCommand($"DELETE FROM [IdentityServer].[aspnetusers] where ApprenticeId = '{apprenticeId}'");

    public async Task<string> DeleteUserLogsTableData(string email) => await GetNullableData($"DELETE [LoginService].[UserLogs] where email = '{email}'");

    public async Task<string> GetApprenticeIdFromAspNetUsersTable(string email) => await GetDataAsString($"SELECT ApprenticeId FROM [IdentityServer].[AspNetUsers] WHERE Email = '{email}'");
}
