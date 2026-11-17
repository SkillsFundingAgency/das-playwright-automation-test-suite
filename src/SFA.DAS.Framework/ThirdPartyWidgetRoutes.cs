using System.Text.RegularExpressions;

namespace SFA.DAS.Framework;

public static class ThirdPartyWidgetRoutes
{
    private static readonly Regex CoBrowseHosts = new(@"^https?://([a-z0-9-]+\.)*rcrsv\.io(:\d+)?/", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex ZendeskSnippet = new(@"^https?://static\.zdassets\.com/ekr/snippet\.js", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex ZendeskHosts = new(@"^https?://([a-z0-9-]+\.)*(zdassets|zendesk)\.com(:\d+)?/", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static async Task BlockAsync(IBrowserContext context)
    {
        // Employer pages call zE in <head>, before the external snippet is loaded.
        await context.AddInitScriptAsync("window.zE = window.zE || function () {};");

        await context.RouteAsync(CoBrowseHosts, route => route.AbortAsync());
        await context.RouteAsync(ZendeskHosts, route => route.AbortAsync());

        // The last matching route wins; keep the snippet stub ahead of the broader abort.
        await context.RouteAsync(ZendeskSnippet, route => route.FulfillAsync(new()
        {
            Status = 200,
            ContentType = "application/javascript",
            Body = "window.zE = window.zE || function () {};"
        }));
    }
}
