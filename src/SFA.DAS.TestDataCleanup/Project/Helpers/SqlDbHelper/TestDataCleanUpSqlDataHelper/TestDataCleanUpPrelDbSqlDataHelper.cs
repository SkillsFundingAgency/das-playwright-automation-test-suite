namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpPrelDbSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.PermissionsDbConnectionString)
{
    public override string SqlFileName => "EasPrelTestDataCleanUp";

    public async Task<(List<string>, List<string>)> CleanUpPrelTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetPrelAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpPrelTestData);
    }

    internal async Task<int> CleanUpPrelTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetPrelAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select Id from dbo.Accounts where Id > {greaterThan} and id < {lessThan} and Id not in ({string.Join(",", easaccountidsnottodelete)}) order by id desc");
    }
}
