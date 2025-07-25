using System;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;

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

    protected abstract string Linktext { get; }

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

    private async void NavigateTo(bool navigate)
    {
        if (navigate)
        {
            objectContext.SetDebugInformation($"Clicked menu item - {Linktext}");

            await NavigateToMenuItem(Linktext);
        }
    }

    protected async Task NavigateToMenuItem(string name)
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = name }).ClickAsync();
    }
}


public class InterimCreateAnAdvertHomePage(ScenarioContext context) : InterimYourApprenticeshipAdvertsHomePage(context, true)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create an advert");
    }
}

public class InterimYourApprenticeshipAdvertsHomePage(ScenarioContext context, bool navigate, bool gotourl) : InterimEmployerBasePage(context, navigate, gotourl)
{
    protected override string Linktext => "Adverts";

    public InterimYourApprenticeshipAdvertsHomePage(ScenarioContext context, bool navigate) : this(context, navigate, false) { }

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your apprenticeship adverts");
    }
}

public class InterimApprenticesHomePage(ScenarioContext context, bool gotourl) : InterimEmployerBasePage(context, true, gotourl)
{
    protected override string Linktext => "Apprentices";

    public override async Task VerifyPage()
    {
        await Task.Delay(1000);
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Apprentices");
    }
}


public class InterimFinanceHomePage(ScenarioContext context, bool navigate, bool gotourl) : InterimEmployerBasePage(context, navigate, gotourl)
{
    protected override string Linktext => "Finance";

    public InterimFinanceHomePage(ScenarioContext context, bool navigate) : this(context, navigate, false) { }

    public override async Task VerifyPage()
    {
        await Task.Delay(1000); 
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Finance");
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
        await NavigateToMenuItem("Home");

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<YourOrganisationsAndAgreementsPage> GoToYourOrganisationsAndAgreementsPage()
    {
        await NavigateToMenuItem("Your organisations and");

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

        await page1.CloseAsync();
    }
    public async Task GoToAccessibilityStatementPage()
    {
        await page.Locator("footer >> text=Accessibility statement").ClickAsync();

        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Assertions.Expect(page.GetByRole(AriaRole.Main))
            .ToContainTextAsync("Accessibility statement");
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
        await NavigateToMenuItem("Your team");

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }

    public async Task<PAYESchemesPage> GotoPAYESchemesPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "More" }).ClickAsync();

        await NavigateToMenuItem("PAYE schemes");

        return await VerifyPageAsync(() => new PAYESchemesPage(context));
    }

    private async Task NavigateToSettings(string name)
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Settings" }).ClickAsync();

        await page.GetByRole(AriaRole.Menuitem, new() { Name = name }).ClickAsync();
    }
}

public abstract class InterimYourTeamPage(ScenarioContext context, bool navigate) : InterimEmployerBasePage(context, navigate)
{
    protected override string Linktext => "Your team";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Your team");
    }
}

public class YourTeamPage(ScenarioContext context, bool navigate = false) : InterimYourTeamPage(context, navigate)
{


    public async Task<AccessDeniedPage> ClickInviteANewMemberButtonAndRedirectedToAccessDeniedPage()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Invite a new member" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    public async Task<CreateInvitationPage> ClickInviteANewMemberButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Invite a new member" }).ClickAsync();

        return await VerifyPageAsync(() => new CreateInvitationPage(context));
    }

    public async Task<ViewTeamMemberPage> ClickViewMemberLink(string email)
    {
        await page.GetByRole(AriaRole.Row, new() { Name = email }).GetByLabel("View details").ClickAsync();

        return await VerifyPageAsync(() => new ViewTeamMemberPage(context));
    }

    public async Task VerifyInvitationResentHeaderInfoMessage()
    {
        await VerifyInvitationActionHeader("Invitation resent");
    }

    public async Task VerifyInvitationCancelledHeaderInfoMessage()
    {
        await VerifyInvitationActionHeader("Invitation cancelled");
    }

    public async Task VerifyTeamMemberRemovedHeaderInfoMessage()
    {
        await VerifyInvitationActionHeader("Team member removed");

    }
    private async Task VerifyInvitationActionHeader(string message) => await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync(message);

}

public class ViewTeamMemberPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync(employerPortalDataHelper.FullName);
    }

    public async Task<YourTeamPage> ClickResendInvitationButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Resend invitation" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }

    public async Task<CancelInvitationPage> ClickCancelInvitationLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Cancel invitation" }).ClickAsync();

        return await VerifyPageAsync(() => new CancelInvitationPage(context));
    }

    public async Task<RemoveTeamMemberPage> ClickRemoveTeamMemberButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Remove team member" }).ClickAsync();

        return await VerifyPageAsync(() => new RemoveTeamMemberPage(context));
    }
}

public class RemoveTeamMemberPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Remove team member");
    }

    public async Task<YourTeamPage> ClickYesRemoveNowButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, remove now" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }
}

public class CancelInvitationPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Cancel invitation");
    }

    public async Task<YourTeamPage> ClickYesCancelInvitationButton()
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Yes, cancel invitation" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }

    public async Task<YourTeamPage> ClickNoDontCancelInvitationLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "No, don't cancel invitation" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }
}


public class CreateInvitationPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Create invitation");
    }

    public async Task EnterEmailAndFullName(string email)
    {
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync(email);

        await page.GetByRole(AriaRole.Textbox, new() { Name = "Full name" }).FillAsync(employerPortalDataHelper.FullName);
    }

    public async Task<InvitationSentPage> SelectViewerAccessRadioButtonAndSendInvitation()
    {
        await page.Locator("#radio1").Nth(1).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Send invitation" }).ClickAsync();

        return await VerifyPageAsync(() => new InvitationSentPage(context));
    }
}

public class InvitationSentPage(ScenarioContext context) : InterimHomeBasePage(context, false)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Invitation sent");
    }

    public async Task<YourTeamPage> ViewAllTeamMembers()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "View all team members" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new YourTeamPage(context));
    }
}

public class InterimPAYESchemesPage(ScenarioContext context, bool navigate) : InterimEmployerBasePage(context, navigate)
{
    protected override string Linktext => "PAYE schemes";

    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("PAYE schemes");
    }
}

public class PAYESchemesPage(ScenarioContext context, bool navigate = false) : InterimPAYESchemesPage(context, navigate)
{
    private string SecondPaye => objectContext.GetGatewayPaye(1);

    public async Task<UsingYourGovtGatewayDetailsPage> ClickAddNewSchemeButton()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add new scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new UsingYourGovtGatewayDetailsPage(context));
    }

    public async Task<AccessDeniedPage> ClickAddNewSchemeButtonAndRedirectedToAccessDeniedPage()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Add new scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new AccessDeniedPage(context));
    }

    public async Task<PAYESchemeDetailsPage> ClickNewlyAddedPayeDetailsLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = $"Details for PAYE scheme {SecondPaye}" }).ClickAsync();

        return await VerifyPageAsync(() => new PAYESchemeDetailsPage(context));
    }

    public async Task VerifyPayeSchemeRemovedInfoMessage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync($"You've removed {SecondPaye}");
    }
}

public class PAYESchemeDetailsPage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("PAYE scheme");
    }

    public async Task<RemoveThisSchemePage> ClickRemovePAYESchemeButton()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Remove PAYE scheme" }).ClickAsync();

        return await VerifyPageAsync(() => new RemoveThisSchemePage(context));
    }
}

public class RemoveThisSchemePage(ScenarioContext context) : EmployerPortalBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("#main-content")).ToContainTextAsync("Remove this scheme?");
    }

    public async Task<PAYESchemesPage> SelectYesRadioButtonAndContinue()
    {
        await page.GetByRole(AriaRole.Radio, new() { Name = "Yes, remove scheme" }).CheckAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        return await VerifyPageAsync(() => new PAYESchemesPage(context));
    }
}