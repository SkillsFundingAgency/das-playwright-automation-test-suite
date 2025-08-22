namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;

public class FeedbackProviderHomePage(ScenarioContext context) : ProviderHomePage(context)
{
    public async Task<FeedbackOverviewPage> SelectYourFeedback()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your feedback" }).ClickAsync();

        return await VerifyPageAsync(() => new FeedbackOverviewPage(context));
    }
}

public class FeedbackOverviewPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#your-feedback")).ToContainTextAsync("Your feedback");
    }

    public async Task VerifyApprenticeFeedbackRating(string period, string expectedRating)
    {
        await Assertions.Expect(page.Locator($"#apprentice-rating-description-{period}")).ToContainTextAsync(expectedRating);
    }

    public async Task SelectApprenticeTabForAcademicYear(string academicYear)
    {
        await page.Locator($"#tab_app-{academicYear}").ClickAsync();
    }

    public async Task VerifyEmployerFeedbackRating(string period, string expectedRating)
    {
        await Assertions.Expect(page.Locator($"#employer-rating-description-{period}")).ToContainTextAsync(expectedRating);
    }

    public async Task SelectEmployerTabForAcademicYear(string academicYear)
    {
        await page.Locator($"#tab_emp-{academicYear}").ClickAsync();
    }

    public async Task VerifyText(string expectedText)
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(expectedText);
    }
}