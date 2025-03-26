﻿namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpPsrDbSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.PublicSectorReportingConnectionString)
{
    public override string SqlFileName => "EasPsrTestDataCleanUp";

    private readonly ObjectContext _objectContext = objectContext;

    internal async Task<int> CleanUpPsrTestData(List<string> accountIdToDelete)
    {
        var easaccounthashedids = await new TestDataCleanUpEasAccDbSqlDataHelper(_objectContext, dbConfig).GetAccountHashedIds(accountIdToDelete);

        if (easaccounthashedids.IsNoDataFound()) return 0;

        return await CleanUpTestData(easaccounthashedids.ListOfArrayToList(0), (x) => $"Insert into #accounthashedids values ('{x}')", "create table #accounthashedids (id nvarchar(255))");
    }
}
