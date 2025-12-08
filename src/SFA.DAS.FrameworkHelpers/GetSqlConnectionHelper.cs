using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public static class GetSqlConnectionHelper
{
    internal static async Task<SqlConnection> GetSqlConnection(string connectionString)
    {

        if (connectionString.Contains("Authentication=", StringComparison.OrdinalIgnoreCase))
        {
            return new SqlConnection(connectionString);
        }

        var tenantidkey = "TENANTID=";

        string accessToken;

        if (connectionString.Contains("User ID="))
        {
            accessToken = null;
        }
        else
        {
            if (connectionString.Contains(tenantidkey))
            {
                var tenantidwithKey = connectionString.Split(';').ToList().Single(x => x.StartsWith(tenantidkey, StringComparison.OrdinalIgnoreCase));

                connectionString = connectionString.Replace(tenantidwithKey, string.Empty);

                var tenantid = tenantidwithKey.Replace(tenantidkey, string.Empty);

                accessToken = await AzureTokenService.GetDatabaseAuthToken(tenantid);
            }
            else
            {
                accessToken = await AzureTokenService.GetDatabaseAuthToken();
            }
        }

        return new() { ConnectionString = connectionString, AccessToken = accessToken };
    }
}
