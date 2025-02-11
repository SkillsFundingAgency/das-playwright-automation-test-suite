using SFA.DAS.Framework;
using System;
using static SFA.DAS.Registration.UITests.Project.Pages.YouveLoggedOutPage;

namespace SFA.DAS.Registration.UITests.Project.Pages.InterimPages;

public abstract class NavigateBase : BasePage
{
    protected NavigateBase(ScenarioContext context, string url) : base(context)
    {
        // if (!(string.IsNullOrEmpty(url))) tabHelper.GoToUrl(url);
    }
}

public abstract class Navigate : NavigateBase
{
    //protected static By GlobalNavLink => By.CssSelector("#global-nav-links li a, #navigation li a, .das-navigation__link");

    //private static By MoreLink => By.LinkText("More");

    //protected abstract string Linktext { get; }

    protected Navigate(ScenarioContext context, bool navigate) : this(context, navigate, string.Empty) { }

    protected Navigate(ScenarioContext context, bool navigate, string url) : base(context, url) => NavigateTo(navigate);

    protected Navigate(ScenarioContext context, Action navigate, string url) : base(context, url) => NavigateTo(navigate);

    //protected void RetryClickOnException(By parentElement, Func<IWebElement> childElement)
    //{
    //    formCompletionHelper.RetryClickOnException(() =>
    //    {
    //        if (pageInteractionHelper.IsElementDisplayedAfterPageLoad(parentElement))
    //            formCompletionHelper.ClickElement(parentElement);

    //        return childElement();
    //    });
    //}

    private static void NavigateTo(Action navigate) => navigate.Invoke();

    private void NavigateTo(bool navigate)
    {
        // if (navigate) RetryClickOnException(MoreLink, () => pageInteractionHelper.GetLink(GlobalNavLink, Linktext));
    }
}

public abstract class InterimEmployerBasePage : Navigate
{
    //#region Locators
    //private static By SettingsLink => By.LinkText("Settings");
    //private static By YourAccountsLink => By.LinkText("Your accounts");
    //private static By HelpLink => By.LinkText("Help");
    //private static By RenameAccountLink => By.LinkText("Rename account");
    //private static By NotificationSettingsLink => By.PartialLinkText("Notification");
    //#endregion

    protected InterimEmployerBasePage(ScenarioContext context, bool navigate) : this(context, navigate, false) { }

    protected InterimEmployerBasePage(ScenarioContext context, bool navigate, bool gotourl) : base(context, navigate, GoToUrl(gotourl))
    {
    }

    protected InterimEmployerBasePage(ScenarioContext context, Action navigate, bool gotourl) : base(context, navigate, GoToUrl(gotourl))
    {

    }

    private static string GoToUrl(bool gotourl) => gotourl ? UrlConfig.EmployerApprenticeshipService_BaseUrl : string.Empty;

    public async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<YourOrganisationsAndAgreementsPage> GoToYourOrganisationsAndAgreementsPage()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Your organisations and" }).ClickAsync();

        return await VerifyPageAsync(() => new YourOrganisationsAndAgreementsPage(context));
    }

    public async Task<YourAccountsPage> GoToYourAccountsPage()
    {
        await NavigateToSettings("Your accounts");

        return await VerifyPageAsync(() => new YourAccountsPage(context));
    }

    public async Task GoToHelpPage()
    {
        var page1 = await page.RunAndWaitForPopupAsync(async () =>
        {
            await page.GetByRole(AriaRole.Menuitem, new() { Name = "Help" }).ClickAsync();
        });

        await Assertions.Expect(page1.GetByRole(AriaRole.Main)).ToContainTextAsync("Useful Links");
    }

    public async Task<RenameAccountPage> GoToRenameAccountPage()
    {
        await NavigateToSettings("Rename account");

        return await VerifyPageAsync(() => new RenameAccountPage(context));
    }

    public async Task<NotificationSettingsPage> GoToNotificationSettingsPage()
    {
        await NavigateToSettings("Notifications settings");

        return await VerifyPageAsync(() => new NotificationSettingsPage(context));
    }

    public async Task<YouveLoggedOutPage> SignOut()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new YouveLoggedOutPage(context));
    }

    public async Task<YourTeamPage> GotoYourTeamPage()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Your team" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }

    public async Task<PAYESchemesPage> GotoPAYESchemesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "More" }).ClickAsync();

        await page.GetByRole(AriaRole.Menuitem, new() { Name = "PAYE schemes" }).ClickAsync();

        return await VerifyPageAsync(() => new PAYESchemesPage(context));
    }

    private async Task NavigateToSettings(string name)
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Settings" }).ClickAsync();

        await page.GetByRole(AriaRole.Menuitem, new() { Name = name }).ClickAsync();
    }
}

public class YourTeamPage(ScenarioContext context, bool navigate = false) : InterimEmployerBasePage(context, navigate)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your team");
    }

    public async Task<AccessDeniedPage> ClickInviteANewMemberButtonAndRedirectedToAccessDeniedPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Invite a new member" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }
}

public class PAYESchemesPage(ScenarioContext context, bool navigate = false) : InterimEmployerBasePage(context, navigate)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("PAYE schemes");
    }

    #region Locators
    //private static By AddNewSchemeButton => By.Id("addNewPaye");
    //private By PayeDetailsLink => By.XPath($"//td[contains(text(),'{SecondPaye}')]/following-sibling::td//a");
    //private static By PAYERemovedHeaderInfo => By.CssSelector("h3.das-notification__heading");
    //private string SecondPaye => objectContext.GetGatewayPaye(1);

    #endregion

    //public UsingYourGovtGatewayDetailsPage ClickAddNewSchemeButton()
    //{
    //    await page.GetByRole(AriaRole.Link, new() { Name = "Add new scheme" }).ClickAsync();

    //    return new UsingYourGovtGatewayDetailsPage(context);
    //}

    public async Task<AccessDeniedPage> ClickAddNewSchemeButtonAndRedirectedToAccessDeniedPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add new scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    //public PAYESchemeDetailsPage ClickNewlyAddedPayeDetailsLink()
    //{
    //    formCompletionHelper.Click(PayeDetailsLink);
    //    return new PAYESchemeDetailsPage(context);
    //}

    //public PAYESchemesPage VerifyPayeSchemeRemovedInfoMessage()
    //{
    //    VerifyElement(PAYERemovedHeaderInfo, $"You've removed {SecondPaye}");
    //    return this;
    //}
}