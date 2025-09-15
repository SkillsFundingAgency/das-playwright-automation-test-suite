namespace SFA.DAS.Framework.Hooks;

[Binding, Scope(Tag = "@donottracelogin")]
public class TraceStartAndStopSteps(ScenarioContext context)
{
    [Given("the trace is started")]
    public async Task GivenTheTraceIsStarted()
    {
        var driver = context.Get<Driver>();

        await driver.BrowserContext.Tracing.StartAsync(new()
        {
            Title = context.ScenarioInfo.Title,
            Screenshots = true,
            Snapshots = true
        });
    }

    [Given("the trace is stopped")]
    public async Task GivenTheTraceIsStopped()
    {
        var driver = context.Get<Driver>();

        var tracefileName = $"TRACEDATA_{DateTime.Now:HH-mm-ss-fffff}.zip";

        var tracefilePath = $"{context.Get<ObjectContext>().GetDirectory()}/{tracefileName}";

        await driver.BrowserContext.Tracing.StopAsync(new()
        {
            Path = tracefilePath
        });

        TestContext.AddTestAttachment(tracefilePath, tracefileName);
    }
}
