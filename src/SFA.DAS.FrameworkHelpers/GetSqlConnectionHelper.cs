using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public static class GetSqlConnectionHelper
{
    internal static async Task<SqlConnection> GetSqlConnection(string connectionString)
    {
        return new() { ConnectionString = connectionString, AccessToken = connectionString.Contains("User ID=") ? null : await AzureTokenService.GetDatabaseAuthToken() };
    }
}