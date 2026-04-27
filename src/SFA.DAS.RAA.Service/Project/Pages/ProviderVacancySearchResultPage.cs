using System;
using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class VacancySearchResultsPage(ScenarioContext context) : RaaBasePage(context)
{
    protected async Task DraftVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Draft vacancies");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by vacancy title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and submit" }).ClickAsync();
    }

    public async Task<ManageRecruitPage> GoToVacancyManagePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage"}).First.ClickAsync();

        return await VerifyPageAsync(() => new ManageRecruitPage(context));
    }
}

public class ProviderVacancySearchResultPage(ScenarioContext context) : VacancySearchResultsPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("All vacancies");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipVacancyPage()
    {
        await DraftVacancy();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }

    public async Task<ManageApplicantPage> NavigateToManageApplicant()
    {
        await GoToVacancyManagePage();

        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }

        var newApplicationRow = page.Locator("tr.govuk-table__row", new() { Has = page.Locator("strong.govuk-tag", new() { HasTextString = "New" })}).First;

        await newApplicationRow.Locator("a[data-label='application_review']").ClickAsync();

        return await VerifyPageAsync(() => new ManageApplicantPage(context));
    }

    public async Task CheckApplicantStatus(string status)
    {
        await GoToVacancyManagePage();

        if (IsFoundationAdvert)
        {
            await CheckFoundationTag();
        }

        await Assertions.Expect(page.Locator("td[data-label='Status'] > strong")).ToContainTextAsync(status);
    }

    public async Task<ViewVacancyPage> NavigateToViewAdvertPage()
    {
        await GoToVacancyManagePage();

        string linkTest = isRaaEmployer ? "View advert" : "View vacancy";

        await page.GetByRole(AriaRole.Link, new() { Name = linkTest, Exact = true }).ClickAsync();

        return await VerifyPageAsync(() => new ViewVacancyPage(context));
    }
}

public class ProviderDraftVacanciesListPage(ScenarioContext context) : VacancySearchResultsPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Draft vacancies");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipVacancyPage()
    {
        await DraftVacancy();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}
