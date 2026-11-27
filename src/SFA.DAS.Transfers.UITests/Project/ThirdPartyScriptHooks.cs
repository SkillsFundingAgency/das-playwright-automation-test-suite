namespace SFA.DAS.Transfers.UITests.Project;

[Binding]
public class ThirdPartyScriptHooks(ScenarioContext context)
{
    // Context creation is Order 8; the first employer navigation is Order 22.
    [BeforeScenario(Order = 9)]
    public async Task BlockThirdPartyWidgets()
    {
        if (!context.TryGetValue(out Driver driver) || driver.Browser == null) return;

        await ThirdPartyWidgetRoutes.BlockAsync(driver.BrowserContext);
    }
}
