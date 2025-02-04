namespace SFA.DAS.SupportConsole.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 22)]
    public async Task Navigate()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.SupportTools_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }
}