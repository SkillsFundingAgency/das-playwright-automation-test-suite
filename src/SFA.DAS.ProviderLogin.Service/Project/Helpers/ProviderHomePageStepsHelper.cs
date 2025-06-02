using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;

namespace SFA.DAS.ProviderLogin.Service.Project.Helpers;

public class ProviderHomePageStepsHelper(ScenarioContext context)
{
    private readonly ObjectContext objectContext = context.Get<ObjectContext>();

    private static string Provider_BaseUrl => UrlConfig.Provider_BaseUrl;

    public async Task<ProviderHomePage> GoToProviderHomePage(bool newTab) => await GoToProviderHomePage(context.GetProviderConfig<ProviderConfig>(), newTab);

    public async Task<ProviderHomePage> GoToProviderHomePage(ProviderLoginUser login, bool newTab)
    {
        var driver = context.Get<Driver>();

        if (newTab) await OpenInNewTab();

        objectContext.SetDebugInformation($"Navigated to {Provider_BaseUrl}");

        await driver.Page.GotoAsync(Provider_BaseUrl);

        objectContext.SetUkprn(login.Ukprn);

        return await GoToProviderHomePage(login);
    }

    public async Task<ProviderHomePage> GoToProviderHomePage(ProviderConfig login, bool newTab)
    {
        var loginUser = new ProviderLoginUser { Username = login.Username, Password = login.Password, Ukprn = login.Ukprn };

        return await GoToProviderHomePage(loginUser, newTab);
    }

    private async Task<ProviderHomePage> GoToProviderHomePage(ProviderLoginUser login)
    {
        var loginHelper = new ProviderPortalLoginHelper(context, login);

        await loginHelper.ClickStartNow();

        // provider relogin check
        if (objectContext.GetDebugInformations(Provider_BaseUrl).Count > 1)
        {
            if (await new CheckDfeSignInOrProviderHomePage(context, login.Ukprn).IsProviderHomePageDisplayed()) return new ProviderHomePage(context);
        }

        return await loginHelper.GoToProviderHomePage();
    }

    private async Task OpenInNewTab()
    {
        var driver = context.Get<Driver>();

        var page = await driver.BrowserContext.NewPageAsync();

        driver.Page = page;
    }
}

public class CheckDfeSignInOrProviderHomePage(ScenarioContext context, string ukprn) : CheckMultipleHomePage(context)
{
    public override string[] PageIdentifierCss => [DfeSignInPage.DfePageIdentifierCss, ProviderHomePage.ProviderHomePageIdentifierCss];

    public override string[] PageTitles => [DfeSignInPage.DfePageTitle, ukprn];

    public async Task<bool> IsProviderHomePageDisplayed() => await ActualDisplayedPage(ukprn);
}
