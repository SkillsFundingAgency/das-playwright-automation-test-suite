namespace SFA.DAS.Finance.APITests.Project.Helpers;

public class Inner_EmployerFinanceApiRestClient(ObjectContext objectContext, Inner_ApiFrameworkConfig config) : Inner_BaseApiRestClient(objectContext, config)
{
    protected override string ApiBaseUrl => UrlConfig.InnerApiUrlConfig.Inner_EmployerFinanceApiBaseUrl;

    protected override string AppServiceName => $"{config.config.EmployerFinanceAppServiceName}-ar";

    public async Task<RestResponse> ExecuteEndpoint(string endpoint, HttpStatusCode expectedResponse)
    {
        return await Execute(Method.Get, endpoint, string.Empty, expectedResponse);
    }

    public async Task<RestResponse> ExecuteEndpoint(string endpoint)
    {
        return await ExecuteEndpoint(endpoint, HttpStatusCode.OK);
    }

    public async Task<RestResponse> PostPeriodEnds(string payloadContent)
    {
        await CreateRestRequest(Method.Post, "/api/period-ends", payloadContent);
        var response =  await Execute(HttpStatusCode.OK);
        return response;
    }   
}