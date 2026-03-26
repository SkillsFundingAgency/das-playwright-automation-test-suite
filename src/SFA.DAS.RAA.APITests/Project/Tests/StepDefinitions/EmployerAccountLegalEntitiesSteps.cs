
namespace SFA.DAS.RAA.APITests.Project.Tests.StepDefinitions;

[Binding]
public class EmployerAccountLegalEntitiesSteps
{
    private readonly ScenarioContext _context;
    private readonly Outer_RecruitApiClient _restClient;
    private readonly EmployerLegalEntitiesSqlDbHelper _employerLegalEntitiesSqlHelper;
    private string _accountId;
    private string _expected;
    private RestResponse _apiResponse;

    public EmployerAccountLegalEntitiesSteps(ScenarioContext context)
    {
        _context = context;
        _restClient = _context.GetRestClient<Outer_RecruitApiClient>();
        _employerLegalEntitiesSqlHelper = _context.Get<EmployerLegalEntitiesSqlDbHelper>();
    }

    [Given(@"user prepares request with Employer ID")]
    public async Task GivenUserPreparesRequestWithEmployerId()
        => (_accountId, _expected) = await _employerLegalEntitiesSqlHelper.GetEmployerAccountDetails();

    [When(@"the user sends (GET) request to (.*)")]
    public async Task WhenTheUserSendsGETRequestToVacanciesEmployeraccountlegalentities(Method method, string endpoint)
    {
        await _restClient.CreateRestRequest(method, endpoint.Replace("{hashedAccountId}", _accountId), null);
    }

    [Then(@"a (OK) response is received")]
    public async Task ThenAOKResponseIsReceived(HttpStatusCode responseCode)
    {
        _apiResponse = await _restClient.Execute(responseCode);
    }

    [Then(@"verify response body displays correct information")]
    public async Task ThenVerifyResponseBodyDisplaysCorrectInformation()
        => StringAssert.Contains(_expected, _apiResponse.Content);

}
