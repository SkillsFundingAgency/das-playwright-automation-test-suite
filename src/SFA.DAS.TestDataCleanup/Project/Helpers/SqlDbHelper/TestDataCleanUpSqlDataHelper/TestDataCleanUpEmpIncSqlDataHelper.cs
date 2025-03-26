namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpEmpIncSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.IncentivesDbConnectionString)
{
    public override string SqlFileName => "EmpIncTestDataCleanUp";

    public override bool ExcludeEnvironments => EnvironmentConfig.IsDemoEnvironment;

    public async Task<(List<string>, List<string>)> CleanUpEmpIncTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetEmpIncAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpEmpIncTestData);
    }

    internal async Task<int> CleanUpEmpIncTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetEmpIncAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select id from dbo.Accounts where id > {greaterThan} and id < {lessThan} and id not in ({string.Join(",", easaccountidsnottodelete)}) order by id desc");
    }
}
