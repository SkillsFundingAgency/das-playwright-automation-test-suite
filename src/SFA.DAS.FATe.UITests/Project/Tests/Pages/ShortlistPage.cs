namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class ShortlistPage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).
        ToContainTextAsync("Shortlisted training providers");

    public async Task VerifyLocationFilterInShortlist()
    {
        await page.Locator("summary").Filter(new() { HasText = "View 2 training providers" }).ClickAsync();
        await page.Locator("summary").Filter(new() { HasText = "View 1 training provider" }).ClickAsync();
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("HUGH BAIRD COLLEGE");
        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync("L20 7EW");
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Accounts or finance assistant (level 2)");
    }    
    public async Task VerifyCourseNameShortlisted(string expectedCourseName)
    {
        var courseHeading = page.Locator("h2.govuk-heading-m.govuk-\\!-margin-bottom-4");
        await Assertions.Expect(courseHeading).ToHaveTextAsync(expectedCourseName);
    }
    public async Task GoToTrainingProvidersPage()
    {
        await page.GoBackAsync();
    }
    public async Task ClickOnTrainingProvider(string providerName)
    {
        var expandSummary = page.Locator("summary:has(span.govuk-details__summary-text)");
        await expandSummary.ClickAsync();
        var providerLink = page.GetByRole(AriaRole.Link, new() { Name = providerName });
        await providerLink.ClickAsync();
    }
}