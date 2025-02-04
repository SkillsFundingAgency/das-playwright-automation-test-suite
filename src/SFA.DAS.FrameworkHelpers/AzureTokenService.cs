using Azure.Core;
using Azure.Identity;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers
{
    public static class AzureTokenService
    {
        public static async Task<string> GetDatabaseAuthToken() => await GetAzureToken("https://database.windows.net/");

        public static async Task<string> GetAppServiceAuthToken(string resource) => await GetAzureToken(resource);

        private static async Task<string> GetAzureToken(string resource)
        {
            var tokenCredential = new DefaultAzureCredential();

            var accessToken = await tokenCredential.GetTokenAsync(new TokenRequestContext(scopes: [resource + "/.default"]) { });

            return accessToken.Token;
        }
    }
}