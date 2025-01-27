﻿using Newtonsoft.Json;
using SFA.DAS.FrameworkHelpers;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SFA.DAS.ConfigurationBuilder;

public class MultiConfigurationSetupHelper(ScenarioContext context)
{
    public FrameworkList<T> SetMultiConfiguration<T>(string key)
    {
        var configSection = context.Get<ConfigSection>();

        var list = configSection.GetConfigSection<List<T>>(key);

        if (Configurator.IsAdoExecution)
        {
            var azureList = configSection.GetConfigSection<string>(key);

            list = JsonConvert.DeserializeObject<List<T>>(azureList);
        }

        var listType = new FrameworkList<T>();

        listType.AddRange(list);

        context.Set(listType);

        return listType;
    }
}