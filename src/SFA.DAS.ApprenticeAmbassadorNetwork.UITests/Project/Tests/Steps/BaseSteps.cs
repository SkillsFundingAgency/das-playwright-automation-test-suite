﻿namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps;

public abstract class BaseSteps(ScenarioContext context)
{
    protected readonly ScenarioContext context = context;

    protected readonly ObjectContext objectContext = context.Get<ObjectContext>();

    protected readonly AANSqlHelper _aanSqlHelper = context.Get<AANSqlHelper>();

    protected async Task Navigate(string url)
    {
        var driver = context.Get<Driver>();

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }
}