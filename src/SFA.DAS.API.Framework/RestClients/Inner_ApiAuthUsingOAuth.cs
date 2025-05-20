using SFA.DAS.API.Framework.Helpers;
using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public class Inner_ApiAuthUsingOAuth(Inner_ApiFrameworkConfig config, ObjectContext objectContext) : IInner_ApiGetAuthToken
{
    private RestClient _restClient;

    private RestRequest _restRequest;

    public async Task<(string tokenType, string accessToken)> GetAuthToken(string appServiceName)
    {
        CreateInnerApiAuthTokenRestClient();

        _restRequest.Method = Method.Post;

        _restRequest.AddHeader("content-type", "application/x-www-form-urlencoded");

        var authParameter = new InnerApiOAuthModel(config.config.ClientId, config.config.ClientSecrets, config.GetResource(appServiceName));

        _restRequest.AddParameter("application/x-www-form-urlencoded", authParameter.ToString(), ParameterType.RequestBody);

        var response = await new InnerApiAuthTokenAssertHelper(objectContext).ExecuteInnerApiAuthTokenAndAssertResponse(_restClient, _restRequest);

        AuthTokenResponse authToken = JsonConvert.DeserializeObject<AuthTokenResponse>(response.Content);

        return (authToken.Token_type, authToken.Access_token);
    }

    private void CreateInnerApiAuthTokenRestClient()
    {
        _restClient = new RestClient(UrlConfig.InnerApiUrlConfig.MangeIdentitybaseUrl(config.config.TenantId));

        _restRequest = new RestRequest();
    }
}
