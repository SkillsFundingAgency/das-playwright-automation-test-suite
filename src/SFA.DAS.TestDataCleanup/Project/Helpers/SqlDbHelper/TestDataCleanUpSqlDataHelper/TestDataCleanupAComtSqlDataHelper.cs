namespace SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

public class TestDataCleanupAComtSqlDataHelper(ObjectContext objectContext, DbConfig dbConfig) : BaseSqlDbHelper.TestDataCleanUpSqlDataHelper(objectContext, dbConfig.ApprenticeCommitmentDbConnectionString)
{
    public override string SqlFileName => "EasAComtTestDataCleanUp";

    private readonly ObjectContext _objectContext = objectContext;

    internal async Task<int> CleanUpAComtTestData(List<string[]> apprenticeIds) => await CleanUpUsingCommtApprenticeshipIds(apprenticeIds);

    internal async Task<int> CleanUpAComtTestData(List<string> accountIdToDelete) => await CleanUpAComtTestData(await new GetSupportDataHelper(_objectContext, dbConfig).GetApprenticeIds(accountIdToDelete));
}
