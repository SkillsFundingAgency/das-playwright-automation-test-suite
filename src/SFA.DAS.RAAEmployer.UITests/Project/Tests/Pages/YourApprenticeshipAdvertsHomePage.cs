
namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

public class YourApprenticeshipAdvertsHomePage(ScenarioContext context, bool navigate = false, bool gotourl = false) : InterimYourApprenticeshipAdvertsHomePage(context, navigate, gotourl)
{

    private readonly SearchVacancyPageHelper _searchVacancyPageHelper = new(context);

    //public YourApprenticeshipAdvertsHomePage ClickReturnToDashboard()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Return to dashboard" }).ClickAsync();

    //    return new YourApprenticeshipAdvertsHomePage(context);
    //}

    public async Task<EmployerVacancySearchResultPage> GoToYourAdvertFromDraftAdverts()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Draft adverts" }).ClickAsync();

        return await VerifyPageAsync(() => new EmployerVacancySearchResultPage(context));
    }

    public async Task<CreateAnAdvertHomePage> CreateAnApprenticeshipAdvert()
    {
        await AcceptAllCookiesIfVisible();

        await page.GetByRole(AriaRole.Link, new() { Name = "Create an advert" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnAdvertHomePage(context));
    }

    public async Task<ManageRecruitPage> SelectLiveAdvert() => await _searchVacancyPageHelper.SelectLiveAdvert();

    public async Task<EmployerVacancySearchResultPage> SearchAdvertByReferenceNumber() => await _searchVacancyPageHelper.SearchEmployerVacancy();

    public async Task<ManageYourEmailsEmployerPage> GoToAdvertNotificationsPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Manage your emails" }).ClickAsync();

        return await VerifyPageAsync(() => new ManageYourEmailsEmployerPage(context));
    }

    public async Task<GetStartedWithRecruitmentAPIsPage> ClickRecruitmentAPILink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Recruitment APIs" }).ClickAsync();

        return await VerifyPageAsync(() => new GetStartedWithRecruitmentAPIsPage(context));
    }

    //public ManageYourEmailsPage ClickMangeYourEmailsLink()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Manage your emails" }).ClickAsync();
    //    return new ManageYourEmailsPage(context);
    //}
}

public class ManageYourAdvertEmailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Manage your advert notifications");
    }
}