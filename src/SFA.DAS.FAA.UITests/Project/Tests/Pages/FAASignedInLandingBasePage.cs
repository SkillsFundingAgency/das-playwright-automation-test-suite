namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAASignedInLandingBasePage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.GetByRole(AriaRole.Banner)).ToContainTextAsync("Sign out");

    //private static By SearchHeader => By.CssSelector("[id='service-header__nav'] a[href='/apprenticeships']");

    //private static By ApplicationsHeader => By.CssSelector("[id='service-header__nav'] a[href='/applications']");

    //private static By What => By.CssSelector("[id='WhatSearchTerm']");

    //private static By Where => By.CssSelector("[id='WhereSearchTerm']");

    private static string SearchFAA => (".form button");
    //private static By VacancyName => By.CssSelector("span[itemprop='title']");

    //private static By SavedVacancyLink => By.CssSelector(".govuk-link.govuk-link--no-visited-state");
    private static string AllVacancyLocator => ("[id^='VAC'][id$='-vacancy-title']");

    //public FAA_ApplicationsPage GoToApplications()
    //{
    //    formCompletionHelper.Click(ApplicationsHeader);

    //    return new(context);
    //}

    //public FAASearchApprenticeLandingPage GoToSearch()
    //{
    //    formCompletionHelper.Click(SearchHeader);

    //    return new(context);
    //}

    //public FAA_ApprenticeSummaryPage SearchByReferenceNumber()
    //{
    //    SearchUsingVacancyTitle();

    //    GoToVacancyInFAA();

    //    return new FAA_ApprenticeSummaryPage(context);
    //}

    public async Task SearchByWhatWhere(string whatText, string whereText)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What" }).FillAsync(whatText);

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Where" }).FillAsync(whereText);

        await page.GetByRole(AriaRole.Option, new() { Name = whereText, Exact = false }).First.ClickAsync();

        await page.Locator(SearchFAA).ClickAsync();
    }

    public async Task SearchByWhat(string whatText)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "What" }).FillAsync(whatText);

        await page.Locator(SearchFAA).ClickAsync();
    }

    public async Task SearchByWhere(string whereText)
    {
        await page.GetByRole(AriaRole.Combobox, new() { Name = "Where" }).FillAsync(whereText);

        await page.GetByRole(AriaRole.Option, new() { Name = whereText, Exact = false }).First.ClickAsync();

        await page.Locator(SearchFAA).ClickAsync();
    }

    public async Task<FAASearchResultPage> SearchAtRandom()
    {
        await page.Locator(SearchFAA).ClickAsync();

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }

    public async Task<FAASearchResultPage> SearchRandomVacancyAndGetVacancyTitle()
    {
        await page.Locator(SearchFAA).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Apprenticeship type , Show" }).ClickAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Apprenticeship", Exact = true }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).First.ClickAsync();

        await new FAASearchResultPage(context).VerifySuccessfulResults();

        var allvacancy = await AllTextAsync(AllVacancyLocator);

        var vacancyTitle = RandomDataGenerator.GetRandom(allvacancy);

        objectContext.Set("vacancyTitle", vacancyTitle);

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }

    //public FAASearchResultPage SearchAndSaveVacancyByReferenceNumber()
    //{
    //    SearchUsingVacancyTitle();

    //    return new FAASearchResultPage(context);
    //}

    public async Task<FAABrowseByInterestsPage> ClickBrowseByYourInterests()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Browse by your interests" }).ClickAsync();

        return await VerifyPageAsync(() => new FAABrowseByInterestsPage(context));
    }
}
