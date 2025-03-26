namespace SFA.DAS.TestDataCleanup.Project.Tests.StepDefinitions;

[Binding]
public class TestDataCleanUpWithEmail(ScenarioContext context)
{
    private readonly TestdataCleanupStepsHelper _testDataCleanUpStepsHelper = new(context);

    [Then(@"the test data are cleaned up for email (.*)")]
    public async Task ThenTheTestDataAreCleanedUpForEmail(string email) => await CleanUpTestData(email);

    [Then(@"the test data are cleaned up")]
    public async Task ThenTheTestDataAreCleanedUp() => await CleanUpTestData("AP_Test_101_21Nov2019%");

    private async Task CleanUpTestData(string email) => await _testDataCleanUpStepsHelper.CleanUpAllDbTestData(email);
}
