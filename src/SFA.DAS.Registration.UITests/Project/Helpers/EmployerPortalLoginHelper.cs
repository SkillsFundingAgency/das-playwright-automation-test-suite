using SFA.DAS.Registration.UITests.Project.Pages;
using SFA.DAS.Registration.UITests.Project.Pages.StubPages;

namespace SFA.DAS.Registration.UITests.Project.Helpers;

public class CreateAccountEmployerPortalLoginHelper(ScenarioContext context) : EmployerPortalLoginHelper(context)
{
    private readonly ScenarioContext _context = context;

    protected override async Task<HomePage> Login(EasAccountUser loginUser)
    {
        var page = await new CreateAnAccountToManageApprenticeshipsPage(_context).ClickOnCreateAccountLink();

        var page1 = await page.Login(loginUser);

        return await page1.ContinueToHomePage();
    }
}

public class EmployerPortalLoginHelper(ScenarioContext context) : IReLoginHelper
{
    protected readonly ScenarioContext context = context;

    protected readonly ObjectContext objectContext = context.Get<ObjectContext>();

    protected readonly LoginCredentialsHelper loginCredentialsHelper = context.Get<LoginCredentialsHelper>();

    public async Task<bool> IsSignInPageDisplayed() => await new CheckStubSignInPage(context).IsPageDisplayed();

    public async Task<bool> IsLandingPageDisplayed() => await new CheckIndexPage(context).IsPageDisplayed();

    public async Task<bool> IsYourAccountPageDisplayed() => await new CheckYourAccountPage(context).IsPageDisplayed();

    public async Task<HomePage> ReLogin()
    {
        var page = await new StubSignInEmployerPage(context).Login(GetLoginCredentials());

        return await page.ContinueToHomePage();
    }

    public async Task<AccountUnavailablePage> FailedLogin1()
    {
        var page = await new StubSignInEmployerPage(context).Login(GetLoginCredentials());
        
        return await page.GoToAccountUnavailablePage();
    }

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
        SetCredentials(loginUser, isLevy);

        var homePage = await Login(loginUser);

        return homePage;
    }

    protected void SetCredentials(EasAccountUser loginUser, bool isLevy)
    {
        SetLoginCredentials(loginUser, isLevy);

        objectContext.SetOrUpdateUserCreds(loginUser.Username, loginUser.IdOrUserRef, loginUser.AccountDetails);
    }

    public async Task<HomePage> Login(LevyUser nonLevyUser) => await Login(nonLevyUser, true);

    public async Task<HomePage> Login(NonLevyUser nonLevyUser) => await Login(nonLevyUser, false);

    public LoggedInAccountUser GetLoginCredentials() => loginCredentialsHelper.GetLoginCredentials();
}
