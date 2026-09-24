namespace SFA.DAS.FAA.UITests.Project.Tests.Pages;

public class FAA_ApprenticeSummaryPage(ScenarioContext context) : FAABasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#vacancy-title")).ToContainTextAsync(objectContext.Get("vacancyTitle"));

    //private static By ViewSubmittedApplicationLink => By.CssSelector("a[href*='Submitted']");

    //private static By SaveVacancyLink => By.XPath("//span[normalize-space()='Save vacancy']");

    //private static By SavedVacanciesNavBar => By.XPath("//a[normalize-space()='Saved vacancies']");
    //private static By SavedVacancyLink => By.CssSelector(".govuk-link.govuk-link--no-visited-state");
    //private static By VacancyName => By.CssSelector(".govuk-heading-l faa-vacancy__title");


    public async Task<FAA_ApplicationOverviewPage> Apply()
    {
        var continueLink = page.Locator("a:has-text('Continue your application')");

        if (await continueLink.CountAsync() > 0)
        {
            await continueLink.First.ClickAsync();
        }
        else
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Apply for apprenticeship" }).ClickAsync();
        }
        return await VerifyPageAsync(() => new FAA_ApplicationOverviewPage(context));
    }

    //public FAA_SubmittedApplicationPage ViewSubmittedApplications()
    //{
    //    formCompletionHelper.Click(ViewSubmittedApplicationLink);
    //    return new FAA_SubmittedApplicationPage(context);
    //}

    //public FAA_ApprenticeSummaryPage ConfirmDraftVacancyDeletion()
    //{
    //    pageInteractionHelper.VerifyText(ApplyButton, "Continue your application");
    //    return this;
    //}

    public async Task<FAA_ApprenticeSummaryPage> SaveAndApplyForVacancy()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Follow the link to   Save" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Saved vacancies" }).ClickAsync();

        var vacancyCount = await page.Locator("ol.das-search-results__list")
            .GetByRole(AriaRole.Link, new() { Name = vacancyTitleDataHelper.VacancyTitle}).CountAsync();

        Assert.That(vacancyCount, Is.EqualTo(1));

        await page.GetByRole(AriaRole.Link, new() { Name = vacancyTitleDataHelper.VacancyTitle }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApprenticeSummaryPage(context));
    }

    public async Task<FAA_ApprenticeSummaryPage> RemoveSavedVacancy()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Saved vacancies" }).ClickAsync();

        var vacancyItem = page.Locator("li.das-search-results__list-item").Filter(new() { HasText = vacancyTitleDataHelper.VacancyTitle });

        await vacancyItem.GetByRole(AriaRole.Button, new() { Name = "Remove" }).ClickAsync();

        await Assertions.Expect(page.Locator(".govuk-notification-banner__heading")).ToContainTextAsync($"Removed {vacancyTitleDataHelper.VacancyTitle}");

        await page.GetByRole(AriaRole.Link, new() { Name = vacancyTitleDataHelper.VacancyTitle }).ClickAsync();

        return await VerifyPageAsync(() => new FAA_ApprenticeSummaryPage(context));
    }
}
