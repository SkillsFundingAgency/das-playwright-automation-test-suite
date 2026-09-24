
namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAASearchResultPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    //private static By VacancyName => By.ClassName("das-search-results__link");
    //private static By FavouriteIcon => By.CssSelector("[data-add-favourite=true]");
    //private static By SavedVacancyNavBarLink => By.LinkText("Saved vacancies");
    //private static By ApplyNow => By.CssSelector(".das-button--inline-link");
    //private static By FirstApplicationDisplayed => By.CssSelector("[id^='VAC'][id$='-vacancy-title']");

    public override async Task VerifyPage() => await Assertions.Expect(page.Locator(".govuk-heading-l")).ToContainTextAsync(new Regex(@"results?\sfound"));
    private static string ClickFirstNHSLinkInResult => ("[id$='-vacancy-title']:first-of-type");

    public async Task VerifySuccessfulResults()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(new Regex("results? found"));
    }

    public async Task ClickSignout() => await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

    public async Task<FAA_ApplicationOverviewPage> SaveFromSearchResultsAndApplyForVacancy()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Save   to your favourites" }).First.ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Saved vacancies" }).ClickAsync();

        var vacancyCount = await page.Locator("ol.das-search-results__list")
            .GetByRole(AriaRole.Link, new() { Name = vacancyTitleDataHelper.VacancyTitle }).CountAsync();

        Assert.That(vacancyCount, Is.EqualTo(1));

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply now" }).First.ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    public async Task<FAASearchResultPage> RemoveSavedVacancyFromSearchResultsAndApplyForVacancy()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Saved   to your favourites, click again to remove" }).First.ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Saved vacancies" }).ClickAsync();

        var vacancyCount = await page.Locator("ol.das-search-results__list")
            .GetByRole(AriaRole.Link, new() { Name = vacancyTitleDataHelper.VacancyTitle }).CountAsync();

        Assert.That(vacancyCount, Is.EqualTo(0));

        await SearchUsingVacancyTitle();

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }

    public async Task<FAA_ApprenticeSummaryPage> ClickFirstApprenticeshipThatCanBeAppliedFor()
    {
        var contextVacancyTitle = objectContext.Get("vacancyTitle");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "What" }).FillAsync(contextVacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).First.ClickAsync();

        await page.GetByRole(AriaRole.Heading, new() { Name = contextVacancyTitle }).First.GetByRole(AriaRole.Link).ClickAsync();

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

    public async Task VerifySortOrder(string expectedSortOrder)
    {
        var selectedOptionText = await page.Locator("#sort-results option:checked").TextContentAsync();

        Assert.That(
            selectedOptionText?.Trim(),
            Is.EqualTo(expectedSortOrder),
            $"Expected sort order to be '{expectedSortOrder}' but found '{selectedOptionText}'");
    }

    public async Task SearchByWhereOnSearchResultsPage(string whereText)
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Where" }).FillAsync(whereText);

        await page.GetByRole(AriaRole.Option, new() { Name = whereText, Exact = false }).First.ClickAsync();

        await page.Locator("#within").SelectOptionAsync("40");

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).First.ClickAsync();
    }
}

public class NHSJobsDetailsPage(ScenarioContext context) : FAASignedInLandingBasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Apply on NHS Jobs");

}