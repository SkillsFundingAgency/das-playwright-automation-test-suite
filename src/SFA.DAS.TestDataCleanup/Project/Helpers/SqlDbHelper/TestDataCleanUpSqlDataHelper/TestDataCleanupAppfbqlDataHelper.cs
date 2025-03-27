namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanupAppfbqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.ApprenticeFeedbackDbConnectionString)
{
    public override string SqlFileName => "EasAppfbTestDataCleanUp";

    private readonly ObjectContext _objectContext = objectContext;

    internal async Task<int> CleanUpAppfbTestData(List<string[]> apprenticeIds) => await CleanUpUsingCommtApprenticeshipIds(apprenticeIds);

    internal async Task<int> CleanUpAppfbTestData(List<string> accountIdToDelete) => await CleanUpAppfbTestData(await new GetSupportDataHelper(_objectContext, dbConfig).GetApprenticeIds(accountIdToDelete));
}
