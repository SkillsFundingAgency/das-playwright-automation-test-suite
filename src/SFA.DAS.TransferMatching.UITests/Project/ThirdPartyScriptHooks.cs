namespace SFA.DAS.TransferMatching.UITests.Project;

[Binding]
public class ThirdPartyScriptHooks(ScenarioContext context)
{
    // Runs after the browser context is created (Order = 8) and before the first navigation (Order = 22).
    [BeforeScenario(Order = 9)]
    public async Task BlockThirdPartyWidgets()
    {
        if (!context.TryGetValue(out Driver driver) || driver.Browser == null) return;

        await ThirdPartyWidgetRoutes.BlockAsync(driver.BrowserContext);
    }
}
