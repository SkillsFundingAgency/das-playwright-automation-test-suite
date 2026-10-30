namespace SFA.DAS.TransferMatching.UITests.Project;

[Binding]
public class TransferMatchingConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 2)]
    public void SkipRestrictedLocalJobs()
    {
        if (context.ScenarioInfo.Tags.Contains("ignorelocalexecutioninpp")
            && !Configurator.IsAdoExecution && EnvironmentConfig.IsPPEnvironment)
            Assert.Ignore("Transfer matching jobs require a whitelisted pipeline agent in PP.");
    }

    [BeforeScenario(Order = 11)]
    public void ValidateEmployerConfiguration()
    {
        context.Get<ObjectContext>().SetConsoleAndDebugInformation("Entered TransferMatching employer configuration preflight | Order = 11");
        var config = context.Get<ConfigSection>();
        Dictionary<string, EasAccountUser> users = new()
        {
            [nameof(LevyUser)] = config.GetConfigSection<LevyUser>(),
            [nameof(NonLevyUser)] = config.GetConfigSection<NonLevyUser>(),
            [nameof(TransactorUser)] = config.GetConfigSection<TransactorUser>(),
            [nameof(ViewOnlyUser)] = config.GetConfigSection<ViewOnlyUser>(),
            [nameof(TransferMatchingUser)] = config.GetConfigSection<TransferMatchingUser>(),
            [nameof(TransfersUser)] = config.GetConfigSection<TransfersUser>(),
            [nameof(TransfersUserNoFunds)] = config.GetConfigSection<TransfersUserNoFunds>()
        };
        var missing = users.Where(x => string.IsNullOrWhiteSpace(x.Value?.Username)
            || x.Value.Username.StartsWith("__", StringComparison.Ordinal)).Select(x => x.Key).ToArray();
        if (missing.Length > 0)
            throw new InvalidOperationException($"Configure TransferMatching employer usernames before running UI scenarios: {string.Join(", ", missing)}. See README Transfer matching setup.");
    }

    [BeforeScenario(Order = 13)]
    public async Task SetUpConfiguration()
    {
        context.Get<ObjectContext>().SetConsoleAndDebugInformation("Entered TransferMatching account setup | Order = 13");
        var config = context.Get<ConfigSection>();
        await context.SetEasLoginUser(
        [
            config.GetConfigSection<TransferMatchingUser>(),
            config.GetConfigSection<TransfersUserNoFunds>(),
            config.GetConfigSection<TransfersUser>()
        ]);
        context.Set(config.GetConfigSection<TransferMatchingJobsConfig>());
    }

    [BeforeScenario(Order = 42)]
    public async Task SetUpDataHelpers()
    {
        var course = await new SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.CoursesDataHelper().GetRandomStandardCourse();
        context.Set(new TMDataHelper(course));
        var objectContext = context.Get<ObjectContext>();
        objectContext.SetPledgeDetailList();
        context.Set(new TransferMatchingSqlDataHelper(objectContext, context.Get<DbConfig>()));
    }

    [AfterScenario(Order = 41)]
    public async Task DeletePledges()
    {
        if (context.TestError == null)
            await context.Get<TransferMatchingSqlDataHelper>().DeletePledge(context.Get<ObjectContext>().GetPledgeDetailList());
    }
}
