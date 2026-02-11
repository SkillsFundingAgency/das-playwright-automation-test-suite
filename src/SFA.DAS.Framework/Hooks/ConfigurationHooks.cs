
namespace SFA.DAS.Framework.Hooks;

[Binding]
public class ConfigurationHooks 
{
    private readonly ScenarioContext _context; 
    private readonly string[] _tags;

    public ConfigurationHooks(ScenarioContext context) { _context = context; _tags = _context.ScenarioInfo.Tags; }

    [BeforeScenario(Order = 1)]
    public void SetUpConfiguration()
    {
        var objectContext = _context.Get<ObjectContext>();

        objectContext.SetTestDataList(_tags);

        objectContext.SetConsoleAndDebugInformation("Entered SetUpConfiguration Order = 1 hook");

        var configSection = new ConfigSection(Configurator.GetConfig());

        _context.Set(configSection);

        var dbConfig = configSection.GetConfigSection<DbConfig>();

        if (!Configurator.IsAdoExecution) dbConfig = new LocalHostDbConfig(configSection.GetConfigSection<DbDevConfig>(), _context.ScenarioInfo.Tags.Contains("usesqllogin")).GetLocalHostDbConfig();

        _context.Set(dbConfig);
    }
}