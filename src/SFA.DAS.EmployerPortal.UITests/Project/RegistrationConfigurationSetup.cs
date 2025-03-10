namespace SFA.DAS.EmployerPortal.UITests.Project;

[Binding]
public class EmployerPortalConfigurationSetup(ScenarioContext context)
{
    private readonly ConfigSection _configSection = context.Get<ConfigSection>();

    [BeforeScenario(Order = 12)]
    public async Task SetUpEmployerPortalConfigConfiguration()
    {
        //if (new TestDataSetUpConfigurationHelper(context).NoNeedToSetUpConfiguration()) return;

        await context.SetEasLoginUser(
        [
            //_configSection.GetConfigSection<AuthTestUser>(),
            _configSection.GetConfigSection<LevyUser>(),
            _configSection.GetConfigSection<NonLevyUser>(),
            _configSection.GetConfigSection<TransactorUser>(),
            _configSection.GetConfigSection<ViewOnlyUser>(),
        ]);

        SetMongoDbConfig();
    }

    [BeforeScenario(Order = 2), Scope(Tag = "@addmultiplelevyfunds")]
    public async Task SetUpEmployerPortalConfigConfigurationForAddMultipleLevyFunds()
    {
        await context.SetEasLoginUser([_configSection.GetConfigSection<AddMultiplePayeLevyUser>()]);

        SetMongoDbConfig();
    }

    public void SetMongoDbConfig() => context.SetMongoDbConfig(_configSection.GetConfigSection<MongoDbConfig>());
}