using RestSharp.Authenticators.OAuth2;
using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public abstract class Inner_BaseApiRestClient(ObjectContext objectContext, Inner_ApiFrameworkConfig config) : BaseApiRestClient(objectContext)
{
    protected readonly Inner_ApiFrameworkConfig config = config;

    protected abstract string AppServiceName { get; }

    protected override void AddResource(string resource) => restRequest.Resource = resource;

    protected override async Task AddAuthHeaders()
    {
        var options = GetClientOptions();

        options.Authenticator = await GetAuthenticator();

        restClient = new(options);
    }

    private async Task<OAuth2AuthorizationRequestHeaderAuthenticator> GetAuthenticator()
    {
        (string tokenType, string accessToken) = config.IsVstsExecution ? await GetOAuthToken() : await GetAADAuthToken();

        return new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, tokenType);
    }

    public async Task<(string tokenType, string accessToken)> GetAADAuthToken() => await new Inner_ApiAuthUsingMI(config).GetAuthToken(AppServiceName);

    public async Task<(string tokenType, string accessToken)> GetOAuthToken() => await new Inner_ApiAuthUsingOAuth(config, objectContext).GetAuthToken(AppServiceName);
}
