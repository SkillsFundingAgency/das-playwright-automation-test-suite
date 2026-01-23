using SFA.DAS.Apar.UITests.Project.Helpers.UkprnDataHelpers;
using System;
using System.Linq;

[Binding]
public class AparAdminDataHooks
{
    private readonly ScenarioContext _scenarioContext;

    public AparAdminDataHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario(Order = 0)]
    public void LoadUkprnDataFromScenarioTag()
    {
        var dataTag = _scenarioContext.ScenarioInfo.Tags
            .FirstOrDefault(t => t.StartsWith("rpad", StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(dataTag))
            return;

        var dataHelper = new OldAparAdminUkprnDataHelpers();
        var (providerName, ukprn) = dataHelper.GetOldRoatpAdminData(dataTag);

        _scenarioContext.Set(ukprn, "ukprn");
        _scenarioContext.Set(providerName, "providerName");
    }
}
