using NUnit.Framework;

namespace SFA.DAS.Transfers.UITests.Project.Tests.Contracts;

[TestFixture]
[Category("TransfersWidgetIsolationTests")]
public class TransfersWidgetIsolationTests
{
    [Test]
    public async Task ContinueNavigationDoesNotLoadWidgetsOnEmployerOrNewProviderPages()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
        await using var context = await browser.NewContextAsync();
        var unexpectedRequests = new List<string>();
        var pageErrors = new List<string>();
        await context.RouteAsync("**/*", async route =>
        {
            var uri = new Uri(route.Request.Url);
            if (uri.Host == "employer.test")
            {
                await route.FulfillAsync(new()
                {
                    ContentType = "text/html",
                    Body = uri.AbsolutePath == "/start"
                        ? "<a role='button' href='/tasklist'>Continue</a>"
                        : """
                          <head><script>zE(function () {});</script></head>
                          <body>
                          <script src="https://static.zdassets.com/ekr/snippet.js?key=test"></script>
                          <script>zE('webWidget', 'helpCenter:setSuggestions', {});</script>
                          <script src="https://embed-euw1.rcrsv.io/test"></script>
                          <script src="https://widgets.rcrsv.io/js/model.js"></script>
                          <script src="https://example.zendesk.com/widget.js"></script>
                          <script src="https://static.zdassets.com/widget.js"></script>
                          <script src="https://cdn.application.test/app.js"></script>
                          <h1>Create your employer account</h1>
                          </body>
                          """
                });
                return;
            }

            if (uri.Host != "cdn.application.test")
                unexpectedRequests.Add(route.Request.Url);
            await route.FulfillAsync(new()
            {
                ContentType = "application/javascript",
                Body = "document.body.dataset.applicationReady = 'true';"
            });
        });
        await ThirdPartyWidgetRoutes.BlockAsync(context);

        var employer = await context.NewPageAsync();
        employer.PageError += (_, error) => pageErrors.Add(error);
        await employer.GotoAsync("https://employer.test/start");
        await employer.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        await Assertions.Expect(employer.GetByRole(AriaRole.Heading, new() { Name = "Create your employer account" })).ToBeVisibleAsync();
        await Assertions.Expect(employer.Locator("body")).ToHaveAttributeAsync("data-application-ready", "true");

        var provider = await context.NewPageAsync();
        provider.PageError += (_, error) => pageErrors.Add(error);
        await provider.GotoAsync("https://employer.test/tasklist");
        await Assertions.Expect(provider.Locator("body")).ToHaveAttributeAsync("data-application-ready", "true");
        Assert.That(unexpectedRequests, Is.Empty, "Widget requests must not reach the network.");
        Assert.That(pageErrors, Is.Empty, "Inline zE calls must be safe before and after the snippet.");
    }

    [TestCase("https://rcrsv.io.application.test/app.js")]
    [TestCase("https://zendesk.com.application.test/app.js")]
    [TestCase("https://zdassets.com.application.test/app.js")]
    public async Task SimilarlyNamedApplicationHostsAreNotBlocked(string scriptUrl)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
        await using var context = await browser.NewContextAsync();
        await context.RouteAsync("**/*", route => route.FulfillAsync(new()
        {
            ContentType = "application/javascript",
            Body = "document.body.dataset.applicationReady = 'true';"
        }));
        await ThirdPartyWidgetRoutes.BlockAsync(context);

        var page = await context.NewPageAsync();
        await page.SetContentAsync($"<body><script src='{scriptUrl}'></script></body>");
        await Assertions.Expect(page.Locator("body")).ToHaveAttributeAsync("data-application-ready", "true");
    }
}
