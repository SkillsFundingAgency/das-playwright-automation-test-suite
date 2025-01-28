

namespace SFA.DAS.SupportConsole.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 22)]
    public async Task Navigate()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.SupportConsole_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    [BeforeScenario(Order = 23)]
    public void SetUpHelpers()
    {
        var config = context.GetSupportConsoleConfig<SupportConsoleConfig>();

        var objectContext = context.Get<ObjectContext>();

        var dbConfig = context.Get<DbConfig>();

        var accsqlHelper = new AccountsSqlDataHelper(objectContext, dbConfig);

        var comtsqlHelper = new CommitmentsSqlDataHelper(objectContext, dbConfig);

        var updatedConfig = new SupportConsoleSqlDataHelper(accsqlHelper, comtsqlHelper).GetUpdatedConfig(config);

        context.ReplaceSupportConsoleConfig(updatedConfig);

        context.Get<ObjectContext>().Set("SupportConsoleConfig", updatedConfig);

        context.Set(accsqlHelper);

        context.Set(comtsqlHelper);
    }
}