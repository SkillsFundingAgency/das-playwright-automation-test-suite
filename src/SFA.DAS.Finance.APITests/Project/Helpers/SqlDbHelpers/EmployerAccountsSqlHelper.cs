namespace SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;

public class EmployerAccountsSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.AccountsDbConnectionString)
{
    public async Task<string> SetHashedAccountId(string accountId)
    {
        var query = $"SELECT HashedId FROM [employer_account].[Account] WHERE id = '{accountId}' ";

        var id = await GetDataAsString(query);

        // use extension on ObjectContext from project namespace
        objectContext.SetHashedAccountId(id);

        return id;
    }
}