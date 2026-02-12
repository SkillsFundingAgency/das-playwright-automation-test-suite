using Newtonsoft.Json;

namespace SFA.DAS.ProviderLogin.Service.Project;

[Binding]
public class DfeProviderConfigurationSetup(ScenarioContext context)
{
    private const string DfeProvidersConfig = "DfeProvidersConfig";

    [BeforeScenario(Order = 1)]
    public void SetUpDfeProviderConfiguration()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpDfeProviderConfiguration Order = 1 hook");

        var configSection = context.Get<ConfigSection>();

        var dfeProviderList = configSection.GetConfigSection<List<DfeProviderUsers>>(DfeProvidersConfig);

        if (Configurator.IsAdoExecution)
        {
            var dfeProviderList1 = configSection.GetConfigSection<string>(DfeProvidersConfig);

            dfeProviderList = JsonConvert.DeserializeObject<List<DfeProviderUsers>>(dfeProviderList1);
        }

        var dfeframeworkList = new FrameworkList<DfeProviderUsers>();

        dfeframeworkList.AddRange(dfeProviderList);

        context.Set(dfeframeworkList);
    }
}
