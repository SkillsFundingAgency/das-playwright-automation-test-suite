using SFA.DAS.API.Framework.Helpers;
using System.Threading.Tasks;

namespace SFA.DAS.API.Framework.RestClients;

public abstract class BaseApiRestClient
{
    protected RestClient restClient;

    protected RestRequest restRequest;

    protected readonly ObjectContext objectContext;

    public BaseApiRestClient(ObjectContext objectContext)
    {
        this.objectContext = objectContext;

        restClient = new(GetClientOptions());

        restRequest = new RestRequest();
    }

    protected RestClientOptions GetClientOptions() => new(ApiBaseUrl);

    protected abstract string ApiBaseUrl { get; }

    protected abstract void AddResource(string resource);

    protected abstract Task AddAuthHeaders();

    protected virtual void AddParameter() { }

    public async Task CreateRestRequest(Method method, string resource, string payload, Dictionary<string, string> payloadreplacement)
    {
        await AddRestRequest(method, resource);

        AddPayload(payload, payloadreplacement);
    }

    public async Task CreateRestRequest(Method method, string resource, string payload)
    {
        await AddRestRequest(method, resource);

        AddPayload(payload);
    }

    public async Task<RestResponse> Execute(HttpStatusCode expectedResponse) => await Execute(expectedResponse, string.Empty);

    public async Task<RestResponse> Execute(HttpStatusCode expectedResponse, string resourceContent) => await new ApiAssertHelper(objectContext).ExecuteAndAssertResponse(expectedResponse, resourceContent, restClient, restRequest);

    protected async Task<RestResponse> Execute<T>(Method method, string resource, T payload, HttpStatusCode expectedResponse)
    {
        await CreateRestRequest(method, resource, JsonHelper.Serialize(payload));

        return await Execute(expectedResponse);
    }

    protected async Task<RestResponse> Execute(string resource, HttpStatusCode expectedResponse) => await Execute(Method.Get, resource, string.Empty, expectedResponse);

    protected void Addheader(string key, string value) => restRequest.AddHeader(key, value);

    protected void Addheaders(Dictionary<string, string> dictionary)
    {
        foreach (var item in dictionary) Addheader(item.Key, item.Value);
    }

    private async Task AddRestRequest(Method method, string resource)
    {
        restRequest.Method = method;

        AddResource(resource);

        foreach (var item in restRequest.Parameters.GetParameters(ParameterType.RequestBody)) restRequest.Parameters.RemoveParameter(item);

        AddParameter();

        await AddAuthHeaders();
    }

    private void AddPayload(string payload)
    {
        if (ShouldAddPayload(payload))
        {
            if (payload.EndsWith(".json")) restRequest.AddJsonBody(JsonHelper.ReadAllText(payload));
            else restRequest.AddJsonBody(payload);
        }
    }

    private void AddPayload(string payload, Dictionary<string, string> payloadreplacement)
    {
        if (ShouldAddPayload(payload)) restRequest.AddJsonBody(ReadAllText(payload, payloadreplacement));
    }

    private string ReadAllText(string payload, Dictionary<string, string> payloadreplacement)
    {
        objectContext.SetDebugInformation($"payload before Transformation: {JsonHelper.ReadAllText(payload)}");

        foreach (var item in payloadreplacement) objectContext.SetDebugInformation($"payload replacement Key: '<{item.Key}>' value: '{item.Value}'");

        string text = JsonHelper.ReadAllText(payload, payloadreplacement);

        objectContext.SetDebugInformation($"payload after Transformation: {text}");

        return text;
    }

    private bool ShouldAddPayload(string payload) => restRequest.Method != Method.Get && !string.IsNullOrEmpty(payload);
}
