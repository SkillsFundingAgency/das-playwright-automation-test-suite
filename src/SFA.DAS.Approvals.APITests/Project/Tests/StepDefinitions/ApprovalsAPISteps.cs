using Polly;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;

[Binding]
public class ApprovalsAPISteps
{
    private readonly ScenarioContext _context;
    private readonly Outer_ApprovalsAPIClient _restClient;
    private RestResponse _restResponse = null;
    
    public ApprovalsAPISteps(ScenarioContext context)
    {
        _context = context;
        _restClient = _context.GetRestClient<Outer_ApprovalsAPIClient>();
    }

    [When(@"the user sends (GET|POST|PUT|DELETE) request to (.*) with payload (.*)")]
    public async Task TheUserSendsRequestTo(Method method, string endpoint, string payload)
    {
        await _restClient.CreateRestRequest(method, endpoint, payload);
    }

    [Then(@"api (Accepted) response is received")]
    public async Task ThenApiOKResponseIsReceived(HttpStatusCode responseCode)
    {
        _restResponse = await Execute(responseCode);
    }

    [Then(@"api (Created) response is received")]
    public async Task ThenApiCreatedResponseIsReceived(HttpStatusCode responseCode)
    {
        _restResponse = await Execute(responseCode);
    }

    [Then(@"api (Bad Request) response is received")]
    public async Task ThenApiBadRequestResponseIsReceived(HttpStatusCode responseCode)
    {
        _restResponse = await Execute(responseCode);
    }

    public async Task<RestResponse> SLDPushDataToAS(string resource, string payload)
    {
        await _restClient.CreateRestRequest(Method.Put, resource, payload);
        _restResponse = await Execute(HttpStatusCode.Accepted);
        return _restResponse;
    }

    

    private async Task<RestResponse> Execute(HttpStatusCode responseCode) => await _restClient.Execute(responseCode);
}
