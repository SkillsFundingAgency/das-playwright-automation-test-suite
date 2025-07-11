namespace SFA.DAS.FindEPAO.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 22)]
    public async Task NavigateToFindEPAOHomepage()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.FindEPAO_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }
}