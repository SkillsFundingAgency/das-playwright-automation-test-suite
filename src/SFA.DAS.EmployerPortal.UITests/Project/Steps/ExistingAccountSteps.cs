using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using static SFA.DAS.EmployerPortal.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]
public class ExistingAccountSteps
{
    private readonly ScenarioContext _context;
    private readonly EmployerPortalLoginHelper _employerPortalLoginHelper;
    private readonly CreateAccountEmployerPortalLoginHelper _createAccountEmployerPortalLoginHelper;
    private HomePage _homePage;

    public ExistingAccountSteps(ScenarioContext context)
    {
        _context = context;
        _employerPortalLoginHelper = new EmployerPortalLoginHelper(context);
        _createAccountEmployerPortalLoginHelper = new CreateAccountEmployerPortalLoginHelper(_context);
    }

    [Given(@"the Employer logins using existing (Levy|NonLevy) Account")]
    [When(@"the Employer logins using existing (Levy|NonLevy) Account")]
    public async Task GivenTheEmployerLoginsUsingExistingAccount(string employerType = "NonLevy")
    {
        if (employerType == "Levy")
            _homePage = await _employerPortalLoginHelper.Login(_context.GetUser<LevyUser>());
        else
            _homePage = await _createAccountEmployerPortalLoginHelper.Login(_context.GetUser<NonLevyUser>());
    }

    [Given(@"the Employer logins using existing transactor user account")]
    public async Task GivenTheEmployerLoginsUsingExistingTransactorUserAccount() => _homePage = await _createAccountEmployerPortalLoginHelper.Login(_context.GetUser<TransactorUser>(), true);

    [Given(@"the Employer logins using existing view user account")]
    [When(@"the Employer logins using existing view user account")]
    [Given(@"the levy employer login using existing view user account")]
    public async Task GivenTheEmployerLoginsUsingExistingViewUserAccount() => _homePage = await _createAccountEmployerPortalLoginHelper.Login(_context.GetUser<ViewOnlyUser>(), true);

    [Then(@"Employer is able to navigate to all the link under Settings")]
    public async Task ThenEmployerIsAbleToNavigateToAllTheLinkUnderSettings()
    {
        var page = await _homePage.GoToYourAccountsPage();

        _homePage = await page.OpenAccount();

        var page2 = await _homePage.GoToRenameAccountPage();

        _homePage = await page2.GoToHomePage();

        var page3 = await _homePage.GoToNotificationSettingsPage();

        _homePage = await page3.GoBackToHomePage();
    }

    [Then(@"Employer is able to navigate to Help Page")]
    public async Task ThenEmployerIsAbleToNavigateToHelpPage() => await _homePage.GoToHelpPage();

    [Then(@"the employer can navigate to Accessibility statement page")]
    public async Task ThenEmployerIsAbleToNavigateToAccessibilityStatementPage()
    {
        await _homePage.GoToAccessibilityStatementPage();
    }

    [Then(@"the employer can navigate to home page")]
    public async Task ThenTheEmployerCanNavigateToHomePage() => _homePage = await new HomePage(_context, true).GoToHomePage();

    [Then(@"the user can not add an organisation")]
    public async Task ThenTheUserCanNotAddAnOrganisation()
    {
        var page = await _homePage.GoToYourOrganisationsAndAgreementsPage();

        var page1 = await page.ClickAddNewOrganisationButton();

        var page2 = await page1.SearchForAnOrganisation(OrgType.Company2);

        var page3 = await page2.SelectYourOrganisation(OrgType.Company2);

        var page4 = await page3.ClickYesContinueButtonAndRedirectedToAccessDeniedPage();

        _homePage = await GoBackToTheServiceHomePage(page4);
    }

    [Then(@"the user can not remove the organisation")]
    public async Task ThenTheUserCanNotRemoveTheOrganisation()
    {
        var page = await _homePage.GoToYourOrganisationsAndAgreementsPage();

        var page1 = await page.ClickToRemoveAnOrg();

        _homePage = await GoBackToTheServiceHomePage(page1);
    }

    [Then(@"the user can not add Payee Scheme")]
    public async Task ThenTheUserCanNotAddPayeeScheme()
    {
        var page = await _homePage.GotoPAYESchemesPage();

        var page1 = await page.ClickAddNewSchemeButtonAndRedirectedToAccessDeniedPage();

        _homePage = await GoBackToTheServiceHomePage(page1);
    }

    [Then(@"the user can not invite a team members")]
    public async Task ThenTheUserCanNotInviteATeamMembers()
    {
        var page = await _homePage.GotoYourTeamPage();

        var page1 = await page.ClickInviteANewMemberButtonAndRedirectedToAccessDeniedPage();

        _homePage = await GoBackToTheServiceHomePage(page1);
    }

    [Then(@"the user can not accept agreement")]
    public async Task ThenTheUserCanNotAcceptAgreement()
    {
        var page = await _homePage.ClickAcceptYourAgreementLinkInHomePagePanel();

        var page1 = await page.ClickContinueToYourAgreementButtonToDoYouAcceptTheEmployerAgreementPage();

        var page2 = await page1.ClickYesAndContinueDoYouAcceptTheEmployerAgreementOnBehalfOfPage();

        _homePage = await GoBackToTheServiceHomePage(page2);
    }

    [Then(@"the user can not add an apprentices")]
    public async Task ThenTheUserCanNotAddAnApprentices()
    {
        await _context.Get<Driver>().Page.GetByRole(AriaRole.Link, new() { Name = "Apprentices", Exact = true }).ClickAsync();

        var page = await VerifyPageHelper.VerifyPageAsync(() => new AccessDeniedPage(_context));

        _homePage = await GoBackToTheServiceHomePage(page);
    }

    private async Task<HomePage> GoBackToTheServiceHomePage(AccessDeniedPage accessDeniedPage) => await accessDeniedPage.GoBackToTheServiceHomePage(_employerPortalLoginHelper.GetLoginCredentials().OrganisationName);
}