
using Azure;
using SFA.DAS.RAAProvider.UITests.Project.Tests.Pages;

namespace SFA.DAS.RAAProvider.UITests.Project.Helpers;

public class RecruitmentProviderHomePageStepsHelper(ScenarioContext context)
{
// private static By RecruitApprenticesLink => By.CssSelector("a.das-navigation__link[href*='recruit']");


    public async Task<RecruitmentHomePage> GoToRecruitmentProviderHomePage(bool newTab)
    {
        await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(newTab);
        return new RecruitmentHomePage(context);
    }
    // public ReportsPage GoToReportsPage()
    // {
        // var recruitmentHomePage = new RecruitmentHomePage(context);
        // return recruitmentHomePage.GoToReportsPage();
    // }
    // public async Task<RecruitmentHomePage> NavigateToRecruitmentHomePage()
    // {
    //     await Page.
    //     return new RecruitmentHomePage(context);
    // }

public async Task<ManageYourRecruitmentEmailsPage> GoToManageYourRecruitmentEmailsPage(bool newTab = false)
{
    var page = GoToRecruitmentProviderHomePage(newTab);
    return await page.GoToManageYourRecruitmentEmailsPage();
}
public ApprenticeRequestsPage GoToApprenticeRequestsPage(bool newTab = false)
{
    var recruitmentHomePage = GoToRecruitmentProviderHomePage(newTab);
    return recruitmentHomePage.NavigateToApprenticeRequestsPage();
}
public ManageFundingPage GoToManageFundingPage(bool newTab = false)
{
    var recruitmentHomePage = GoToRecruitmentProviderHomePage(newTab);
    return recruitmentHomePage.NavigateToManageFundingPage();
}
public ManageYourApprenticePage GoToManageYourApprenticesPage(bool newTab = false)
{
    var recruitmentHomePage = GoToRecruitmentProviderHomePage(newTab);
    return recruitmentHomePage.NavigateToManageYourApprenticesPage();
}
public OrganisationsAndAgreementsPage GoToOrganisationsAndAgreementsPage(bool newTab = false)
{
    var recruitmentHomePage = GoToRecruitmentProviderHomePage(newTab);
    return recruitmentHomePage.NavigateToOrganisationsAndAgreementsPage();
}
public NotificationsSettingsPage GoToNotificationsViaSettingsPage(bool newTab = false)
{
    var recruitmentHomePage = GoToRecruitmentProviderHomePage(newTab);
    return recruitmentHomePage.GoToNotificationsSettingsPage();
}
public ChangeYourSignInDetailsPage GoToChangeYourSignInDetailsPage(bool newTab = false)
{
    var recruitmentHomePage = GoToRecruitmentProviderHomePage(newTab);
    return recruitmentHomePage.GoToChangeYourSignInDetailsPage();
}
}
