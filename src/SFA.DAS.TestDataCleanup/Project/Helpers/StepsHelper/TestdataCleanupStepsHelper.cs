namespace SFA.DAS.TestDataCleanup.Project.Helpers.StepsHelper;

public class TestdataCleanupStepsHelper(ScenarioContext context) : TestdataCleanupStepsHelperBase(context)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    public async Task CleanUpAllDbTestData(HashSet<string> email) => await ReportTestDataCleanUp(async () => await new AllDbTestDataCleanUpHelper(_objectContext, _dbConfig).CleanUpAllDbTestData([.. email]));

    public async Task CleanUpAllDbTestData(string email) => await ReportTestDataCleanUp(async () => await new AllDbTestDataCleanUpHelper(_objectContext, _dbConfig).CleanUpAllDbTestData(email));

    public async Task CleanUpComtTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpComtTestData(); });

    public async Task CleanUpPrelTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpPrelTestData(); });

    public async Task CleanUpPfbeTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpPfbeTestData(); });

    public async Task CleanUpEmpFcastTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpEmpFcastTestData(); });

    public async Task CleanUpEmpFinTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpEmpFinTestData(); });

    public async Task CleanUpRsvrTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpRsvrTestData(); });

    public async Task CleanUpEmpIncTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpEmpIncTestData(); });

    public async Task CleanUpEasLtmTestData(int greaterThan, int lessThan) => await ReportTestDataCleanUp(async () => { var x = await GetCleanUpHelper(greaterThan, lessThan); return await x.CleanUpEasLtmTestData(); });
}
