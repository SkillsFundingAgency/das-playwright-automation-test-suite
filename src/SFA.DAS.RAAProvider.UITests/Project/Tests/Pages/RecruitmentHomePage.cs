
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

    public async Task<ReferVacancyPage> SearchReferAdvertTitle()
    {
        await _searchVacancyPageHelper.SearchProviderVacancy();
        await GoToReferredVacancyCheckYourAnswersPage();
        return await VerifyPageAsync(() => new ReferVacancyPage(context));
    }

    public async Task<ReferVacancyPage> GoToReferredVacancyCheckYourAnswersPage()
    {
        await page.Locator(".govuk-table__cell a").First.ClickAsync();

        return await VerifyPageAsync(() => new ReferVacancyPage(context));
    }

    public async Task<ManageYourRecruitmentEmailsPage> GoToManageYourRecruitmentEmailsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage recruitment emails" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageYourRecruitmentEmailsPage(context));
    }

    public async Task<ApprenticeRequestsPage> NavigateToApprenticeRequestsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Apprentice requests" }).ClickAsync();
        return await VerifyPageAsync(() => new ApprenticeRequestsPage(context));
    }

    public async Task<ReportsPage> GoToReportsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Reports" }).ClickAsync();
        return await VerifyPageAsync(() => new ReportsPage(context));
    }

    public async Task<ManageFundingPage> NavigateToManageFundingPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage funding" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageFundingPage(context));
    }

    public async Task<ManageYourApprenticePage> NavigateToManageYourApprenticesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage your apprentices" }).ClickAsync();
        return await VerifyPageAsync(() => new ManageYourApprenticePage(context));
    }
    public async Task<OrganisationsAndAgreementsPage> NavigateToOrganisationsAndAgreementsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "More" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "View employers and manage permissions" }).ClickAsync();
        return await VerifyPageAsync(() => new OrganisationsAndAgreementsPage(context));
    }
    public async Task<NotificationsSettingsPage> GoToNotificationsSettingsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Settings" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Notification settings" }).ClickAsync();
        return await VerifyPageAsync(() => new NotificationsSettingsPage(context));
    }
    public async Task<ChangeYourSignInDetailsPage> GoToChangeYourSignInDetailsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Settings" }).ClickAsync();
        await page.GetByRole(AriaRole.Link, new() { Name = "Change your sign-in details" }).ClickAsync();
        return await VerifyPageAsync(() => new ChangeYourSignInDetailsPage(context));
    }
}
