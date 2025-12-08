namespace SFA.DAS.Finance.APITests.Project.Helpers.SqlDbHelpers;

public class EmployerFinanceSqlHelper(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.FinanceDbConnectionString)
{
    public async Task<string> SetAccountId()
    {
        var accountId = await GetDataAsString($"SELECT top (1) AccountId FROM [employer_financial].[LevyDeclaration] order by Id desc");

        if (string.IsNullOrWhiteSpace(accountId))
        {
            // ensure we don't store null/empty values
            accountId = string.Empty;
        }

        objectContext.SetAccountId(accountId);

        return accountId;
    }

    public async Task SetEmpRef()
    {
        var accountId = objectContext.GetAccountId();

        if (!long.TryParse(accountId, out var accountIdLong))
        {
            // nothing to do when account id is not available
            return;
        }

        var empRef = await GetDataAsString($"Select top (1) EmpRef from [employer_financial].[AccountPaye] Where [AccountId] = {accountIdLong}");
        if (string.IsNullOrWhiteSpace(empRef)) return;
        objectContext.SetEmpRef(empRef);
    }
}