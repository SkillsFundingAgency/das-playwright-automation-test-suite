namespace SFA.DAS.ProvideFeedback.UITests.Project.Tests.Pages;

public abstract class ProviderFeedbackBasePage(ScenarioContext context) : BasePage(context)
{
    //protected static By NavigationBarFeedbackLink => By.CssSelector(".app-navigation__link[href*='feedback']");

    //protected static By ContinueToSubmitButton => By.CssSelector("button.govuk-button[type=submit]");

    //protected async Task ClickContinueButton() => formCompletionHelper.ClickButtonByText(ContinueToSubmitButton, "Continue");

    //public ApprenticeFeedbackSelectProviderPage NavigateToGiveFeedbackOnYourTrainingProvider()
    //{
    //    formCompletionHelper.Click(NavigationBarFeedbackLink);
    //    return new(context);
    //}
}

public class FeedbackProviderHomePage(ScenarioContext context) : ProviderHomePage(context)
{
    public async Task<FeedbackOverviewPage> SelectYourFeedback()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your feedback" }).ClickAsync();

        return await VerifyPageAsync(() => new FeedbackOverviewPage(context));
    }
}

public class FeedbackOverviewPage(ScenarioContext context) : ProviderFeedbackBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#your-feedback")).ToContainTextAsync("Your feedback");
    }

    public async Task VerifyApprenticeFeedbackRating(string period, string expectedRating)
    {
        await Assertions.Expect(page.Locator($"apprentice-rating-description-{period}")).ToContainTextAsync(expectedRating);
    }

    public async Task SelectApprenticeTabForAcademicYear(string academicYear)
    {
        await page.Locator($"tab_app-{academicYear}").ClickAsync();
    }

    public async Task VerifyEmployerFeedbackRating(string period, string expectedRating)
    {
        await Assertions.Expect(page.Locator($"employer-rating-description-{period}")).ToContainTextAsync(expectedRating);
    }

    public async Task SelectEmployerTabForAcademicYear(string academicYear)
    {
        await page.Locator($"tab_emp-{academicYear}").ClickAsync();
    }

    public async Task VerifyText(string expectedText)
    {
        await Assertions.Expect(page.Locator("#app-All, #emp-All")).ToContainTextAsync(expectedText);
    }
}