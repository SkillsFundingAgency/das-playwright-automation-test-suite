﻿using SFA.DAS.ConfigurationBuilder;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework.Hooks
{
    [Binding]
    public class ConfigurationHooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 1)]
        public void SetUpConfiguration()
        {
            var configSection = new ConfigSection(Configurator.GetConfig());

            context.Set(configSection);
        }
    }
}