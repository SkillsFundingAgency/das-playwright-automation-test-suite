namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public class ReviewYourDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Review your details");
    }

    public async Task VerifyInfoTextInReviewYourDetailsPage(string expectedText)
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(expectedText, new LocatorAssertionsToContainTextOptions { IgnoreCase = true });
    }

    public async Task<DetailsUpdatedPage> SelectUpdateMyDetailsRadioOptionAndContinueInReviewYourDetailsPage()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, update my details using" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new DetailsUpdatedPage(context));
    }
}
