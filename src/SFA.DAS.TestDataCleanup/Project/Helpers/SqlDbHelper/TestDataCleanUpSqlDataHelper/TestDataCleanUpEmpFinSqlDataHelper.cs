namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpEmpFinSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.FinanceDbConnectionString)
{
    public override string SqlFileName => "EasFinTestDataCleanUp";

    public async Task<(List<string>, List<string>)> CleanUpEmpFinTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetEmpFinAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpEmpFinTestData);
    }

    internal async Task<int> CleanUpEmpFinTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetEmpFinAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select id from employer_financial.Account where id > {greaterThan} and id < {lessThan} and id not in ({string.Join(",", easaccountidsnottodelete)}) order by id desc");
    }
}
