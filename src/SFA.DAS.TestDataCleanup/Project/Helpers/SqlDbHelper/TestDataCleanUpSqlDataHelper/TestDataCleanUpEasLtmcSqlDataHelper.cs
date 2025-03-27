namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpEasLtmcSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.TMDbConnectionString)
{
    public override string SqlFileName => "EasLtmTestDataCleanUp";

    public async Task<(List<string>, List<string>)> CleanUpEasLtmTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetEasLtmAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpEasLtmTestData);
    }

    internal async Task<int> CleanUpEasLtmTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetEasLtmAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select id from dbo.EmployerAccount where id > {greaterThan} and id < {lessThan} and id not in ({string.Join(",", easaccountidsnottodelete)}) order by id desc");
    }
}
