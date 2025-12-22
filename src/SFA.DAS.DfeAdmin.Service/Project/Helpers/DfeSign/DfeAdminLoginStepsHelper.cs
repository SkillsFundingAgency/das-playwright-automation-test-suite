using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;
using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using SFA.DAS.Framework.Hooks;
using SFA.DAS.Login.Service.Project;

namespace SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;

public class DfeAdminLoginStepsHelper(ScenarioContext context) : FrameworkBaseHooks(context)
{
    #region Login
    public async Task NavigateAndLoginToASAdmin()
    {
        await Navigate(UrlConfig.Admin_BaseUrl);

        await LoginToAsAdmin();
    }

    public async Task LoginToAsAssessor1() => await CheckAndLoginToAsAdmin(context.GetUser<AsAssessor1User>());

    public async Task LoginToAsAssessor2() => await CheckAndLoginToAsAdmin(context.GetUser<AsAssessor2User>());

    public async Task LoginToQfastAsAdmin() => await SubmitValidLoginDetails(context.GetUser<QfastDfeAdminUser>());
    public async Task LoginToQfastAsAOUser() => await SubmitValidLoginDetails(context.GetUser<QfastAOUser>());
    public async Task LoginToQfastAsIFATEUser() => await SubmitValidLoginDetails(context.GetUser<QfastIFATEUser>());
    public async Task LoginToQfastAsOFQUALUser() => await SubmitValidLoginDetails(context.GetUser<QfastOFQUALUser>());
    public async Task LoginToQfastAsDataImporterUser() => await SubmitValidLoginDetails(context.GetUser<QfastDataImporter>());
    public async Task LoginToQfastAsReviewerUser() => await SubmitValidLoginDetails(context.GetUser<QfastReviewer>());
    public async Task LoginToQfastAsFormEditorUser() => await SubmitValidLoginDetails(context.GetUser<QfastFormEditor>());

    public async Task LoginToAsAdmin() => await SubmitValidLoginDetails(new ASAdminLandingPage(context), GetAsAdminUser());

    public async Task LoginToSupportTool(DfeAdminUser dfeAdminUser) => await SubmitValidLoginDetails(new ASEmpSupportToolLandingPage(context), dfeAdminUser);

    public async Task SubmitValidAsLoginDetails(ASLandingCheckBasePage landingPage) => await SubmitValidLoginDetails(landingPage, GetAsAdminUser());

    #endregion

    #region CheckAndLogin

    public async Task CheckAndLoginToAsAdmin() => await CheckAndLoginToAsAdmin(GetAsAdminUser());
    
    public async Task CheckAndLoginToAsAdmin(DfeAdminUser dfeAdminUser) => await CheckAndLoginTo(new ASAdminLandingPage(context), dfeAdminUser);

    public async Task CheckAndLoginToSupportTool(DfeAdminUser dfeAdminUser) => await CheckAndLoginTo(new ASEmpSupportToolLandingPage(context), dfeAdminUser);

    #endregion

    #region CheckAndLoginToASVacancyQa

    public async Task CheckAndLoginToASVacancyQa() => await CheckAndLoginTo(new ASVacancyQaLandingPage(context), context.GetUser<VacancyQaUser>());

    #endregion


    private async Task CheckAndLoginTo(ASLandingCheckBasePage landingPage, DfeAdminUser dfeAdminUser)
    {
        if (await landingPage.IsPageDisplayed()) await landingPage.ClickStartNowButton();

        if (await new CheckDfeSignInPage(context).IsPageDisplayed()) await SubmitValidLoginDetails(dfeAdminUser);
    }

    private async Task SubmitValidLoginDetails(ASLandingCheckBasePage landingPage, DfeAdminUser dfeAdminUser)
    {
        await landingPage.VerifyPage();

        await landingPage.ClickStartNowButton();

        await SubmitValidLoginDetails(dfeAdminUser);
    }

    private async Task SubmitValidLoginDetails(DfeAdminUser dfeAdminUser) => await new DfeSignInPage(context).SubmitValidLoginDetails(dfeAdminUser);

    private AsAdminUser GetAsAdminUser() => context.GetUser<AsAdminUser>();
}