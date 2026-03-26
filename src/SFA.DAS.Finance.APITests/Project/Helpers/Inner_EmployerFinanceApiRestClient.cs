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
        var (tokenType, accessToken) = await GetAADAuthToken();

        await CreateRestRequest(Method.Post, "/api/period-ends", payloadContent);
        Addheader("Authorization", $"{tokenType} {accessToken}");

        var response = await Execute(HttpStatusCode.OK);
        return response;
    }

    public async Task<RestResponse> PostTransferStaging(string payloadContent)
    {
        await CreateRestRequest(Method.Post, "/api/transfers/staging", payloadContent);
        var response = await Execute(HttpStatusCode.Conflict);
        return response;
    }

    public async Task<RestResponse> PostPaymentsStaging(string payloadContent)
    {
        await CreateRestRequest(Method.Post, "/api/payments/staging", payloadContent);
        var response = await Execute(HttpStatusCode.Conflict);
        return response;
    }

    public async Task<RestResponse> PutPaymentMetaDataStaging(string paymentId, string payloadContent)
    {
        await CreateRestRequest(Method.Put, $"/api/payments/{paymentId}/metadata/staging", payloadContent);
        var response = await Execute(HttpStatusCode.OK);
        return response;
    }

    public async Task<RestResponse> PostEnglishFractions(string payloadContent)
    {
        await CreateRestRequest(Method.Post, "/api/english-fractions", payloadContent);
        var response = await Execute(HttpStatusCode.OK);
        return response;
    }
}