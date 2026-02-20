using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public static class GetSqlConnectionHelper
{
    internal static async Task<SqlConnection> GetSqlConnection(string connectionString)
    {
        if (connectionString.Contains("User ID="))
        {
            return new SqlConnection(connectionString);
        }

        var parts = connectionString
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Trim())
            .ToList();

        const string tenantIdKey = "TENANTID=";
        const string authKey = "AUTHENTICATION=";

        string tenantId = null;

        for (int i = parts.Count - 1; i >= 0; i--)
        {
            if (parts[i].StartsWith(tenantIdKey, StringComparison.OrdinalIgnoreCase))
            {
                tenantId = parts[i].Substring(tenantIdKey.Length);
                parts.RemoveAt(i);
                break;
            }
        }

        for (int i = parts.Count - 1; i >= 0; i--)
        {
            if (parts[i].StartsWith(authKey, StringComparison.OrdinalIgnoreCase))
            {
                parts.RemoveAt(i);
                break;
            }
        }

        var baseConnectionString = string.Join(";", parts.Where(p => !string.IsNullOrWhiteSpace(p)));

        if (string.IsNullOrWhiteSpace(baseConnectionString))
        {
            throw new ArgumentException("Connection string cannot be empty after processing.", nameof(connectionString));
        }

        var accessToken = tenantId is not null
            ? await AzureTokenService.GetDatabaseAuthToken(tenantId)
            : await AzureTokenService.GetDatabaseAuthToken();

        var sqlConnection = new SqlConnection(baseConnectionString);
        sqlConnection.AccessToken = accessToken;

        return sqlConnection;
    }
}
