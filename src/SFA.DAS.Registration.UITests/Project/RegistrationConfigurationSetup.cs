using SFA.DAS.Login.Service.Project;
using SFA.DAS.MongoDb.DataGenerator;

namespace SFA.DAS.Registration.UITests.Project;

[Binding]
public class RegistrationConfigurationSetup(ScenarioContext context)
{
    private readonly ConfigSection _configSection = context.Get<ConfigSection>();

    [BeforeScenario(Order = 12)]
    public async Task SetUpRegistrationConfigConfiguration()
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
    public async Task SetUpRegistrationConfigConfigurationForAddMultipleLevyFunds()
    {
        await context.SetEasLoginUser([_configSection.GetConfigSection<AddMultiplePayeLevyUser>()]);

        SetMongoDbConfig();
    }

    public void SetMongoDbConfig() => context.SetMongoDbConfig(_configSection.GetConfigSection<MongoDbConfig>());
}