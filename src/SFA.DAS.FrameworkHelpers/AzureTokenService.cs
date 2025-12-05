using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;

namespace SFA.DAS.FrameworkHelpers
{
    public static class AzureTokenService
    {
        private const string SqlResource = "https://database.windows.net/";

        public static Task<string> GetDatabaseAuthToken(string tenantid) =>
            GetAzureToken(SqlResource, tenantid);

        public static Task<string> GetDatabaseAuthToken() =>
            GetAzureToken(SqlResource);

        public static Task<string> GetAppServiceAuthToken(string resource, string tenantid) =>
            GetAzureToken(resource, tenantid);

        private static async Task<string> GetAzureToken(string resource, string tenantid)
        {
            Console.WriteLine($"[AzureTokenService] Getting token for resource '{resource}' with explicit tenant '{tenantid}'");
            Console.WriteLine($"[AzureTokenService] Env AZURE_CLIENT_ID={Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")}");
            Console.WriteLine($"[AzureTokenService] Env AZURE_TENANT_ID={Environment.GetEnvironmentVariable("AZURE_TENANT_ID")}");

            var options = new DefaultAzureCredentialOptions
            {
                // This is the only one that actually matters for forcing tenant
                TenantId = tenantid
            };

            var tokenCredential = new DefaultAzureCredential(options);

            var context = new TokenRequestContext(new[] { resource + "/.default" });

            AccessToken accessToken;
            try
            {
                accessToken = await tokenCredential.GetTokenAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AzureTokenService] ERROR acquiring token for tenant {tenantid}: {ex}");
                throw;
            }

            LogTokenClaims(accessToken.Token);

            return accessToken.Token;
        }

        private static async Task<string> GetAzureToken(string resource)
        {
            Console.WriteLine($"[AzureTokenService] Getting token for resource '{resource}' with default tenant");
            Console.WriteLine($"[AzureTokenService] Env AZURE_CLIENT_ID={Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")}");
            Console.WriteLine($"[AzureTokenService] Env AZURE_TENANT_ID={Environment.GetEnvironmentVariable("AZURE_TENANT_ID")}");

            var tokenCredential = new DefaultAzureCredential();
            var context = new TokenRequestContext(new[] { resource + "/.default" });

            AccessToken accessToken;
            try
            {
                accessToken = await tokenCredential.GetTokenAsync(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AzureTokenService] ERROR acquiring token (default tenant): {ex}");
                throw;
            }

            LogTokenClaims(accessToken.Token);

            return accessToken.Token;
        }

        /// <summary>
        /// Debug helper: logs key claims so you can verify which principal is used.
        /// </summary>
        private static void LogTokenClaims(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(jwtToken);

                string? appId = jwt.Claims.FirstOrDefault(c => c.Type == "appid")?.Value;
                string? oid   = jwt.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
                string? tid   = jwt.Claims.FirstOrDefault(c => c.Type == "tid")?.Value;
                string? upn   = jwt.Claims.FirstOrDefault(c => c.Type == "upn")?.Value;

                Console.WriteLine("[AzureTokenService] Token claims:");
                Console.WriteLine($"    appid (client id): {appId}");
                Console.WriteLine($"    oid   (object id): {oid}");
                Console.WriteLine($"    tid   (tenant id): {tid}");
                Console.WriteLine($"    upn/user:          {upn}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AzureTokenService] Failed to decode JWT for logging: {ex.Message}");
            }
        }
    }
}
