namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpRsvrSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.ReservationsDbConnectionString)
{
    public override string SqlFileName => "EasRsrvTestDataCleanUp";

    public async Task<(List<string>, List<string>)> CleanUpRsvrTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetRsvrAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpRsvrTestData);
    }

    internal async Task<int> CleanUpRsvrTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetRsvrAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select id from dbo.Account where id > {greaterThan} and id < {lessThan} and id not in ({string.Join(",", easaccountidsnottodelete)}) order by id desc");
    }
}
