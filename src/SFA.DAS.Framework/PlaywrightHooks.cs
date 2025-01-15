﻿using Microsoft.Playwright;
using NUnit.Framework;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework;

[Binding]
public class PlaywrightHooks(ScenarioContext context)
{
    private static InitializeDriver driver;

    private IBrowserContext browserContext;

    [BeforeTestRun]
    public static Task BeforeAll()
    {
        driver = new InitializeDriver();

        return Task.CompletedTask;
    }

    [BeforeScenario(Order = 4)]
    public async Task SetupPlaywrightDriver()
    {
        browserContext = await driver.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        await browserContext.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = false
        });

        var page = await browserContext.NewPageAsync();

        context.Set(new Driver(page, context.Get<ObjectContext>()));
    }


    [AfterScenario(Order = 4)]
    public async Task StopTracing()
    {
        var tracefileName = $"TRACEDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

        var tracefilePath = $"{context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

        await browserContext.Tracing.StopAsync(new()
        {
            Path = tracefilePath
        });

        TestContext.AddTestAttachment(tracefilePath, tracefileName);
    }

    [AfterTestRun]
    public static void AfterAll()
    {
        driver.Dispose();
    }
}
