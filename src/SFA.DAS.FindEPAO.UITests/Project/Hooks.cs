using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.FindEPAO.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context) :FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 22)]
    public async Task NavigateToFindEPAOHomepage() => await Navigate(UrlConfig.FindEPAO_BaseUrl);
}