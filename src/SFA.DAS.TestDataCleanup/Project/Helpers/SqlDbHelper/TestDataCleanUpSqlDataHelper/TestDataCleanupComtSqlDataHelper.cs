namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;


public class TestDataCleanupComtSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.CommitmentsDbConnectionString)
{
    public override string SqlFileName => "EasComtTestDataCleanUp";

    internal async Task<List<string[]>> GetApprenticeIds(List<string> accountidsTodelete)
    {
        return await GetMultipleData($"select a.id from Apprenticeship a Join Commitment c on c.id = a.CommitmentId and c.EmployerAccountId in ({string.Join(",", accountidsTodelete)})");
    }

    public async Task<(List<string>, List<string>)> CleanUpComtTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetComtAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpComtTestData);
    }

    internal async Task<int> CleanUpComtTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetComtAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select id from dbo.Accounts where id > {greaterThan} and id < {lessThan} and id not in ({string.Join(",", easaccountidsnottodelete)}) order by id desc");
    }
}
