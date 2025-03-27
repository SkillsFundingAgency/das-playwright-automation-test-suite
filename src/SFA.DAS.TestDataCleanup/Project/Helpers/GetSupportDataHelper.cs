namespace SFA.DAS.TestDataCleanup.Project.Helpers;

public class GetSupportDataHelper(ObjectContext objectContext, DbConfig dbConfig)
{
    internal async Task<List<string[]>> GetApprenticeIds(List<string> accountidsTodelete) => await new TestDataCleanupComtSqlDataHelper(objectContext, dbConfig).GetApprenticeIds(accountidsTodelete);
}
