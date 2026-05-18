
using Azure;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Helpers;

public class RecruitmentProviderHomePageStepsHelper(ScenarioContext context)
{
// private static By RecruitApprenticesLink => By.CssSelector("a.das-navigation__link[href*='recruit']");


    public async Task<RecruitmentHomePage> GoToRecruitmentProviderHomePage(bool newTab)
    {
        await new ProviderHomePageStepsHelper(context).GoToProviderHomePageAndClickRecruitVacanciesLink(newTab);
        return new RecruitmentHomePage(context);
    }


    public async Task<ReportsPage> GoToReportsPage()
    {
        var page = new RecruitmentHomePage(context);
        return await page.GoToReportsPage();
    }
    // public async Task<RecruitmentHomePage> NavigateToRecruitmentHomePage()
    // {
    //     await Page.
    //     return new RecruitmentHomePage(context);
    // }

    public async Task<ManageYourRecruitmentEmailsPage> GoToManageYourRecruitmentEmailsPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.GoToManageYourRecruitmentEmailsPage();
    }

    public async Task<ApprenticeRequestsPage> GoToApprenticeRequestsPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.NavigateToApprenticeRequestsPage();
    }

    public async Task<ManageFundingPage> GoToManageFundingPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.NavigateToManageFundingPage();
    }

    public async Task<ManageYourApprenticePage> GoToManageYourApprenticesPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.NavigateToManageYourApprenticesPage();
    }

    public async Task<OrganisationsAndAgreementsPage> GoToOrganisationsAndAgreementsPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.NavigateToOrganisationsAndAgreementsPage();
    }

    public async Task<NotificationsSettingsPage> GoToNotificationsViaSettingsPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.GoToNotificationsSettingsPage();
    }

    public async Task<ChangeYourSignInDetailsPage> GoToChangeYourSignInDetailsPage(bool newTab = false)
    {
        var page = await GoToRecruitmentProviderHomePage(newTab);
        return await page.GoToChangeYourSignInDetailsPage();
    }
}
