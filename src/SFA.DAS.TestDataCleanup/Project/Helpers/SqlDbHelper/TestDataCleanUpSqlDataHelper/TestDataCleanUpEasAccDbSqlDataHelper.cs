namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanUpEasAccDbSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.AccountsDbConnectionString)
{
    public override string SqlFileName => "EasAccTestDataCleanUp";

    internal async Task<List<string[]>> GetAccountIds(List<string> userEmail) => await GetMultipleData($"select AccountId from employer_account.Membership where UserId in (select id from employer_account.[User] where Email = {GetAccountIdsQuery(userEmail)})");

    internal async Task<List<string[]>> GetUserEmailList(List<string> userEmail)
    {
        var past5months = GetPastfiveMonthsDate();

        var query = $"select top 100 Email from employer_account.[User] where " +
            $"Email like {GetUserEmailListQuery(userEmail)} " +
            $"and Email like '___Test__________________________@%' " +
            $"and Email not like '%perftest.com' " +
            $"and Email not like '%PerfTest%' " +
            $"and Email not like '%{past5months[0]}%' " +
            $"and Email not like '%{past5months[1]}%' " +
            $"and Email not like '%{past5months[2]}%' " +
            $"and Email not like '%{past5months[3]}%' " +
            $"and Email not like '%{past5months[4]}%' " +
            $"and Email not in ({TestDataCleanUpEmailsInUse.GetInUseEmails()}) order by NEWID() desc";

        var userEmailList = await GetMultipleData(query);

        return userEmailList;
    }

    internal async Task<List<string[]>> GetAccountIds(int greaterThan, int lessThan) => await GetMultipleAccountData($"select Id from employer_account.Account where id > {greaterThan} and id < {lessThan} order by id desc");

    internal async Task<List<string[]>> GetAccountHashedIds(List<string> accountIdToDelete) => await GetMultipleData($"select HashedId from employer_account.Account where id in ({string.Join(",", accountIdToDelete)})");

    internal async Task<int> CleanUpEasDbTestData(List<string> emailsToDelete) => await CleanUpUsingEmail(emailsToDelete);

    private static string Join(string separator, List<string> list) => string.Join(separator, list.Select(x => $"('{x}')"));

    private static string GetAccountIdsQuery(List<string> userEmail) => Join(" or Email = ", userEmail);

    private static string GetUserEmailListQuery(List<string> userEmail) => Join(" or Email like ", userEmail);

    private static List<string> GetPastfiveMonthsDate()
    {
        List<string> strings = [];

        for (int i = 1; i < 6; i++) strings.Add($"{DateTime.Now.AddMonths(-i):MMMyyyy}");

        return strings;
    }
}
