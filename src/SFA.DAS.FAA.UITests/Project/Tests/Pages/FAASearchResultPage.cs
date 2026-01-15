
namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAASearchResultPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    //private static By VacancyName => By.ClassName("das-search-results__link");
    //private static By FavouriteIcon => By.CssSelector("[data-add-favourite=true]");
    //private static By SavedVacancyNavBarLink => By.LinkText("Saved vacancies");
    //private static By ApplyNow => By.CssSelector(".das-button--inline-link");
    //private static By FirstApplicationDisplayed => By.CssSelector("[id^='VAC'][id$='-vacancy-title']");

    private static string ClickFirstNHSLinkInResult => ("[id$='-vacancy-title']:first-of-type");

    public async Task VerifySuccessfulResults()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(new Regex("results? found"));
    }

    public async Task ClickSignout() => await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

    public async Task<FAA_ApplicationOverviewPage> SaveFromSearchResultsAndApplyForVacancy()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Follow the link to   Save" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Saved vacancies" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Saved vacancies" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    public async Task<FAA_ApprenticeSummaryPage> ClickFirstApprenticeshipThatCanBeAppliedFor()
    {
        var contextVacancyTitle = objectContext.Get("vacancyTitle");

        await page.GetByRole(AriaRole.Link, new() { Name = contextVacancyTitle }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApprenticeSummaryPage(context));
    }

    public async Task<NHSJobsDetailsPage> GoToNHSJobDetailsPageAndVerifyJobDisplayed()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Apprenticeship type , Show" }).ClickAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Apprenticeship", Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).First.ClickAsync();

        await page.Locator(ClickFirstNHSLinkInResult).First.ClickAsync();

        return await VerifyPageAsync(() => new NHSJobsDetailsPage(context));
    }
}

public class NHSJobsDetailsPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("See more details about this apprenticeship on NHS Jobs");

}