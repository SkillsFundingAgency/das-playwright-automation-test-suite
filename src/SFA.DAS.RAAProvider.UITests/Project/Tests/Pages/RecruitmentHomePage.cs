
using SFA.DAS.ProviderLogin.Service.Project.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

public class RecruitmentHomePage(ScenarioContext context) : InterimProviderBasePage(context)
{
    private readonly SearchVacancyPageHelper _searchVacancyPageHelper = new(context);
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Recruitment dashboard");
    }

    public async Task<CreateAVacancyPage> CreateVacancy()
    {
        await page.Locator("a[data-automation='create-vacancy']").ClickAsync();
        return await VerifyPageAsync(() => new CreateAVacancyPage(context));
    }

    public async Task<ViewAllVacancyPage> GoToViewAllVacancyPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "View all vacancies" }).ClickAsync();
        return await VerifyPageAsync(() => new ViewAllVacancyPage(context));
    }

    public async Task<GetStartedWithRecruitmentAPIsPage> NavigateToRecruitmentAPIs()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Recruitment APIs" }).ClickAsync();
        return await VerifyPageAsync(() => new GetStartedWithRecruitmentAPIsPage(context));
    }

     public async Task<ProviderDraftVacanciesListPage> GoToYourAdvertFromDraftAdverts()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Draft vacancies" }).ClickAsync();
        return await VerifyPageAsync(() => new ProviderDraftVacanciesListPage(context));
    }

    public async Task<ProviderVacancySearchResultPage> SearchVacancyByVacancyReference()
    {
        await _searchVacancyPageHelper.SearchProviderVacancy();
        return await VerifyPageAsync(() => new ProviderVacancySearchResultPage(context));
    } 

    public async Task<ProviderVacancySearchResultPage> SearchVacancy() => await _searchVacancyPageHelper.SearchProviderVacancy();

    public async Task<ManageRecruitPage> SelectLiveVacancy() => await _searchVacancyPageHelper.SelectLiveVacancy();

    // public async Task<ReferVacancyPage> SearchReferAdvertTitle()
    // {

    // }

    public async Task<ManageYourRecruitmentEmailsPage> GoToManageYourRecruitmentEmailsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage recruitment emails" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageYourRecruitmentEmailsPage(context));
    }

}
