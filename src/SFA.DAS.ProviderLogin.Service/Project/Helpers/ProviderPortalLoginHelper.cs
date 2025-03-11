using Microsoft.Playwright;
using SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;
using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ProviderLogin.Service.Project.Helpers;

internal class ProviderPortalLoginHelper : IReLoginHelper
{
    private readonly ScenarioContext _context;

    private readonly ProviderLoginUser providerLoginUser;

    internal ProviderPortalLoginHelper(ScenarioContext context, ProviderLoginUser user) { _context = context; providerLoginUser = user; }

    public async Task<bool> IsLandingPageDisplayed() => await new CheckProviderLandingPage(_context).IsPageDisplayed();

    public async Task<bool> IsSignInPageDisplayed() => await new CheckDfeSignInPage(_context).IsPageDisplayed();

    internal async Task ClickStartNow() { if (await IsLandingPageDisplayed()) await new ProviderLandingPage(_context).ClickStartNow(); }

    internal async Task SubmitValidLoginDetails() { if (await IsSignInPageDisplayed()) await new ProviderDfeSignInPage(_context).SubmitValidLoginDetails(providerLoginUser); }

    internal async Task<ProviderHomePage> GoToProviderHomePage()
    {
        await SubmitValidLoginDetails();

        if (await new CheckSelectYourOrgOrProviderHomePage(_context, providerLoginUser.Ukprn).IsSelectYourOrganisationDisplayed())
        {
            await new SelectYourOrganisationPage(_context).SelectOrganisation(providerLoginUser.Ukprn);
        }

        return new ProviderHomePage(_context);
    }
}

public class CheckProviderLandingPage(ScenarioContext context) : CheckPage(context)
{
    protected override string PageTitle => ProviderLandingPage.ProviderLandingPageTitle;

    protected override ILocator PageLocator => new ProviderLandingPage(context).ProviderLandingPageIdentifier;
}

public class ProviderDfeSignInPage(ScenarioContext context) : DfeSignInPage(context)
{
    public async Task SubmitValidLoginDetails(ProviderLoginUser login)
    {
        await SubmitValidLoginDetails(login.Username, login.Password);
    }
}

public class CheckSelectYourOrgOrProviderHomePage(ScenarioContext context, string ukprn) : CheckMultipleHomePage(context)
{
    public override string[] PageIdentifierCss => [SelectYourOrganisationPage.SyoPageIdentifierCss, ProviderHomePage.ProviderHomePageIdentifierCss];

    public override string[] PageTitles => [SelectYourOrganisationPage.SyoPageTitle, ukprn];

    public async Task<bool> IsSelectYourOrganisationDisplayed() => await ActualDisplayedPage(SelectYourOrganisationPage.SyoPageTitle);
}

public class SelectYourOrganisationPage(ScenarioContext context) : BasePage(context)
{
    public override async Task VerifyPage() => await Assertions.Expect(page.Locator("#content")).ToContainTextAsync("Select your organisation");

    public static string SyoPageTitle => "Select your organisation";

    public static string SyoPageIdentifierCss => ".govuk-heading-xl";

    public async Task<ProviderHomePage> SelectOrganisation(string ukprn)
    {
        await page.GetByText($"UKPRN: {ukprn}").ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return new ProviderHomePage(context);
    }
}
