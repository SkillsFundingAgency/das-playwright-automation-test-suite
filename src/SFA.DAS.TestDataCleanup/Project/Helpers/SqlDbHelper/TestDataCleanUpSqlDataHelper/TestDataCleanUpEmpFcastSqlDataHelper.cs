namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpEmpFcastSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.FcastDbConnectionString)
{
    public override string SqlFileName => "EasFcastTestDataCleanUp";

    public async Task<(List<string>, List<string>)> CleanUpEmpFcastTestData(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await CleanUpTestData(async () => await GetEmpFcastAccountids(greaterThan, lessThan, easaccountidsnottodelete), CleanUpEmpFcastTestData);
    }

    internal async Task<int> CleanUpEmpFcastTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetEmpFcastAccountids(int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        return await GetAccountids($"select distinct EmployerAccountId from dbo.Commitment where EmployerAccountId > {greaterThan} and EmployerAccountId < {lessThan} and EmployerAccountId not in ({string.Join(",", easaccountidsnottodelete)}) order by EmployerAccountId desc");
    }
}
