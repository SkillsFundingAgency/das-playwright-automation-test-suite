namespace SFA.DAS.FATe.UITests.Project.Tests.Pages;

public class FATeHomePage(ScenarioContext context) : FATeBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Find apprenticeship training for your apprentice");
    public async Task<FATeHomePage> AcceptCookieAndAlert()
    {
        await AcceptAllCookies();

        await page.Locator(".das-cookie-banner__hide-button").ClickAsync();

        return await VerifyPageAsync(() => new FATeHomePage(context));
    }
    public async Task<Search_TrainingCourses_ApprenticeworkLocationPage> ClickStartNow()
    {
        await page.Locator("a.govuk-button--start").ClickAsync();
        return await VerifyPageAsync(() => new Search_TrainingCourses_ApprenticeworkLocationPage(context));
    }
    public async Task StartNow()
    {
        await page.Locator("a.govuk-button--start").ClickAsync();
    }
}

