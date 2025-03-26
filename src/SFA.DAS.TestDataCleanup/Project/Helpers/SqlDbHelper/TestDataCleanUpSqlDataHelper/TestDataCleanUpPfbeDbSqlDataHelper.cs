﻿namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpPfbeDbSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.EmployerFeedbackDbConnectionString)
{
    public override string SqlFileName => "EasPfbeTestDataCleanUp";

    public async Task<(List<string>, List<string>)> CleanUpPfbeTestData(int greaterThan, int lessThan, List<string> easaccountids)
    {
        return await CleanUpTestData(async () => await GetPfbeAccountids(greaterThan, lessThan, easaccountids), CleanUpPfbeTestData);
    }

    internal async Task<int> CleanUpPfbeTestData(List<string> accountIdToDelete) => await CleanUpUsingAccountIds(accountIdToDelete);

    private async Task<List<string>> GetPfbeAccountids(int greaterThan, int lessThan, List<string> easaccountids)
    {
        return await GetAccountids($"select AccountId from dbo.EmployerEmailDetails where AccountId > {greaterThan} and AccountId < {lessThan} and AccountId not in ({string.Join(",", easaccountids)}) order by AccountId desc");
    }
}
