using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class EmployerHomePageStepsHelper
{
    private readonly ScenarioContext _context;
    private readonly EmployerPortalLoginHelper _loginHelper;
    private readonly ObjectContext _objectContext;

    public EmployerHomePageStepsHelper(ScenarioContext context)
    {
        _context = context;
        _objectContext = _context.Get<ObjectContext>();
        _loginHelper = new EmployerPortalLoginHelper(_context);
    }

    public async Task<HomePage> Login(EasAccountUser loginUser) => await _loginHelper.Login(loginUser, true);

    public async Task<HomePage> GotoEmployerHomePage(bool openInNewTab = true)
    {
        await GoToEmployerLoginPage(openInNewTab);

        if (await _loginHelper.IsSignInPageDisplayed())
            return await _loginHelper.ReLogin();

        if (await _loginHelper.IsYourAccountPageDisplayed())
            return await new YourAccountsPage(_context).ClickAccountLink(_objectContext.GetOrganisationName());

        return new HomePage(_context, !openInNewTab);
    }

    public async Task<AccountUnavailablePage> ValidateUnsuccessfulLogon()
    {
        await GoToEmployerLoginPage(true);

        if (await _loginHelper.IsSignInPageDisplayed()) return await _loginHelper.FailedLogin1();

        return new AccountUnavailablePage(_context);
    }

    private async Task OpenInNewTab()
    {
        var driver = _context.Get<Driver>();

        var page = await driver.BrowserContext.NewPageAsync();

        driver.Page = page;

        await NavigateToEmployerApprenticeshipService();
    }

    private async Task GoToEmployerLoginPage(bool openInNewTab)
    {
        if (openInNewTab) await OpenInNewTab();

        if (await _loginHelper.IsLandingPageDisplayed()) await new CreateAnAccountToManageApprenticeshipsPage(_context).GoToStubSignInPage();
    }

    public async Task NavigateToEmployerApprenticeshipService()
    {
        var driver = _context.Get<Driver>();

        var url = UrlConfig.EmployerApprenticeshipService_BaseUrl;

        _objectContext.SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }
}