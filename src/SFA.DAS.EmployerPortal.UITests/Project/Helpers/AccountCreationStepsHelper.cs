using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;
using static SFA.DAS.EmployerPortal.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class AccountCreationStepsHelper(ScenarioContext context)
{
    private readonly RegistrationDataHelper _registrationDataHelper = context.Get<RegistrationDataHelper>();
    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();
    private readonly AccountSignOutHelper _accountSignOutHelper = new(context);

    public async Task<HomePage> CreateUserAccount() => await AddNewAccount(await RegisterUserAccount(), 0);

    public async Task<HomePage> AddNewAccount(HomePage homePage, int index, OrgType orgType = OrgType.Default)
    {
        _objectContext.SetAdditionalOrganisationName(GetOrgName(orgType), index);

        var page = await homePage.GoToYourAccountsPage();

        var page1 = await page.AddNewAccount();

        return await AddNewAccount(page1, index, orgType);
    }

    internal async Task<AddAPAYESchemePage> RegisterUserAccount() =>
        await RegisterUserAccount(new CreateAnAccountToManageApprenticeshipsPage(context), null);

    internal async Task<AddAPAYESchemePage> RegisterUserAccount(CreateAnAccountToManageApprenticeshipsPage indexPage, string email)
    {
        var page = await RegisterStubUserAccount(indexPage, email);

        var page1 = await page.EnterNameAndContinue(_registrationDataHelper);

        var page2 = await page1.ConfirmNameAndContinue();

        var page3 = await page2.ClickContinueButtonToAcknowledge();

        var page4 = await page3.GoToAddPayeLink();

        return await page4.SelectOptionLessThan3Million();
    }


    internal async Task<HomePage> AcceptUserInvite(CreateAnAccountToManageApprenticeshipsPage indexPage, string email)
    {
        var page = await RegisterStubUserAccount(indexPage, email);

        var page1 = await page.EnterNameAndContinue(_registrationDataHelper);

        var page2 = await page1.ConfirmNameAndContinue();

        var page3 = await page2.ClickContinueToInvitationsPage();

        return await page3.ClickAcceptInviteLink();
    }


    internal static async Task<StubAddYourUserDetailsPage> RegisterUserAccount(StubSignInEmployerPage stubSignInPage, string email)
    {
        var page = await stubSignInPage.Register(email);

        return await page.ContinueToStubAddYourUserDetailsPage();
    }

    internal async Task<StubAddYourUserDetailsPage> UserLogsIntoStub() => await RegisterStubUserAccount(new CreateAnAccountToManageApprenticeshipsPage(context), null);


    internal static async Task<SelectYourOrganisationPage> SearchForAnotherOrg(HomePage homepage, OrgType orgType)
    {
        var page = await homepage.GoToYourOrganisationsAndAgreementsPage();

        var page1 = await page.ClickAddNewOrganisationButton();

        return await page1.SearchForAnOrganisation(orgType);
    }


    internal static async Task<CheckYourDetailsPage> AddPayeDetailsForSingleOrgAornRoute(AddAPAYESchemePage addAPAYESchemePage)
    {
        var page = await addAPAYESchemePage.AddAORN();

        return await page.EnterAornAndPayeDetailsForSingleOrgScenarioAndContinue();
    }


    internal static async Task<TheseDetailsAreAlreadyInUsePage> ReEnterAornDetails(AddAPAYESchemePage addAPAYESchemePage)
    {
        var page = await addAPAYESchemePage.AddAORN();

        return await page.ReEnterTheSameAornDetailsAndContinue();
    }


    internal async Task<CreateAnAccountToManageApprenticeshipsPage> SignOut() => await _accountSignOutHelper.SignOut();

    internal static async Task<CheckYourDetailsPage> SearchAndSelectOrg(SearchForYourOrganisationPage searchForYourOrganistionPage, OrgType org)
    {
        var page = await searchForYourOrganistionPage.SearchForAnOrganisation(org);

        return await page.SelectYourOrganisation(org);
    }

    internal static async Task<SearchForYourOrganisationPage> AddADifferentPaye(AddAPAYESchemePage addAPAYESchemePage)
    {
        var page = await addAPAYESchemePage.AddPaye();

        var page1 = await page.ContinueToGGSignIn();

        return await page1.SignInTo(1);
    }

    internal async Task<AddAPAYESchemePage> CreateAnotherUserAccount(CreateAnAccountToManageApprenticeshipsPage indexPage) => await CreateUserAccount(indexPage, _registrationDataHelper.AnotherRandomEmail);

    internal async Task<AddAPAYESchemePage> CreateUserAccount(CreateAnAccountToManageApprenticeshipsPage indexPage, string email) =>
        await RegisterUserAccount(indexPage, email);

    internal static async Task<HomePage> AddAnotherPayeSchemeToTheAccount(HomePage homePage)
    {
        var page = await homePage.GotoPAYESchemesPage();

        var page1 = await page.ClickAddNewSchemeButton();

        var page2 = await page1.ContinueToGGSignIn();

        var page3 = await page2.EnterPayeDetailsAndContinue(1);

        var page4 = await page3.ClickContinueInConfirmPAYESchemePage();

        return await page4.SelectContinueAccountSetupInPAYESchemeAddedPage();
    }

    internal static async Task RemovePayeSchemeFromTheAccount(HomePage homePage)
    {
        var page = await homePage.GotoPAYESchemesPage();

        var page1 = await page.ClickNewlyAddedPayeDetailsLink();

        var page2 = await page1.ClickRemovePAYESchemeButton();

        var page3 = await page2.SelectYesRadioButtonAndContinue();

        await page3.VerifyPayeSchemeRemovedInfoMessage();
    }


    internal static async Task<HomePage> AddNewAccount(AddAPAYESchemePage addAPAYESchemePage, int index, OrgType orgType = OrgType.Default)
    {
        var page = await addAPAYESchemePage.AddPaye();

        var page1 = await page.ContinueToGGSignIn();

        var page2 = await page1.SignInTo(index);

        var page3 = await page2.SearchForAnOrganisation(orgType);

        var page4 = await page3.SelectYourOrganisation(orgType);

        var page5 = await GoToSignAgreementPage(page4);

        var page6 = await page5.SignAgreementFromCreateAccountTasks();

        var page7 = await page6.SelectContinueToCreateYourEmployerAccount();

        var page8 = await page7.GoToTrainingProviderLink();

        var page9 = await page8.AddTrainingProviderLater();

        return await page9.SelectGoToYourEmployerAccountHomepage();
    }

    internal static async Task<SignAgreementPage> GoToSignAgreementPage(CheckYourDetailsPage checkYourDetailsPage)
    {
        var page = await checkYourDetailsPage.ClickYesThisIsMyOrg();

        var page1 = await page.ContinueToConfirmationPage();

        var page2 = await page1.GoToSetYourAccountNameLink();

        var page3 = await page2.SelectoptionToSkipNameChange();

        var page4 = await page3.ContinueToAcknowledge();

        var page5 = await page4.GoToYourEmployerAgreementLink();

        return await page5.ClickContinueToYourAgreementButtonToDoYouAcceptTheEmployerAgreementPage();
    }

    internal static async Task<YouHaveAcceptedTheEmployerAgreementPage> SignAgreementFromHomePage(HomePage homePage)
    {
        var page = await homePage.ClickAcceptYourAgreementLinkInHomePagePanel();

        var page1 = await page.ClickContinueToYourAgreementButtonInAboutYourAgreementPage();

        return await page1.SignAgreementFromHomePage();
    }


    internal void UpdateOrganisationName(OrgType orgType) => _objectContext.SetOrganisationName(GetOrgName(orgType));

    private string GetOrgName(OrgType orgType)
    {
        return orgType switch
        {
            OrgType.Company => _registrationDataHelper.CompanyTypeOrg,
            OrgType.PublicSector => _registrationDataHelper.PublicSectorTypeOrg,
            _ => _registrationDataHelper.CharityTypeOrg1Name,
        };
    }

    private static async Task<StubAddYourUserDetailsPage> RegisterStubUserAccount(CreateAnAccountToManageApprenticeshipsPage indexPage, string email)
    {
        var page = await indexPage.ClickOnCreateAccountLink();

        return await RegisterUserAccount(page, email);
    }
}
