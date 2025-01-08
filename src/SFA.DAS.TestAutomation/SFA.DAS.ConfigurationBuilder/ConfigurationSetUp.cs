using TechTalk.SpecFlow;

namespace SFA.DAS.ConfigurationBuilder
{
    [Binding]
    public class ConfigurationSetup(ScenarioContext context)
    {
        [BeforeScenario(Order = 0)]
        public void SetUpConfiguration()
        {
            var configSection = new ConfigSection(Configurator.GetConfig());

            context.Set(configSection);
        }
    }
}