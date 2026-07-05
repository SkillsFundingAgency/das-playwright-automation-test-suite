using Azure.Core;
using Azure.Identity;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers
{
    public static class AzureTokenService
    {
        public static async Task<string> GetDatabaseAuthToken(string tenantid) => await GetAzureToken("https://database.windows.net/", tenantid);

        public static async Task<string> GetDatabaseAuthToken() => await GetAzureToken("https://database.windows.net/");

        public static async Task<string> GetAppServiceAuthToken(string resource, string tenantid) => await GetAzureToken(resource, tenantid);

        private static async Task<string> GetAzureToken(string resource, string tenantid)
        {
            if (!TestPlatformFinder.IsAdoExecution && RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return await GetTokenViaAzCli(resource, tenantid);

            var options = new DefaultAzureCredentialOptions
            {
                SharedTokenCacheTenantId = tenantid,
                VisualStudioTenantId = tenantid,
                VisualStudioCodeTenantId = tenantid,
                InteractiveBrowserTenantId = tenantid,
            };
            var tokenCredential = new DefaultAzureCredential(options);
            var accessToken = await tokenCredential.GetTokenAsync(new TokenRequestContext(scopes: new string[] { resource + "/.default" }) { });
            return accessToken.Token;
        }

        private static async Task<string> GetAzureToken(string resource)
        {
            if (!TestPlatformFinder.IsAdoExecution && RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return await GetTokenViaAzCli(resource, null);

            var tokenCredential = new DefaultAzureCredential();
            var accessToken = await tokenCredential.GetTokenAsync(new TokenRequestContext(scopes: new string[] { resource + "/.default" }) { });
            return accessToken.Token;
        }

        private static async Task<string> GetTokenViaAzCli(string resource, string tenantid)
        {
            var args = $"account get-access-token --resource {resource} --output json";
            if (!string.IsNullOrEmpty(tenantid))
                args += $" --tenant {tenantid}";

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/usr/local/bin/az",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            var stdout = await proc.StandardOutput.ReadToEndAsync();
            var stderr = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            if (proc.ExitCode != 0)
                throw new InvalidOperationException($"az CLI failed (exit {proc.ExitCode}): {stderr}");

            using var doc = JsonDocument.Parse(stdout);
            return doc.RootElement.GetProperty("accessToken").GetString()!;
        }
    }
}
