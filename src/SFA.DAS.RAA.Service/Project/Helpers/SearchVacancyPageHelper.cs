using Azure;
using SFA.DAS.RAA.DataGenerator.Project;
using SFA.DAS.RAA.Service.Project.Pages;
using System.Text.RegularExpressions;

namespace SFA.DAS.RAA.Service.Project.Helpers;

public class SearchVacancyPageHelper(ScenarioContext context)
{
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    protected readonly IPage page = context.Get<Driver>().Page;

    //private static By SearchInput => By.CssSelector("input#search-input");

    //private static By SearchButton => By.CssSelector(".govuk-button.das-search-form__button");

    //private static By Manage => By.CssSelector("[data-label='Action']");
    //private static By ProviderManage => By.CssSelector("td[data-label='Action'] a.govuk-link");


    //public async Task<ManageRecruitPage> SelectLiveVacancy()
    //{
    //    _formCompletionHelper.ClickLinkByText("Live vacancies");
    //    _pageInteractionHelper.WaitforURLToChange($"filter=Live");
    //    _formCompletionHelper.ClickElement(RandomDataGenerator.GetRandomElementFromListOfElements(_pageInteractionHelper.FindElements(ProviderManage)));
    //    return await VerifyPageHelper.VerifyPageAsync(() => new ManageRecruitPage(context));
    //}

    public async Task<ManageRecruitPage> SelectLiveAdvert()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Live adverts" }).ClickAsync();

        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your adverts");

        var locators = await page.GetByRole(AriaRole.Row, new() { Name = "VAC" }).Filter(new LocatorFilterOptions { HasNotTextString = "Foundation" }).GetByRole(AriaRole.Link).AllAsync();

        var locator = RandomDataGenerator.GetRandom(locators);

        await locator.ClickAsync();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new ManageRecruitPage(context));
    }

    //public async Task<ProviderVacancySearchResultPage> SearchVacancyByVacancyReference()
    //{
    //    await SearchVacancy();
    //    return await VerifyPageHelper.VerifyPageAsync(() => new ProviderVacancySearchResultPage(context));
    //}

    //public async Task<ProviderVacancySearchResultPage> SearchProviderVacancy()
    //{
    //    await SearchVacancy();
    //    return await VerifyPageHelper.VerifyPageAsync(() => new ProviderVacancySearchResultPage(context));
    //}

    //public async Task<VacancyCompletedAllSectionsPage> SearchReferVacancy()
    //{
    //    await SearchVacancy();
    //    return await VerifyPageHelper.VerifyPageAsync(() => new VacancyCompletedAllSectionsPage(context));
    //}

    public async Task<EmployerVacancySearchResultPage> SearchEmployerVacancy()
    {
        await SearchVacancy();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new EmployerVacancySearchResultPage(context));
    }

    internal async Task SearchVacancy()
    {
        var vacRef = _objectContext.GetVacancyReference();

        await page.GetByRole(AriaRole.Combobox, new() { Name = "Search by advert title or" }).FillAsync(vacRef);
        
        await page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

        await Assertions.Expect(page).ToHaveURLAsync(new Regex($"searchTerm={vacRef}"), new PageAssertionsToHaveURLOptions { IgnoreCase = true, Timeout = 20000 });
    }
}
