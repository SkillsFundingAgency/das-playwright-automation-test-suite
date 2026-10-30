using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper;
using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.BaseSqlDbHelper;
using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Steps;

[Binding]
public class VerifyDbConnectionSteps(ScenarioContext context)
{
    [Then("the db connection are verified")]
    public async Task VerifyConnections()
    {
        var config = context.Get<DbConfig>();
        var objects = context.Get<ObjectContext>();
        ProjectSqlDbHelper[] helpers =
        [
            new AssessorDbSqlDataHelper(objects, config),
            new LoginDbSqlDataHelper(objects, config),
            new QnaDbSqlDataHelper(objects, config),
            new ApplyDbSqlDataHelper(objects, config),
            new RoatpDbSqlDataHelper(objects, config),
            new TestDataCleanupAComtSqlDataHelper(objects, config),
            new TestDataCleanupComtSqlDataHelper(objects, config),
            new TestDataCleanUpEasLtmcSqlDataHelper(objects, config),
            new TestDataCleanUpEmpFcastSqlDataHelper(objects, config),
            new TestDataCleanUpEmpFinSqlDataHelper(objects, config),
            new TestDataCleanUpEmpIncSqlDataHelper(objects, config),
            new TestDataCleanUpPfbeDbSqlDataHelper(objects, config),
            new TestDataCleanUpPrelDbSqlDataHelper(objects, config),
            new TestDataCleanUpPsrDbSqlDataHelper(objects, config),
            new TestDataCleanUpRsvrSqlDataHelper(objects, config),
            new EmploymentCheckDbSqlDataHelper(objects, config),
            new TestDataCleanUpEasAccDbSqlDataHelper(objects, config),
            new ACommitmentLoginDbSqlDataHelper(objects, config),
            new CrsDbSqlDataHelper(objects, config),
            new TprDbSqlDataHelper(objects, config),
            new TestDataCleanUpUsersDbSqlDataHelper(objects, config)
        ];
        foreach (var helper in helpers)
        {
            if (helper.ExcludeEnvironments)
            {
                objects.SetDebugInformation($"EXCLUDED: {helper.GetCaller()} does not exist in this environment.");
                continue;
            }
            Assert.That(await helper.GetTableCatalog(), Does.StartWith("das-"), helper.GetCaller());
            objects.SetDebugInformation($"{helper.GetCaller()} connected successfully.");
        }
    }
}
