using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SFA.DAS.FrameworkHelpers
{
    public static class GetSqlConnectionHelper
    {
        /// <summary>
        /// Returns a SqlConnection which will either use:
        /// - connection-string authentication (if Authentication= is present), or
        /// - SQL auth (if User ID= is present), or
        /// - Azure AD access token (via AzureTokenService).
        /// </summary>
        internal static async Task<SqlConnection> GetSqlConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string must not be null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"[GetSqlConnection] Original connection string: {MaskSensitiveParts(connectionString)}");

            // 1. If connection string has Authentication specified, don't set AccessToken
            if (connectionString.Contains("Authentication=", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[GetSqlConnection] Using connection-string authentication (Authentication= present).");
                return new SqlConnection(connectionString);
            }

            // 2. If connection string has a username, assume SQL auth (User ID / UID / Uid)
            if (connectionString.Contains("User ID=", StringComparison.OrdinalIgnoreCase) ||
                connectionString.Contains("UID=", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[GetSqlConnection] Using SQL authentication (User ID detected in connection string).");
                return new SqlConnection(connectionString);
            }

            // 3. Otherwise, use AAD token auth. Optional TENANTID= support.
            const string tenantIdKey = "TENANTID=";
            string? tenantId = null;

            // Pull tenant from connection string if present
            if (connectionString.IndexOf(tenantIdKey, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var parts = connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(p => p.Trim())
                                            .ToList();

                var tenantPart = parts
                    .SingleOrDefault(p => p.StartsWith(tenantIdKey, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrEmpty(tenantPart))
                {
                    tenantId = tenantPart.Substring(tenantIdKey.Length);

                    // remove TENANTID=... from actual connection string
                    parts.Remove(tenantPart);
                    connectionString = string.Join(";", parts) + ";"; // rebuild with trailing ';'

                    Console.WriteLine($"[GetSqlConnection] Using tenant ID from connection string: {tenantId}");
                }
            }

            if (tenantId == null)
            {
                Console.WriteLine("[GetSqlConnection] No TENANTID= in connection string. Using default tenant for token.");
            }

            var accessToken = await AzureTokenService.GetDatabaseAuthToken(tenantId);

            Console.WriteLine($"[GetSqlConnection] Access token acquired: {(string.IsNullOrEmpty(accessToken) ? "NO" : "YES")}");

            // Create SqlConnection with AccessToken
            var sqlConnection = new SqlConnection
            {
                ConnectionString = connectionString,
                AccessToken = accessToken
            };

            return sqlConnection;
        }

        private static string MaskSensitiveParts(string connectionString)
        {
            // Very light masking of password & secret-ish things for logging
            if (string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            string[] sensitiveKeys =
            {
                "Password", "Pwd", "User ID", "UID", "ClientSecret"
            };

            var parts = connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(p => p.Trim())
                                        .ToArray();

            for (int i = 0; i < parts.Length; i++)
            {
                foreach (var key in sensitiveKeys)
                {
                    if (parts[i].StartsWith(key + "=", StringComparison.OrdinalIgnoreCase))
                    {
                        parts[i] = $"{key}=***";
                        break;
                    }
                }
            }

            return string.Join(";", parts);
        }
    }
}
