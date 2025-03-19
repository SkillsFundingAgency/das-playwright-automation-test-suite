using Microsoft.Playwright;
using SFA.DAS.DfeAdmin.Service.Project.Tests.Pages;
using SFA.DAS.Login.Service.Project.Helpers;
using System;

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

public class ProviderLandingPage(ScenarioContext context) : BasePage(context)
{
    public static string ProviderLandingPageTitle => "Apprenticeship service for training providers: sign in or register for an account";

    public ILocator ProviderLandingPageIdentifier => page.Locator(".govuk-heading-xl");

    public override async Task VerifyPage()
    {
        await Assertions.Expect(ProviderLandingPageIdentifier).ToContainTextAsync(ProviderLandingPageTitle);
    }

    public async Task ClickStartNow() => await page.GetByRole(AriaRole.Link, new() { Name = "Start now" }).ClickAsync();
}



public abstract class InterimProviderBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly string ukprn = context.Get<ObjectContext>().GetUkprn();
    #endregion
}


public class ProviderHomePage(ScenarioContext context) : InterimProviderBasePage(context)
{
    public static string ProviderHomePageIdentifierCss => "#main-content .govuk-hint";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"UKPRN: {ukprn}");
    }


    //protected static By AddNewApprenticesLink => By.LinkText("Add new apprentices");

    //protected static By AddAnEmployerLink => By.LinkText("Add an employer");

    //protected static By ViewEmployersAndManagePermissionsLink => By.LinkText("View employers and manage permissions");

    //protected static By ProviderManageYourApprenticesLink => By.LinkText("Manage your apprentices");

    //protected static By ProviderRecruitApprenticesLink => By.LinkText("Recruit apprentices");

    //protected static By DeveloperAPIsLink => By.LinkText("Developer APIs");

    //protected static By GetFundingLink => By.LinkText("Get funding for non-levy employers");

    //protected static By ManageYourFundingLink => By.LinkText("Manage your funding reserved for non-levy employers");

    //protected static By ManageEmployerInvitations => By.LinkText("Manage employer invitations");

    //protected static By InviteEmployers => By.LinkText("Send invitation to employer");

    //protected static By RecruitTrainees => By.LinkText("Recruit trainees");

    //protected static By AppsIndicativeEarningsReport => By.LinkText("Apps Indicative earnings report");

    //protected static By YourStandardsAndTrainingVenues => By.LinkText("Your standards and training venues");
    //protected static By YourFeedback => By.LinkText("Your feedback");

    //protected static By ViewEmployerRequestsForTraining => By.LinkText("View employer requests for training");

    public async Task ClickAddAnEmployerLink() => await page.GetByRole(AriaRole.Link, new() { Name = "Add an employer" }).ClickAsync();

    public async Task ClickViewEmployersAndManagePermissionsLink() => await page.GetByRole(AriaRole.Link, new() { Name = "View employers and manage" }).ClickAsync();

}