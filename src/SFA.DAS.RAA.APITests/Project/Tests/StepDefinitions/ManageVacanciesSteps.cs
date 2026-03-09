using SFA.DAS.Framework;
using SFA.DAS.RAA.DataGenerator.Project;
using SFA.DAS.RAA.DataGenerator.Project.Helpers;
using System;

namespace SFA.DAS.RAA.APITests.Project.Tests.StepDefinitions;

[Binding]
public class ManageVacanciesSteps
{
    private readonly ScenarioContext _scenarioContext;
    private Outer_ManageVacancyApiClient _restClient;
    private readonly ObjectContext _objectContext;
    private RestResponse _apiResponse;
    protected readonly VacancyTitleDatahelper vacancyTitleDataHelper;
    protected readonly RAADataHelper rAADataHelper;

    public ManageVacanciesSteps(ScenarioContext context)
    {
        _scenarioContext = context;
        _objectContext = context.Get<ObjectContext>();
        vacancyTitleDataHelper = context.GetValue<VacancyTitleDatahelper>();
        rAADataHelper = context.GetValue<RAADataHelper>();
    }

    [When(@"the user sends (POST) request to (.*) with payload (.*)")]
    public async Task TheUserSendsRequestTo(Method method, string endpoint, string payload)
    {
        bool isEmployer = _scenarioContext.ScenarioInfo.Tags
            .Any(t => string.Equals(t, "raaapiemployer", StringComparison.OrdinalIgnoreCase)
            || string.Equals(t, "raaemployer", StringComparison.OrdinalIgnoreCase));

        bool isUIScenario = _scenarioContext.ScenarioInfo.Tags.Contains("raaemployer");

        Raa_Outer_ApiAuthTokenConfig apiAuthTokenConfig = isEmployer
            ? _scenarioContext.Get<Raa_Emp_Outer_ApiAuthTokenConfig>()
            : _scenarioContext.Get<Raa_Pro_Outer_ApiAuthTokenConfig>();

        (string msg, string apiKey) = _scenarioContext.ScenarioInfo.Tags.Contains("invalidapikey") ? ("An invalid", apiAuthTokenConfig.InvalidApiKey) : ("A valid", apiAuthTokenConfig.ApiKey);

        string hashedid = apiAuthTokenConfig.Hashed_AccountId;

        string ukprn = apiAuthTokenConfig.Ukprn;

        string vacancyTitle = rAADataHelper.VacancyTitle;

        string additionalQuestion1 = isUIScenario? _scenarioContext.Get<AdvertDataHelper>().AdditionalQuestion1: "Do you have a driving license?";

        string additionalQuestion2 = isUIScenario ? _scenarioContext.Get<AdvertDataHelper>().AdditionalQuestion2: "What interests you about this apprenticeship?";

        _objectContext.SetDebugInformation($"'{msg}' Api key - '{apiKey}' is used to create vacancy for hashedid - '{hashedid}'and ukprn - '{ukprn}'");

        _restClient = new Outer_ManageVacancyApiClient(_objectContext, apiKey);

        var dynamicGuid = Guid.NewGuid().ToString();

        var payloadreplacement = new Dictionary<string, string>
        {
            { "ukprn", ukprn},
            { "hashedid", hashedid},
            { "vacancyTitle", vacancyTitle },
            { "additionalQuestion1", additionalQuestion1 },
            { "additionalQuestion2", additionalQuestion2 }
        };

        await _restClient.CreateRestRequest(method, $"/managevacancies/{endpoint}/{dynamicGuid}", payload, payloadreplacement);
    }

    [Then(@"a (Created|Unauthorized) response is received")]
    public async Task ThenAOKResponseIsReceived(HttpStatusCode responseCode)
    {
        _apiResponse = await _restClient.Execute(responseCode);

        if (!string.IsNullOrEmpty(_apiResponse?.Content))
        {
                using var doc = System.Text.Json.JsonDocument.Parse(_apiResponse.Content);

                if (doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Object &&
                    doc.RootElement.TryGetProperty("vacancyReference", out var vacancyRefElement))
                {
                    var vacancyRef = vacancyRefElement.GetString();
                    if (!string.IsNullOrEmpty(vacancyRef))
                    {
                        _objectContext.SetVacancyReference(vacancyRef);
                    }
                }  
        }
    }

    [Then(@"verify response body displays vacancy reference number")]
    public void ThenVerifyResponseBodyDisplaysVacancyReferenceNumber()
    {
        var expected = "vacancyReference";
        StringAssert.Contains(expected, _apiResponse.Content);
    }

    [Then("verify response body displays Access denied due to invalid subscription key")]
    public async Task ThenVerifyResponseBodyDisplaysAccessDeniedDueToInvalidSubscriptionKey()
    {
        var expected = "Access denied due to invalid subscription key";
        StringAssert.Contains(expected, _apiResponse.Content);
    }
}
