namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.BaseSqlDbHelper;

public abstract class TestDataCleanUpSqlDataHelper(ObjectContext objectContext, string connectionString) : ProjectSqlDbHelper(objectContext, connectionString)
{
    public abstract string SqlFileName { get; }

    protected async Task<int> CleanUpUsingEmail(List<string> emailsToDelete) => await CleanUpTestData(emailsToDelete, (x) => $"Insert into #emails values ('{x}')", "create table #emails (email varchar(255))");

    protected async Task<int> CleanUpUsingAccountIds(List<string> accountIdToDelete) => await CleanUpTestData(accountIdToDelete, (x) => $"Insert into #accountids values ({x})", "create table #accountids (id bigint)");

    protected async Task<int> CleanUpUsingCommtApprenticeshipIds(List<string[]> commtApprenticeshipIdsToDelete)
    {
        if (commtApprenticeshipIdsToDelete.IsNoDataFound()) return 0;

        return await CleanUpTestData(commtApprenticeshipIdsToDelete.ListOfArrayToList(0), (x) => $"Insert into #commitmentsapprenticeshipid values ({x})", "create table #commitmentsapprenticeshipid (id bigint)");
    }

    protected async Task<int> CleanUpTestData(List<string> listToDelete, Func<string, string> insertQueryFunc, string createQuery)
    {
        if (ExcludeEnvironments) return 0;

        var insertquery = listToDelete.Select(x => insertQueryFunc(x)).ToList();

        var sqlQuery = $"{createQuery};{insertquery.ToString(";")};" + FileHelper.GetSql(SqlFileName);

        var noOfRowsDeleted = await ReTryExecuteSqlCommand(sqlQuery) - listToDelete.Count;

        return noOfRowsDeleted;
    }

    protected async Task<(List<string>, List<string>)> CleanUpTestData(Func<Task<List<string>>> getAccountidfunc, Func<List<string>, Task<int>> deleteAccountidfunc)
    {
        List<string> accountIdToDelete = [];

        List<string> accountIdNotDeleted = [];

        if (ExcludeEnvironments) return (accountIdToDelete, accountIdNotDeleted);

        try
        {
            accountIdToDelete = await getAccountidfunc.Invoke();

            if (accountIdToDelete.IsNoDataFound()) return ([], []);

            await deleteAccountidfunc(accountIdToDelete);

        }
        catch (Exception ex)
        {
            accountIdNotDeleted.Add($"({SqlFileName}){Environment.NewLine}{ex.Message}");
        }
        finally
        {
            var accountIdNotDeletedindb = await getAccountidfunc.Invoke();

            if (!accountIdNotDeletedindb.IsNoDataFound()) accountIdNotDeleted.AddRange(accountIdNotDeletedindb);
        }

        return (accountIdToDelete.Except(accountIdNotDeleted).ToList(), accountIdNotDeleted);
    }
}
