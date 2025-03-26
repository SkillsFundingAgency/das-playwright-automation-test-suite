using SFA.DAS.TestDataCleanup.Project.Helpers;

namespace SFA.DAS.TestDataCleanup.Project;

[Binding]
public class TestDataCleanUp(ScenarioContext context)
{
    [AfterScenario(Order = 98)]
    public async Task CleanUpTestData()
    {
        if (context.TestError == null && context.ScenarioInfo.Tags.Contains("regression"))
        {
            context.Get<ObjectContext>().SetDebugInformation("*********** Cleaning test data from all db **********");

            await context.Get<TryCatchExceptionHelper>().AfterScenarioException(async () => 
            {
                var dbNameToTearDown = context.Get<ObjectContext>().GetDbNameToTearDown();

                if (dbNameToTearDown.Count > 0)
                {
                    if (dbNameToTearDown.TryGetValue(CleanUpDbName.EasUsersTestDataCleanUp, out HashSet<string> emails))

                    {
                        context.Get<ObjectContext>().SetDebugInformation($"****** cleaning test data related to emails - {string.Join(",", emails.ToList())}");

                        await new TestdataCleanupStepsHelper(context).CleanUpAllDbTestData(emails);
                    }

                }
            });
        }
    }
}