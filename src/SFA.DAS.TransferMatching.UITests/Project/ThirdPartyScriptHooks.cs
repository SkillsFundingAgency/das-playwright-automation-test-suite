using System.Text.RegularExpressions;

namespace SFA.DAS.TransferMatching.UITests.Project;

[Binding]
public class ThirdPartyScriptHooks(ScenarioContext context)
{
    // The rcrsv.io co-browse widget embedded in the employer accounts site delays the page load event
    // and loads heavy WASM models that starve the main thread, causing navigation and click timeouts.
    private static readonly Regex BlockedHosts = new(@"^https?://([a-z0-9-]+\.)*rcrsv\.io(:\d+)?/", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    // The Zendesk snippet is a parser-blocking <script> at the top of <body>; in PP it and its widget calls
    // can take several seconds, leaving the page blank long enough for assertions to time out.
    // The snippet is stubbed (so the inline zE(...) calls don't throw) and the rest of the widget is aborted.
    private static readonly Regex ZendeskSnippet = new(@"^https?://static\.zdassets\.com/ekr/snippet\.js", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ZendeskHosts = new(@"^https?://([a-z0-9-]+\.)*(zdassets|zendesk)\.com(:\d+)?/", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    // Runs after the browser context is created (Order = 8) and before the first navigation (Order = 22).
    [BeforeScenario(Order = 9)]
    public async Task BlockThirdPartyWidgets()
    {
        if (!context.TryGetValue(out Driver driver) || driver.Browser == null) return;

        await driver.BrowserContext.RouteAsync(BlockedHosts, route => route.AbortAsync());

        await driver.BrowserContext.RouteAsync(ZendeskHosts, route => route.AbortAsync());

        // Registered last so it takes precedence over the broader Zendesk route.
        await driver.BrowserContext.RouteAsync(ZendeskSnippet, route => route.FulfillAsync(new()
        {
            Status = 200,
            ContentType = "application/javascript",
            Body = "window.zE = window.zE || function () {};"
        }));
    }
}
