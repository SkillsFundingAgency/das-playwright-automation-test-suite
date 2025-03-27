namespace SFA.DAS.TestDataCleanup.Project.Helpers;

public class AllDbTestDataCleanUpHelper(ObjectContext objectContext, DbConfig dbConfig)
{
    private string _sqlFileName, _dbName;

    private readonly List<string> usersdeleted = [];

    private readonly List<string> userswithconstraints = [];

    private List<string[]> _apprenticeIds;

    public async Task<(List<string>, List<string>)> CleanUpAllDbTestData(string email) => await CleanUpAllDbTestData([email]);

    public async Task<(List<string>, List<string>)> CleanUpAllDbTestData(List<string> email)
    {
        List<List<string>> userEmailListoflist = [];

        (var easAccDbSqlDataHelper, var userEmailListArray) = await GetUserEmailList(email);

        if (userEmailListArray.IsNoDataFound()) return (usersdeleted, userswithconstraints);

        var userEmailList = userEmailListArray.ListOfArrayToList(0);

        AddInUseEmails(userEmailList);

        int batchCount = 25;

        for (int i = 0; i < userEmailList.Count; i += batchCount) userEmailListoflist.Add(userEmailList.Skip(i).Take(batchCount).ToList());

        foreach (var item in userEmailListoflist) await CleanUpTestData(easAccDbSqlDataHelper, item);

        return (usersdeleted, userswithconstraints);
    }

    private async Task<(TestDataCleanUpEasAccDbSqlDataHelper, List<string[]>)> GetUserEmailList(List<string> email)
    {
        var easAccDbSqlDataHelper = new TestDataCleanUpEasAccDbSqlDataHelper(objectContext, dbConfig);

        SetDetails(easAccDbSqlDataHelper);

        return (easAccDbSqlDataHelper, await easAccDbSqlDataHelper.GetUserEmailList(email));
    }

    private static void AddInUseEmails(List<string> userEmailList) => TestDataCleanUpEmailsInUse.AddInUseEmails(userEmailList);

    private async Task CleanUpTestData(TestDataCleanUpEasAccDbSqlDataHelper easAccDbSqlDataHelper, List<string> userEmailList)
    {
        try
        {
            var accountIdsListArray = await easAccDbSqlDataHelper.GetAccountIds(userEmailList);

            var noOfRowsDeleted = await CleanUpUsersDbTestData(userEmailList);

            var appaccdbNameToTearDown = objectContext.GetDbNameToTearDown();

            if (appaccdbNameToTearDown.TryGetValue(CleanUpDbName.EasAppAccTestDataCleanUp, out HashSet<string> appaccemails))
            {
                noOfRowsDeleted += await CleanUpAppAccDbTestData([.. appaccemails]);
            }

            var accountidsTodelete = accountIdsListArray.ListOfArrayToList(0);

            if (!string.IsNullOrEmpty(accountidsTodelete[0])) noOfRowsDeleted += await CleanUpTestDataUsingAccountId(accountidsTodelete);

            noOfRowsDeleted += await CleanUpEasDbTestData(easAccDbSqlDataHelper, userEmailList);

            int x = userEmailList.Count;

            if (x < 15) usersdeleted.Add($"{userEmailList.ToString(",")}");
            if (accountidsTodelete.Count < 15) usersdeleted.Add($"{accountidsTodelete.ToString(",")}");
            usersdeleted.Add($"{noOfRowsDeleted} total rows deleted across the dbs");
            usersdeleted.Add($"{userEmailList.Count} email account{(x == 1 ? string.Empty : "s")} deleted");
        }
        catch (Exception ex)
        {
            userswithconstraints.Add($"{userEmailList.ToString(",")},{_dbName}({_sqlFileName}){Environment.NewLine}{ex.Message}");
        }
    }

    private async Task<int> CleanUpTestDataUsingAccountId(List<string> accountidsTodelete)
    {
        return await CleanUpRsvrTestData(accountidsTodelete)
            + await CleanUpPrelTestData(accountidsTodelete)
            + await CleanUpPsrTestData(accountidsTodelete)
            + await CleanUpPfbeTestData(accountidsTodelete)
            + await CleanUpEmpFcastTestData(accountidsTodelete)
            + await CleanUpAppfbTestData(accountidsTodelete)
            + await CleanUpEmpFinTestData(accountidsTodelete)
            + await CleanUpEmpIncTestData(accountidsTodelete)
            + await CleanUpAComtTestData()
            + await CleanUpEasLtmTestData(accountidsTodelete)
            + await CleanUpComtTestData(accountidsTodelete);
    }

    private async Task<int> CleanUpEasDbTestData(TestDataCleanUpEasAccDbSqlDataHelper helper, List<string> emailsToDelete)
    {
        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpEasDbTestData(emailsToDelete));
    }

    private async Task<int> CleanUpComtTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanupComtSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpComtTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpEasLtmTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpEasLtmcSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpEasLtmTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpAppfbTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanupAppfbqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        _apprenticeIds = await new GetSupportDataHelper(objectContext, dbConfig).GetApprenticeIds(accountidsTodelete);

        return await SetDebugMessage(async () => await helper.CleanUpAppfbTestData(_apprenticeIds));
    }

    private async Task<int> CleanUpAComtTestData()
    {
        var helper = new TestDataCleanupAComtSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpAComtTestData(_apprenticeIds));
    }

    private async Task<int> CleanUpAppAccDbTestData(List<string> emailsToDelete)
    {
        var helper = new TestDataCleanUpAppAccDbSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpAppAccDbTestData(emailsToDelete));
    }

    private async Task<int> CleanUpEmpIncTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpEmpIncSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpEmpIncTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpEmpFinTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpEmpFinSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpEmpFinTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpEmpFcastTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpEmpFcastSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpEmpFcastTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpPfbeTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpPfbeDbSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpPfbeTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpPsrTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpPsrDbSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpPsrTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpPrelTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpPrelDbSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpPrelTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpRsvrTestData(List<string> accountidsTodelete)
    {
        var helper = new TestDataCleanUpRsvrSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpRsvrTestData(accountidsTodelete));
    }

    private async Task<int> CleanUpUsersDbTestData(List<string> emailsToDelete)
    {
        var helper = new TestDataCleanUpUsersDbSqlDataHelper(objectContext, dbConfig);

        SetDetails(helper);

        return await SetDebugMessage(async () => await helper.CleanUpUsersDbTestData(emailsToDelete));
    }

    private void SetDetails(TestDataCleanUpSqlDataHelper helper)
    {
        _dbName = helper.dbName;

        _sqlFileName = helper.SqlFileName;
    }

    private async Task<int> SetDebugMessage(Func<Task<int>> func)
    {
        int noOfrowsDeleted;

        string message;

        try
        {
            noOfrowsDeleted = await func();

            message = $"{noOfrowsDeleted} rows deleted from {_dbName}";
        }
        catch (Exception ex)
        {
            noOfrowsDeleted = 0;

            message = $"FAILED to delete from {_dbName}({_sqlFileName})";

            userswithconstraints.Add($"{_dbName}({_sqlFileName}){Environment.NewLine}{ex.Message}");
        }

        usersdeleted.Add(message);

        return noOfrowsDeleted;
    }
}
