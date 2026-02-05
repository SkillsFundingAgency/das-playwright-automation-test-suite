namespace SFA.DAS.EmployerAccounts.APITests.Project.Helpers;

public class Inner_EmployerAccountsLegacyApiRestClient(ObjectContext objectContext, Inner_ApiFrameworkConfig config) : Inner_BaseApiRestClient(objectContext, config)
{
    protected override string ApiBaseUrl => UrlConfig.InnerApiUrlConfig.Inner_EmployerAccountsLegacyApiBaseUrl;

    protected override string AppServiceName => $"{config.config.EmployerAccountsLegacyAppServiceName}";

    public async Task<RestResponse> ExecuteEndpoint(string endpoint, HttpStatusCode expectedResponse)
    {
        return await Execute(Method.Get, endpoint, string.Empty, expectedResponse);
    }

    public async Task<RestResponse> ExecuteEndpoint(string endpoint)
    {
        return await ExecuteEndpoint(endpoint, HttpStatusCode.OK);
    }
}
