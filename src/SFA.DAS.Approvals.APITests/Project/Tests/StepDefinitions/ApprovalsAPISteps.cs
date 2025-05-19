using RestSharp;
using SFA.DAS.Approvals.APITests.Project;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;

[Binding]
public class ApprovalsAPISteps(ScenarioContext context)
{
    private readonly Outer_ApprovalsAPIClient _restClient = context.GetRestClient<Outer_ApprovalsAPIClient>();

    private RestResponse _restResponse = null;

    [When(@"the user sends (GET|POST|PUT|DELETE) request to (.*) with payload (.*)")]
    public async Task TheUserSendsRequestTo(Method method, string endpoint, string payload)
    {
        await _restClient.CreateRestRequest(method, endpoint, payload);
    }

    [Then(@"api (OK) response is received")]
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

    private async Task<RestResponse> Execute(HttpStatusCode responseCode) => await _restClient.Execute(responseCode);
}
