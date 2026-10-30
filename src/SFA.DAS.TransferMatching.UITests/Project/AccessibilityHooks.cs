using Deque.AxeCore.Playwright;

namespace SFA.DAS.TransferMatching.UITests.Project;

[Binding]
[Scope(Tag = "accessibility")]
public class AccessibilityHooks(ScenarioContext context)
{
    [AfterStep]
    public async Task AuditCurrentPage()
    {
        if (context.TestError != null) return;
        var result = await context.Get<Driver>().Page.RunAxe();
        var failures = result.Violations.Select(x => $"{x.Id}: {x.Help} ({x.HelpUrl})").ToArray();
        foreach (var failure in failures) context.Get<ObjectContext>().SetDebugInformation(failure);
        Assert.That(failures, Is.Empty, string.Join(Environment.NewLine, failures));
    }
}
