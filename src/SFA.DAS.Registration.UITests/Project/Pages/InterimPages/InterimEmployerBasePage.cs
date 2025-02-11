using SFA.DAS.Framework;
using System;

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

    public HomePage GoToHomePage() => new(context, true);

    //public YourOrganisationsAndAgreementsPage GoToYourOrganisationsAndAgreementsPage() => new(context, true);

    //public YourAccountsPage GoToYourAccountsPage()
    //{
    //    NavigateToSettings(YourAccountsLink);
    //    return new YourAccountsPage(context);
    //}

    //public EmployerHelpPage GoToHelpPage()
    //{
    //    tabHelper.OpenInNewTab(() => formCompletionHelper.ClickElement(HelpLink));
    //    return new EmployerHelpPage(context);
    //}

    //public RenameAccountPage GoToRenameAccountPage()
    //{
    //    NavigateToSettings(RenameAccountLink);
    //    return new RenameAccountPage(context);
    //}

    //public NotificationSettingsPage GoToNotificationSettingsPage()
    //{
    //    NavigateToSettings(NotificationSettingsLink);
    //    return new NotificationSettingsPage(context);
    //}

    public async Task<YouveLoggedOutPage> SignOut()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new YouveLoggedOutPage(context));
    }

    //public YourTeamPage GotoYourTeamPage() => new(context, true);

    //public PAYESchemesPage GotoPAYESchemesPage() => new(context, true);

    //private void NavigateToSettings(By by) => RetryClickOnException(SettingsLink, () => pageInteractionHelper.FindElement(by));
}