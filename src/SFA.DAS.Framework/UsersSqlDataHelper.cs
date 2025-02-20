using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;

namespace SFA.DAS.Framework;

public class UsersSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.UsersDbConnectionString)
{
    public async Task<int> ReinstateAccountInDb(string email) => await ExecuteSqlCommand($"UPDATE [User] SET IsSuspended = 0, LastSuspendedDate = null WHERE Email = '{email}'");

    public async Task<string> GetUserId(string email) => await GetDataAsString($"SELECT Id FROM dbo.[User] WHERE email = '{email}'");
}
