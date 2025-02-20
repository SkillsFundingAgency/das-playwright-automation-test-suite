﻿using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;
using System.Linq;

namespace SFA.DAS.ConfigurationBuilder.BeforeScenario;

[Binding]
public class MailosaurAPIConfigurationSetup(ScenarioContext context)
{
    private const string MailosaurApiConfig = "MailasourApiConfig";

    [BeforeScenario(Order = 5)]
    public void SetUpMailosaurAPIConfiguration()
    {
        var mailosaurApiConfigs = new MultiConfigurationSetupHelper(context).SetMultiConfiguration<MailosaurApiConfig>(MailosaurApiConfig);

        var mailosaurApiConfig = Configurator.IsAdoExecution ? mailosaurApiConfigs.Single(x => x.ServerName == "azure") : mailosaurApiConfigs.Single(x => x.ServerName == "local");

        context.Set(new MailosaurUser(mailosaurApiConfig.ServerName, mailosaurApiConfig.ServerId, mailosaurApiConfig.ApiToken));
    }
}
