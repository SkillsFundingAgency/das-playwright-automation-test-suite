using NUnit.Framework;
using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper;
using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.BaseSqlDbHelper;
using SFA.DAS.TestDataCleanup.Project.Helpers.SqlDbHelper.TestDataCleanUpSqlDataHelper;

namespace SFA.DAS.Transfers.UITests.Project.Tests.Steps;

[Binding]
public class VerifyDbConnectionSteps(ScenarioContext context)
{
    [Then(@"^the db connection are verified$")]
    public async Task VerifyConnections()
    {
        var config = context.Get<DbConfig>();
        var objectContext = context.Get<ObjectContext>();
        ProjectSqlDbHelper[] helpers =
        [
            new AssessorDbSqlDataHelper(objectContext, config),
            new LoginDbSqlDataHelper(objectContext, config),
            new QnaDbSqlDataHelper(objectContext, config),
            new ApplyDbSqlDataHelper(objectContext, config),
            new RoatpDbSqlDataHelper(objectContext, config),
            new TestDataCleanupAComtSqlDataHelper(objectContext, config),
            new TestDataCleanupComtSqlDataHelper(objectContext, config),
            new TestDataCleanUpEasLtmcSqlDataHelper(objectContext, config),
            new TestDataCleanUpEmpFcastSqlDataHelper(objectContext, config),
            new TestDataCleanUpEmpFinSqlDataHelper(objectContext, config),
            new TestDataCleanUpEmpIncSqlDataHelper(objectContext, config),
            new TestDataCleanUpPfbeDbSqlDataHelper(objectContext, config),
            new TestDataCleanUpPrelDbSqlDataHelper(objectContext, config),
            new TestDataCleanUpPsrDbSqlDataHelper(objectContext, config),
            new TestDataCleanUpRsvrSqlDataHelper(objectContext, config),
            new EmploymentCheckDbSqlDataHelper(objectContext, config),
            new TestDataCleanUpEasAccDbSqlDataHelper(objectContext, config),
            new ACommitmentLoginDbSqlDataHelper(objectContext, config),
            new CrsDbSqlDataHelper(objectContext, config),
            new TprDbSqlDataHelper(objectContext, config),
            new TestDataCleanUpUsersDbSqlDataHelper(objectContext, config)
        ];
        var failures = new List<string>();
        foreach (var helper in helpers)
        {
            var caller = helper.GetCaller();
            if (helper.ExcludeEnvironments)
            {
                objectContext.SetDebugInformation($"EXCLUDED - '{caller}' does not exist for this environment.");
                continue;
            }
            try
            {
                var catalog = await helper.GetTableCatalog();
                StringAssert.StartsWith("das-", catalog);
                objectContext.SetDebugInformation($"'{caller}' connected successfully to '{catalog}'.");
            }
            catch (Exception exception)
            {
                var failure = $"FAILED - '{caller}' could not be connected: {exception.Message}";
                objectContext.SetDebugInformation(failure);
                failures.Add(failure);
            }
        }
        Assert.That(failures, Is.Empty, string.Join(Environment.NewLine, failures));
    }
}
