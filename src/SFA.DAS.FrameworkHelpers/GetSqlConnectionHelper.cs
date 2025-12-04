using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public static class GetSqlConnectionHelper
{
    internal static async Task<SqlConnection> GetSqlConnection(string connectionString)
    {
    // If connection string has Authentication specified, don't set AccessToken
    if (connectionString.Contains("Authentication=", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Using connection string authentication");
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

            Console.WriteLine($"Using tenant ID from connection string: {tenantid}");
            accessToken = await AzureTokenService.GetDatabaseAuthToken(tenantid);
        }
        else
        {
            Console.WriteLine($"Using default tenant for token");
            accessToken = await AzureTokenService.GetDatabaseAuthToken();
        }

        Console.WriteLine($"Access token acquired: {(string.IsNullOrEmpty(accessToken) ? "NO" : "YES")}");
    }

    return new() { ConnectionString = connectionString, AccessToken = accessToken };
    }
}
