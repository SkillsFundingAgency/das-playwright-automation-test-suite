using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class EmployerHomePageStepsHelper : FrameworkBaseHooks
{
    private readonly EmployerPortalLoginHelper _loginHelper;
    private readonly ObjectContext _objectContext;

    public EmployerHomePageStepsHelper(ScenarioContext context) : base(context)
    {
        _objectContext = this.context.Get<ObjectContext>();
        _loginHelper = new EmployerPortalLoginHelper(this.context);
    }

    public async Task<HomePage> Login(EasAccountUser loginUser) => await _loginHelper.Login(loginUser, true);

    public async Task<HomePage> GotoEmployerHomePage(bool openInNewTab = true)
    {
        await GoToEmployerLoginPage(openInNewTab);

        if (await _loginHelper.IsSignInPageDisplayed())
            return await _loginHelper.ReLogin();

        if (await _loginHelper.IsYourAccountPageDisplayed())
            return await new YourAccountsPage(context).ClickAccountLink(_objectContext.GetOrganisationName());

        return new HomePage(context, !openInNewTab);
    }

    public async Task<AccountUnavailablePage> ValidateUnsuccessfulLogon()
    {
        await GoToEmployerLoginPage(true);

        if (await _loginHelper.IsSignInPageDisplayed()) return await _loginHelper.FailedLogin1();

        return new AccountUnavailablePage(context);
    }

    public async Task GoToEmployerLoginPage(bool openInNewTab)
    {
        if (openInNewTab) await OpenNewTab();

        await NavigateToEmployerApprenticeshipService();

        if (await _loginHelper.IsLandingPageDisplayed()) await new CreateAnAccountToManageApprenticeshipsPage(context).GoToStubSignInPage();
    }

    public async Task NavigateToEmployerApprenticeshipService(bool openInNewTab = false)
    {
        if (openInNewTab) await OpenNewTab();

        await Navigate(UrlConfig.EmployerApprenticeshipService_BaseUrl);
    }
}