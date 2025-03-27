namespace SFA.DAS.TestDataCleanup.Project.Tests.StepDefinitions;

[Binding]
public class TestDataCleanUpWithEmail(ScenarioContext context)
{
    private readonly TestdataCleanupStepsHelper _testDataCleanUpStepsHelper = new(context);

    [Then(@"the test data are cleaned up for email (.*)")]
    public async Task ThenTheTestDataAreCleanedUpForEmail(string email) => await CleanUpTestData(email);

    [Then(@"the test data are cleaned up")]
    public async Task ThenTheTestDataAreCleanedUp() => await CleanUpTestData("NL_Test_106_27Oct2022_%");

    private async Task CleanUpTestData(string email) => await _testDataCleanUpStepsHelper.CleanUpAllDbTestData(email);
}
