
using System;

namespace SFA.DAS.RAA.APITests.Project.Tests.StepDefinitions;

[Binding]
public class DisplayAdvertSteps
{
    private readonly ScenarioContext _context;
    private readonly ObjectContext _objectContext;
    private Outer_DisplayAdvertApiClient _restClient;
    private RestResponse _apiResponse;

    public DisplayAdvertSteps(ScenarioContext context)
    {
        _context = context;
        _objectContext = context.Get<ObjectContext>();
    }

    [When(@"the user sends a (GET) request to (.*)")]
    public async Task TheUserSendsAGetRequestTo(Method method, string endpoint)
    {
        Raa_Outer_ApiAuthTokenConfig apiAuthTokenConfig = _context.ScenarioInfo.Tags.Contains("raaapiemployer")
            ? (Raa_Outer_ApiAuthTokenConfig)_context.Get<Raa_Emp_Outer_ApiAuthTokenConfig>()
            : (Raa_Outer_ApiAuthTokenConfig)_context.Get<Raa_Pro_Outer_ApiAuthTokenConfig>();

        (string msg, string apiKey) = _context.ScenarioInfo.Tags.Contains("invalidapikey") ? ("An invalid", apiAuthTokenConfig.InvalidApiKey) : ("A valid", apiAuthTokenConfig.DisplayAdvertApiKey);

        _restClient = new Outer_DisplayAdvertApiClient(_objectContext, apiKey);

        if (endpoint.Contains("vacancyref", StringComparison.OrdinalIgnoreCase))
        {
            endpoint = endpoint.Replace("vacancyref", apiAuthTokenConfig.VacancyReference, StringComparison.OrdinalIgnoreCase);
        }

        await _restClient.CreateRestRequest(method, endpoint, null);
    }

    [Then(@"a (OK|Unauthorized) response status is received")]
    public async Task ThenAOKResponseIsReceived(HttpStatusCode responseCode)
    {
        _apiResponse = await _restClient.Execute(responseCode);
    }

    [Then(@"verify response body displays (.*) data")]
    public void ThenVerifyResponseBodyDisplaysVacancyInformation(string expected)
    {
        StringAssert.Contains(expected, _apiResponse.Content);
    }

    [Then("verify response body displays Access denied due to invalid key")]
    public void ThenVerifyResponseBodyDisplaysAccessDenied()
    {
        var expected = "Access denied due to invalid subscription key";
        StringAssert.Contains(expected, _apiResponse.Content);
    }
}
