using Newtonsoft.Json;
using NUnit.Framework;
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
            context.Get<ObjectContext>().SetDebugInformation(key);

            TestContext.Progress.WriteLine(key);

            TestContext.Out.WriteLine(key);

            var azureList = configSection.GetConfigSection<string>(key);

            context.Get<ObjectContext>().SetDebugInformation(azureList);

            TestContext.Progress.WriteLine(azureList);

            TestContext.Out.WriteLine(azureList);

            list = JsonConvert.DeserializeObject<List<T>>(azureList);
        }

        var listType = new FrameworkList<T>();

        listType.AddRange(list);

        context.Set(listType);

        return listType;
    }
}