﻿namespace SFA.DAS.TestDataCleanup.Project.Helpers.StepsHelper;

public class TestdataCleanupWithAccountIdStepsHelper
{
    private readonly DbConfig _dbConfig;
    private readonly int _greaterThan;
    private readonly int _lessThan;
    private readonly List<string> _easaccountidsnottodelete;
    private readonly ObjectContext _objectContext;

    public TestdataCleanupWithAccountIdStepsHelper(ObjectContext objectContext, DbConfig dbConfig, int greaterThan, int lessThan, List<string> easaccountidsnottodelete)
    {
        _dbConfig = dbConfig;
        _greaterThan = greaterThan;
        _lessThan = lessThan;
        _easaccountidsnottodelete = easaccountidsnottodelete;
        _objectContext = objectContext; ;
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpComtTestData()
    {
        return AddDbName(await new TestDataCleanupComtSqlDataHelper(_objectContext, _dbConfig).CleanUpComtTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "comt");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpPrelTestData()
    {
        return AddDbName(await new TestDataCleanUpPrelDbSqlDataHelper(_objectContext, _dbConfig).CleanUpPrelTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "prel");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpPfbeTestData()
    {
        return AddDbName(await new TestDataCleanUpPfbeDbSqlDataHelper(_objectContext, _dbConfig).CleanUpPfbeTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "pfbe");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpEmpFcastTestData()
    {
        return AddDbName(await new TestDataCleanUpEmpFcastSqlDataHelper(_objectContext, _dbConfig).CleanUpEmpFcastTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "fcast");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpEmpFinTestData()
    {
        return AddDbName(await new TestDataCleanUpEmpFinSqlDataHelper(_objectContext, _dbConfig).CleanUpEmpFinTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "empfin");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpRsvrTestData()
    {
        return AddDbName(await new TestDataCleanUpRsvrSqlDataHelper(_objectContext, _dbConfig).CleanUpRsvrTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "rsvr");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpEmpIncTestData()
    {
        return AddDbName(await new TestDataCleanUpEmpIncSqlDataHelper(_objectContext, _dbConfig).CleanUpEmpIncTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "empinc");
    }

    internal async Task<(List<string> usersdeleted, List<string> userswithconstraints)> CleanUpEasLtmTestData()
    {
        return AddDbName(await new TestDataCleanUpEasLtmcSqlDataHelper(_objectContext, _dbConfig).CleanUpEasLtmTestData(_greaterThan, _lessThan, _easaccountidsnottodelete), "easltm");
    }

    private static (List<string> usersdeleted, List<string> userswithconstraints) AddDbName((List<string>, List<string>) users, string dbname)
    {
        List<string> x(List<string> users, string dbname) => users.Select(x => $"{x},{dbname}").ToList();

        return (x(users.Item1, dbname), x(users.Item2, dbname));
    }
}
