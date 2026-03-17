using System.Collections.Generic;

﻿namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

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
    private static string AllVacancyLocator => "[id^='VAC'][id$='-vacancy-title']";

    public async Task<FAA_ApplicationsPage> GoToApplications()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Your applications" }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApplicationsPage(context));
    }

    //public FAASearchApprenticeLandingPage GoToSearch()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Search" }).ClickAsync();

    //    return new(context);
    //}

    public async Task<FAA_ApprenticeSummaryPage> SearchByReferenceNumber()
    {
        await SearchUsingVacancyTitle();

        await GoToVacancyInFAA();

        return await VerifyPageAsync(() => new FAA_ApprenticeSummaryPage(context));
    }

    public async Task SearchForFoundationCourseAndApply()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Search" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search apprenticeships", new LocatorAssertionsToContainTextOptions { Timeout = 10000});

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Apprenticeship type , Show" }).ClickAsync();

        await page.GetByRole(AriaRole.Checkbox, new() { Name = "Foundation apprenticeship" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Apply filters" }).Nth(1).ClickAsync();

        await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "Foundation" }).GetByRole(AriaRole.Link, new() { Name = "apprenticeship" }).First.ClickAsync();

        await CheckFoundationTag();

        await Assertions.Expect(page.Locator("#summary")).ToContainTextAsync("You cannot apply for a foundation apprenticeship if you’re 25 or over.");
    }

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

        const int maxPageIterations = 20;
        var attempts = 0;
        IReadOnlyList<string>? allvacancy = null;
        List<string> vacancies = new();

        while (attempts < maxPageIterations)
        {
            allvacancy = await AllTextAsync(AllVacancyLocator);

            if (allvacancy != null)
            {
                vacancies = allvacancy.Where(vacancy => !string.IsNullOrEmpty(vacancy) && !vacancy.Contains("from NHS jobs", StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (vacancies.Count > 0)
            {
                break;
            }

            var next = page.GetByRole(AriaRole.Link, new() { Name = "Next" });
            if (await next.CountAsync() == 0)
            {
                break;
            }

            await next.First.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            attempts++;
        }

        if (vacancies == null || vacancies.Count == 0)
            throw new Exception("No vacancy found that does not contain 'from NHS jobs' after paging.");

        var vacancyTitle = RandomDataGenerator.GetRandom(allvacancy);

        objectContext.Set("vacancyTitle", vacancyTitle);

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }

    public async Task<FAASearchResultPage> SearchAndSaveVacancyByReferenceNumber()
    {
        await SearchUsingVacancyTitle();

        return await VerifyPageAsync(() => new FAASearchResultPage(context));
    }

    public async Task<FAABrowseByInterestsPage> ClickBrowseByYourInterests()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Browse by your interests" }).ClickAsync();

        return await VerifyPageAsync(() => new FAABrowseByInterestsPage(context));
    }
}
