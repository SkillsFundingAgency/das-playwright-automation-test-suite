
namespace SFA.DAS.Framework.Hooks;

[Binding]
public class ConfigurationHooks(ScenarioContext context)
{
    [BeforeScenario(Order = 1)]
    public void SetUpConfiguration()
    {
        var configSection = new ConfigSection(Configurator.GetConfig());

        context.Set(configSection);

        var dbConfig = configSection.GetConfigSection<DbConfig>();

        if (!Configurator.IsAdoExecution) dbConfig = new LocalHostDbConfig(configSection.GetConfigSection<DbDevConfig>(), context.ScenarioInfo.Tags.Contains("usesqllogin")).GetLocalHostDbConfig();

        context.Set(dbConfig);
    }
}