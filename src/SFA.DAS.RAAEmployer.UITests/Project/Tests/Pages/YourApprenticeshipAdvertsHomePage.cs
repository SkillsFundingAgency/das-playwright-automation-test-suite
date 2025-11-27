using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.RAA.Service.Project.Helpers;
using SFA.DAS.RAA.Service.Project.Pages;

namespace SFA.DAS.RAAEmployer.UITests.Project.Tests.Pages;

public class YourApprenticeshipAdvertsHomePage(ScenarioContext context, bool navigate = false, bool gotourl = false) : InterimYourApprenticeshipAdvertsHomePage(context, navigate, gotourl)
{

    private readonly SearchVacancyPageHelper _searchVacancyPageHelper = new(context);

    //protected override By AcceptCookieButton => By.CssSelector("#btn-cookie-accept");
    //private static By CreateAnAdvertButton => By.LinkText("Create an advert");


    //private static By RecruitmentAPIsLink => By.LinkText("Recruitment APIs");
    //private static By ManageYourEmailsLink => By.LinkText("Manage your emails");
    //private static By ReturnToDashboardLink => By.LinkText("Return to dashboard");

    //public YourApprenticeshipAdvertsHomePage ClickReturnToDashboard()
    //{
    //    formCompletionHelper.Click(ReturnToDashboardLink);

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

        page.SetDefaultNavigationTimeout(10000);

        page.SetDefaultTimeout(15000);

        await page.GetByRole(AriaRole.Link, new() { Name = "Create an advert" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateAnAdvertHomePage(context));
    }

    public async Task<ManageRecruitPage> SelectLiveAdvert() => await _searchVacancyPageHelper.SelectLiveAdvert();

    public async Task<EmployerVacancySearchResultPage> SearchAdvertByReferenceNumber() => await _searchVacancyPageHelper.SearchEmployerVacancy();

    public async Task<ManageYourAdvertEmailsPage> GoToAdvertNotificationsPage()
    {
        await NavigateToSettings("Manage your advert");

        return await VerifyPageAsync(() => new ManageYourAdvertEmailsPage(context));
    }

    //public GetStartedWithRecruitmentAPIsPage ClickRecruitmentAPILink()
    //{
    //    formCompletionHelper.Click(RecruitmentAPIsLink);
    //    return new GetStartedWithRecruitmentAPIsPage(context);
    //}

    //public ManageYourEmailsPage ClickMangeYourEmailsLink()
    //{
    //    formCompletionHelper.Click(ManageYourEmailsLink);
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