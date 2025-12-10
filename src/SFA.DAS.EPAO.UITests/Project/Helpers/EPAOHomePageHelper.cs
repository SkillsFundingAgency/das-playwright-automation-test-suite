using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;
using SFA.DAS.Framework;
using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.EPAO.UITests.Project.Helpers;

public class EPAOHomePageHelper : FrameworkBaseHooks
{
    private readonly EPAOApplySqlDataHelper _ePAOSqlDataHelper;
    private readonly DfeAdminLoginStepsHelper _dfeAdminLoginStepsHelper;

    public EPAOHomePageHelper(ScenarioContext context) : base(context)
    {
        _ePAOSqlDataHelper = context.Get<EPAOApplySqlDataHelper>();
        _dfeAdminLoginStepsHelper = new DfeAdminLoginStepsHelper(this.context);
    }

    public async Task<StaffDashboardPage> LoginToEpaoAdminHomePage(bool openInNewTab)
    {
        var url = UrlConfig.Admin_BaseUrl;

        await OpenUrl(url, openInNewTab);

        await _dfeAdminLoginStepsHelper.CheckAndLoginToAsAdmin();

        return await VerifyPageHelper.VerifyPageAsync(() => new StaffDashboardPage(context));
    }

    public async Task<AS_LandingPage> GoToEpaoAssessmentLandingPage(bool openInNewTab = false)
    {
        var url = UrlConfig.EPAOAssessmentService_BaseUrl;

        await OpenUrl(url, openInNewTab);

        return await VerifyPageHelper.VerifyPageAsync(() => new AS_LandingPage(context));
    }

    public async Task<AP_PR1_SearchForYourOrganisationPage> LoginInAsApplyUser(GovSignUser loginUser)
    {
        await StubSign(loginUser);

        return await VerifyPageHelper.VerifyPageAsync(() => new AP_PR1_SearchForYourOrganisationPage(context));
    }

    public async Task<AS_LoggedInHomePage> LoginInAsNonApplyUser(GovSignUser loginUser) { await StubSign(loginUser); return new AS_LoggedInHomePage(context); }

    public async Task<AS_LoggedInHomePage> LoginInAsStandardApplyUser(GovSignUser loginUser, string standardcode, string organisationId)
    {
        await _ePAOSqlDataHelper.DeleteStandardApplicication(standardcode, organisationId, loginUser.Username);

        return await LoginInAsNonApplyUser(loginUser);
    }

    private async Task OpenUrl(string url, bool openInNewTab)
    {
        if (openInNewTab) { await OpenNewTab(); }

        await Navigate(url);
    }

    public async Task<AS_LoggedInHomePage> StageTwoEPAOStandardCancelUser(GovSignUser loginUser) => await LoginInAsNonApplyUser(loginUser);

    private async Task StubSign(GovSignUser loginUser)
    {
        var page = await GoToEpaoAssessmentLandingPage();

        var page1 = await page.GoToStubSign();

        var page2 = await page1.SubmitValidUserDetails(loginUser);

        await page2.Continue();

        context.Get<EPAOAdminDataHelper>().LoginEmailAddress = loginUser.Username;

        context.Set(new EPAOAssessorPortalLoggedInUser { Username = loginUser.Username, IdOrUserRef = loginUser.IdOrUserRef });
    }
}
