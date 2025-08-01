﻿using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Framework.Hooks;
using SFA.DAS.SupportTools.UITests.Project.Helpers.SqlHelpers;

namespace SFA.DAS.SupportTools.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private readonly DbConfig _dbConfig = context.Get<DbConfig>();

    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 22)]
    public async Task Navigate() => await Navigate(UrlConfig.SupportTools_BaseUrl);

    [BeforeScenario(Order = 23)]
    public async Task SetUpDataHelpers()
    {
        context.Set(new LoginCredentialsHelper(_objectContext));

        context.Set(new EmployerPortalSqlDataHelper(_objectContext, _dbConfig));

        var config = context.GetSupportConsoleConfig<SupportToolsConfig>();

        var objectContext = context.Get<ObjectContext>();

        var dbConfig = context.Get<DbConfig>();

        var accsqlHelper = new AccountsSqlDataHelper(objectContext, dbConfig);

        var comtsqlHelper = new CommitmentsSqlDataHelper(objectContext, dbConfig);

        var updatedConfig = await new SupportConsoleSqlDataHelper(accsqlHelper, comtsqlHelper).GetUpdatedConfig(config);

        context.ReplaceSupportConsoleConfig(updatedConfig);

        context.Get<ObjectContext>().Set("SupportConsoleConfig", updatedConfig);

        context.Set(accsqlHelper);

        context.Set(comtsqlHelper);
    }
}