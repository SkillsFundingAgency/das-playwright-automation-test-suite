using Azure.Core;
using Azure.Identity;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers
{
    public static class AzureTokenService
    {
        public static async Task<string> GetDatabaseAuthToken(string tenantid) => await GetAzureToken("https://database.windows.net/", tenantid);

        public static async Task<string> GetDatabaseAuthToken() => await GetAzureToken("https://database.windows.net/");

        public static async Task<string> GetAppServiceAuthToken(string resource) => await GetAzureToken(resource);

        private static async Task<string> GetAzureToken(string resource, string tenantid)
        {
            // Create DefaultAzureCredentialOptions and specify the tenant ID
            var options = new DefaultAzureCredentialOptions
            {
                SharedTokenCacheTenantId = tenantid,
                VisualStudioTenantId = tenantid,
                VisualStudioCodeTenantId = tenantid,
                ManagedIdentityClientId = tenantid,
                InteractiveBrowserTenantId = tenantid,
            };

            var tokenCredential = new DefaultAzureCredential(options);

            var accessToken = await tokenCredential.GetTokenAsync(new TokenRequestContext(scopes: new string[] { resource + "/.default" }) { });

            return accessToken.Token;
        }

        private static async Task<string> GetAzureToken(string resource)
        {
            var tokenCredential = new DefaultAzureCredential();

            var accessToken = await tokenCredential.GetTokenAsync(new TokenRequestContext(scopes: new string[] { resource + "/.default" }) { });

            return accessToken.Token;
        }
    }
}