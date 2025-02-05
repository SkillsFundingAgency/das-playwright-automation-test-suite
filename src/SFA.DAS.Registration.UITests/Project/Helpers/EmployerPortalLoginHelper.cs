

using SFA.DAS.Registration.UITests.Project.Pages;

namespace SFA.DAS.Registration.UITests.Project.Helpers;

public class EmployerPortalLoginHelper(ScenarioContext context) : IReLoginHelper
{
    protected readonly ScenarioContext context = context;
    private readonly RegistrationSqlDataHelper _registrationSqlDataHelper = context.Get<RegistrationSqlDataHelper>();
    protected readonly ObjectContext objectContext = context.Get<ObjectContext>();
    protected readonly LoginCredentialsHelper loginCredentialsHelper = context.Get<LoginCredentialsHelper>();

    public async Task<bool> IsSignInPageDisplayed() => await new CheckStubSignInPage(context).IsPageDisplayed();

    public async Task<bool> IsLandingPageDisplayed() => await new CheckIndexPage(context).IsPageDisplayed();

    public async Task<bool> IsYourAccountPageDisplayed() => await new CheckYourAccountPage(context).IsPageDisplayed();

    public async Task<HomePage> ReLogin() => await new StubSignInEmployerPage(context).Login(GetLoginCredentials()).ContinueToHomePage();

    public async Task<AccountUnavailablePage> FailedLogin1() => await new StubSignInEmployerPage(context).Login(GetLoginCredentials()).GoToAccountUnavailablePage();

    protected virtual async Task<HomePage> Login(EasAccountUser loginUser)
    {
        var page = await new CreateAnAccountToManageApprenticeshipsPage(context).GoToStubSignInPage();

        var page1 = await page.Login(loginUser);

        return await page1.ContinueToHomePage();
    }

    protected virtual void SetLoginCredentials(EasAccountUser loginUser, bool isLevy)
        => loginCredentialsHelper.SetLoginCredentials(loginUser.Username, loginUser.IdOrUserRef, loginUser.OrganisationName, isLevy);

    public async Task<HomePage> Login(EasAccountUser loginUser, bool isLevy)
    {
        await SetCredentials(loginUser, isLevy);

        var homePage = await Login(loginUser);

        return homePage;
    }

    protected async Task SetCredentials(EasAccountUser loginUser, bool isLevy)
    {
        SetLoginCredentials(loginUser, isLevy);

        var data = await _registrationSqlDataHelper.CollectAccountDetails(loginUser.Username);

        loginUser.UserCreds = objectContext.SetOrUpdateUserCreds(loginUser.Username, loginUser.IdOrUserRef, data);
    }

    public async Task<HomePage> Login(LevyUser nonLevyUser) => await Login(nonLevyUser, true);

    public async Task<HomePage> Login(NonLevyUser nonLevyUser) => await Login(nonLevyUser, false);

    public LoggedInAccountUser GetLoginCredentials() => loginCredentialsHelper.GetLoginCredentials();
}
