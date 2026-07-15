using SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

namespace SFA.DAS.RAA.Service.Project.Pages;

public abstract class VacancySearchResultPage(ScenarioContext context) : RaaBasePage(context)
{
    //protected static By Filter => By.CssSelector("#Filter");
    //private static By SearchInput => By.CssSelector("input#search-input");
    //protected static By VacancyStatusSelector => By.CssSelector("[data-label='Status']");

    //protected static By VacancyActionSelector => By.CssSelector("[id^='manage']");
    //protected static By RejectedVacancyActionSelector => By.CssSelector("[data-label='Action']");
    //private static By SearchButton => By.CssSelector(".govuk-button.das-search-form__button");

    protected async Task DraftVacancy()
    {
        //await page.GetByLabel("Filter adverts by").SelectOptionAsync(new[] { "All" });

        //await page.GetByLabel("Filter adverts by").SelectOptionAsync(new[] { "Draft" });

        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Draft adverts");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and submit" }).ClickAsync();
    }

    protected async Task ReviewVacancy()
    {
        await Assertions.Expect(page.Locator(".govuk-heading-xl")).ToContainTextAsync("Adverts with shared applications");

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Review" }).ClickAsync();
    }

    public async Task <VacancyCompletedAllSectionsPage> GoToVacancyCompletedPage()
    {
        await page.Locator("[id^='manage']").ClickAsync();

        return await VerifyPageAsync(() => new VacancyCompletedAllSectionsPage(context));
    }

    public async Task<ManageRecruitPage> GoToVacancyManagePage()
    {
        string linkText = isRaaEpc ? "Review" : "Manage";
        await page.GetByRole(AriaRole.Link, new() { Name = linkText}).First.ClickAsync();

        return await VerifyPageAsync(() => new ManageRecruitPage(context));
    }

    public async Task<SharedApplicatinsForAVacancyPage> GoToSharedAppsManagePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Review" }).First.ClickAsync();

        return await VerifyPageAsync(() => new SharedApplicatinsForAVacancyPage(context));
    }
}

public class EmployerVacancySearchResultPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("All adverts");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertPage()
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

public class EmployerDraftVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Draft adverts");
    }

    public async Task<CreateAnApprenticeshipAdvertOrVacancyPage> CreateAnApprenticeshipAdvertPage()
    {
        await DraftVacancy();

        return await VerifyPageAsync(() => new CreateAnApprenticeshipAdvertOrVacancyPage(context));
    }
}

public class EmployerSharedApplicationsVacanciesListPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Adverts with shared applications");
    }

    public async Task<ManageApplicantPage> NavigateToManageApplicant()
    {
        await GoToSharedAppsManagePage();

        var newApplicationRow = page.Locator("tr.govuk-table__row", new() { Has = page.Locator("strong.govuk-tag", new() { HasTextString = "Response needed" }) }).First;

        await newApplicationRow.Locator("a[data-label='application_review']").ClickAsync();

        return await VerifyPageAsync(() => new ManageApplicantPage(context));
    }
}