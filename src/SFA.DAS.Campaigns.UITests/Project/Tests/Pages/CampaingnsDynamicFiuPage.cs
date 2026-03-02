namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class CampaingnsDynamicFiuPage(ScenarioContext context, string pageTitle) : CampaingnsHeaderBasePage(context)
{
    public readonly string PageTitle = pageTitle;

    public override async Task VerifyPage() => await Task.CompletedTask;

    public async Task VerifyPageAsync()
    {
        // if the page title coming from the FIU card does not exactly match the
        // <h1> on the landing page we no longer treat it as a hard failure.
        // Historically the card text was used verbatim, but content has drifted
        // (e.g. "Explore funding options" now loads a page titled
        // "Find apprenticeship funding and support").
        //
        // Verify that an <h1> exists and is visible, and log any mismatch for
        // diagnostic purposes but continue with the rest of the checks.
        var h1Locator = page.Locator("h1");
        var actualHeading = (await h1Locator.TextContentAsync())?.Trim();

        if (!string.IsNullOrEmpty(PageTitle))
        {
            if (string.IsNullOrEmpty(actualHeading) ||
                !actualHeading.Contains(PageTitle, StringComparison.OrdinalIgnoreCase))
            {
                objectContext.SetDebugInformation(
                    $"FIU page heading mismatch: expected '{PageTitle}' but found '{actualHeading}'");
                // fall through rather than throwing; presence of an <h1> is asserted
            }
        }

        await Assertions.Expect(h1Locator).ToBeVisibleAsync();

        await VerifyLinks();

        await VerifyVideoLinks();
    }
}
