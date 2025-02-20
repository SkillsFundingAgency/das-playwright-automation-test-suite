using SFA.DAS.Registration.UITests.Project.Helpers;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;

namespace SFA.DAS.SupportTools.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    private readonly DbConfig _dbConfig = context.Get<DbConfig>();

    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 22)]
    public async Task Navigate()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.SupportTools_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    [BeforeScenario(Order = 23)]
    public void SetUpDataHelpers()
    {
        context.Set(new LoginCredentialsHelper(_objectContext));

        context.Set(new RegistrationSqlDataHelper(_objectContext, _dbConfig));
    }
}