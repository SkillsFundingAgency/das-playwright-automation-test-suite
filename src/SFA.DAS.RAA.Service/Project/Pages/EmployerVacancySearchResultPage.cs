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

    //public void VerifyAdvertStatus(string expected)
    //{
    //    VerifyElement(() => tableRowHelper.GetColumn(vacancyTitleDataHelper.VacancyTitle, VacancyStatusSelector), expected, () => new SearchVacancyPageHelper(context).SearchVacancy());
    //}

    protected async Task DraftVacancy()
    {
        await page.GetByLabel("Filter adverts by").SelectOptionAsync(new[] { "All" });

        await page.GetByLabel("Filter adverts by").SelectOptionAsync(new[] { "Draft" });

        await Assertions.Expect(page.Locator("h3")).ToContainTextAsync("draft adverts");

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Search by advert title or" }).FillAsync(vacancyTitleDataHelper.VacancyTitle);

        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await page.GetByRole(AriaRole.Row, new() { Name = vacancyTitleDataHelper.VacancyTitle }).GetByRole(AriaRole.Link, new() { Name = "Edit and submit" }).ClickAsync();
    }

    //public VacancyCompletedAllSectionsPage GoToVacancyCompletedPage()
    //{
    //    formCompletionHelper.ClickElement(VacancyActionSelector);

    //    return await VerifyPageAsync(() => new VacancyCompletedAllSectionsPage(context));
    //}
    //public VacancyCompletedAllSectionsPage GoToRejectedVacancyCompletedPage()
    //{
    //    formCompletionHelper.ClickElement(RejectedVacancyActionSelector);

    //    return await VerifyPageAsync(() => new VacancyCompletedAllSectionsPage(context));
    //}

    public async Task<ManageRecruitPage> GoToVacancyManagePage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage", Exact = true }).First.ClickAsync();

        return await VerifyPageAsync(() => new ManageRecruitPage(context));
    }
}

public class EmployerVacancySearchResultPage(ScenarioContext context) : VacancySearchResultPage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your adverts");
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

        await page.Locator("a[data-label='application_review']").ClickAsync();

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