using NUnit.Framework;
using SFA.DAS.Framework;
using SFA.DAS.Registration.UITests.Project;
using SFA.DAS.Registration.UITests.Project.Helpers;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Registration.UITests.Project.Pages;
using SFA.DAS.Registration.UITests.Project.Pages.StubPages;
using SFA.DAS.UI.FrameworkHelpers;
using static SFA.DAS.Registration.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.Registration.UITests.Project.Steps;

[Binding]
public class CreateAccountSteps
{
    private readonly ScenarioContext _context;
    private readonly ObjectContext _objectContext;
    private readonly RegistrationDataHelper _registrationDataHelper;
    private readonly RegistrationSqlDataHelper _registrationSqlDataHelper;
    private readonly TprSqlDataHelper _tprSqlDataHelper;
    private readonly AccountCreationStepsHelper _accountCreationStepsHelper;

    private HomePage _homePage;
    private AddAPAYESchemePage _addAPAYESchemePage;
    private GgSignInPage _gGSignInPage;
    private SearchForYourOrganisationPage _searchForYourOrganisationPage;
    private SelectYourOrganisationPage _selectYourOrganisationPage;
    private CheckYourDetailsPage _checkYourDetailsPage;
    //private TheseDetailsAreAlreadyInUsePage _theseDetailsAreAlreadyInUsePage;
    private EnterYourPAYESchemeDetailsPage _enterYourPAYESchemeDetailsPage;
    private UsingYourGovtGatewayDetailsPage _usingYourGovtGatewayDetailsPage;
    private CreateAnAccountToManageApprenticeshipsPage _indexPage;
    private AddPayeSchemeUsingGGDetailsPage _addPayeSchemeUsingGGDetailsPage;
    private SignAgreementPage _doYouAcceptTheEmployerAgreementOnBehalfOfPage;


    public CreateAccountSteps(ScenarioContext context)
    {
        _context = context;
        _objectContext = _context.Get<ObjectContext>();
        _registrationDataHelper = context.Get<RegistrationDataHelper>();
        _registrationSqlDataHelper = context.Get<RegistrationSqlDataHelper>();
        _tprSqlDataHelper = context.Get<TprSqlDataHelper>();
        _accountCreationStepsHelper = new AccountCreationStepsHelper(context);
    }

    [Given(@"a User Account is created")]
    [When(@"a User Account is created")]
    public async Task AUserAccountIsCreated() => _addAPAYESchemePage = await _accountCreationStepsHelper.RegisterUserAccount();

    [When("the User initiates Account creation")]
    public async Task UserInitiatesAccountCreation() => await _accountCreationStepsHelper.RegisterUserAccount();

    [Given(@"the User adds PAYE details")]
    [When(@"the User adds PAYE details")]
    [When(@"the User adds valid PAYE details")]
    public async Task<SearchForYourOrganisationPage> AddPayeDetails() => await AddPayeDetails(0);

    [Given(@"the User adds PAYE details attached to a (SingleOrg|MultiOrg) through AORN route")]
    [When(@"the User adds PAYE details attached to a (SingleOrg|MultiOrg) through AORN route")]
    public async Task WhenTheUserAddsPAYEDetailsAttachedToASingleOrgThroughAORNRoute(string org)
    {
        if (org.Equals("SingleOrg"))
        {
            _tprSqlDataHelper.CreateSingleOrgAornData();

            _checkYourDetailsPage = await AccountCreationStepsHelper.AddPayeDetailsForSingleOrgAornRoute(_addAPAYESchemePage);
        }
        else
        {
            _tprSqlDataHelper.CreateMultiOrgAORNData();

            var page = await _addAPAYESchemePage.AddAORN();

            var page1 = await page.EnterAornAndPayeDetailsForMultiOrgScenarioAndContinue();

            _checkYourDetailsPage = await page1.SelectFirstOrganisationAndContinue();
        }

        _doYouAcceptTheEmployerAgreementOnBehalfOfPage = await AccountCreationStepsHelper.GoToSignAgreementPage(_checkYourDetailsPage);
    }

    [When(@"the User adds Invalid PAYE details")]
    public async Task WhenTheUserAddsInvalidPAYEDetails()
    {
        var page = await _addAPAYESchemePage.AddPaye();

        _gGSignInPage = await page.ContinueToGGSignIn();

        await _gGSignInPage.SignInWithInvalidDetails();
    }

    //[Then(@"the '(.*)' error message is shown")]
    //public async Task ThenTheErrorMessageIsShown(string error) => Assert.AreEqual(error, _gGSignInPage.GetErrorMessage());

    [When(@"the User adds valid PAYE details on Gateway Sign In Page")]
    public async Task WhenTheUserAddsValidPAYEDetailsOnGatewaySignInPage() => _searchForYourOrganisationPage = await _gGSignInPage.SignInTo(0);

    [When(@"adds Organisation details")]
    public async Task AddOrganisationDetails() => await AddOrganisationTypeDetails(OrgType.Default);

    [When(@"adds (Company|PublicSector|Charity) Type Organisation details")]
    public async Task AddOrganisationTypeDetails(OrgType orgType)
    {
        var page = await _searchForYourOrganisationPage.SearchForAnOrganisation(orgType);

        var page1 = await page.SelectYourOrganisation(orgType);

        _doYouAcceptTheEmployerAgreementOnBehalfOfPage = await AccountCreationStepsHelper.GoToSignAgreementPage(page1);
    }

    [When(@"enters an Invalid Company number for Org search")]
    public async Task<SelectYourOrganisationPage> WhenAnEmployerEntersAnInvalidCompanyNumberForOrgSearchInOrganisationSearchPage()
    {
        _selectYourOrganisationPage = await _searchForYourOrganisationPage.SearchForAnOrganisation(_registrationDataHelper.InvalidCompanyNumber);

        return await BasePage.VerifyPageAsync(() => new SelectYourOrganisationPage(_context));
    }

    //[Then(@"the '(.*)' message is shown")]
    //public async Task ThenTheMessageIsShown(string resultMessage) => Assert.AreEqual(resultMessage, await _selectYourOrganisationPage.GetSearchResultsText());

    [Then(@"the Employer is able to Sign the Agreement and view the Home page")]
    [When(@"the Employer is able to Sign the Agreement")]
    [Then(@"the Employer is able to Sign the Agreement")]
    [When(@"the Employer Signs the Agreement")]
    [Then(@"the Employer Signs the Agreement")]
    public async Task SignTheAgreementAndAddProviderLater()
    {
        var page = await _doYouAcceptTheEmployerAgreementOnBehalfOfPage
            .SignAgreement();

        var page1 = await page.SelectContinueToCreateYourEmployerAccount();

        var page2 = await page1.GoToTrainingProviderLink();

        var page3 = await page2.AddTrainingProviderLater();

        _homePage = await page3.SelectGoToYourEmployerAccountHomepage();
    }

    [When(@"the Employer does not sign the Agreement")]
    [Then(@"the Employer does not sign the Agreement")]
    public async Task DoNotSignTheAgreementAndAddProviderLater()
    {
        var page = await _doYouAcceptTheEmployerAgreementOnBehalfOfPage
            .DoNotSignAgreement();

        var page1 = await page.GoToTrainingProviderLink();

        var page2 = await page1.AddTrainingProviderLater();

        _homePage = await page2.SelectGoToYourEmployerAccountHomepage();
    }

    [Given(@"an Employer creates a Non Levy Account and Signs the Agreement")]
    [When(@"an Employer creates a Non Levy Account and Signs the Agreement")]
    public async Task EmployerCreatesANonLevyAccountAndSignsTheAgreement() =>
        await GivenAnEmployerAccountWithSpecifiedTypeOrgIsCreatedAndAgeementIsSigned(OrgType.Company);

    //[When(@"an Employer creates a Non Levy Account and not Signs the Agreement during registration")]
    //public async Task WhenAnEmployerCreatesANonLevyAccountAndNotSignsTheAgreementDuringRegistration() =>
    //    await GivenAnEmployerAccountWithSpecifiedTypeOrgIsCreatedAndAgeementIsNotSigned(OrgType.Company);

    [Given(@"an Employer Account with (Company|PublicSector|Charity) Type Org is created and agreement is Signed")]
    [When(@"an Employer Account with (Company|PublicSector|Charity) Type Org is created and agreement is Signed")]
    [Then(@"a Levy Employer Account with (Company|PublicSector|Charity) Type Org is created and agreement is Signed")]
    public async Task GivenAnEmployerAccountWithSpecifiedTypeOrgIsCreatedAndAgeementIsSigned(OrgType orgType)
    {
        _accountCreationStepsHelper.UpdateOrganisationName(orgType);

        await CreateUserAccountAndAddOrg(orgType);

        await SignTheAgreementAndAddProviderLater();
    }

    [When(@"an Employer Account with (Company|PublicSector|Charity) Type Org is created")]
    [When(@"an Employer Account with (Company|PublicSector|Charity) Type Org is created and agreement is Not Signed")]
    public async Task GivenAnEmployerAccountWithSpecifiedTypeOrgIsCreatedAndAgeementIsNotSigned(OrgType orgType)
    {
        await CreateUserAccountAndAddOrg(orgType);

        await DoNotSignTheAgreementAndAddProviderLater();
    }

    [Given(@"an Employer creates a Levy Account and Signs the Agreement")]
    [When(@"an Employer creates a Levy Account and Signs the Agreement")]
    public async Task EmployerCreatesALevyAccountAndSignsTheAgreement() =>
        await GivenAnEmployerAccountWithSpecifiedTypeOrgIsCreatedAndAgeementIsSigned(OrgType.Company);

    //[When(@"an Employer creates a Levy Account and not Signs the Agreement during registration")]
    //public async Task WhenAnEmployerCreatesALevyAccountAndNotSignsTheAgreementDuringRegistration() =>
    //    await GivenAnEmployerAccountWithSpecifiedTypeOrgIsCreatedAndAgeementIsNotSigned(OrgType.Company);

    [When(@"the Employer initiates adding same Org of (Company|PublicSector|Charity) Type again")]
    public async Task WhenTheEmployerInitiatesAddingSameOrgTypeAgain(OrgType orgType) =>
        _selectYourOrganisationPage = await AccountCreationStepsHelper.SearchForAnotherOrg(_homePage, orgType);

    //[Then(@"'Already added' message is shown to the User")]
    //public async Task ThenAlreadyAddedMessageIsShownToTheUser() => await _selectYourOrganisationPage.VerifyOrgAlreadyAddedMessage();

    [Then(@"ApprenticeshipEmployerType in Account table is marked as (.*)")]
    public async Task ThenApprenticeshipEmployerTypeInAccountTableIsMarkedAs(string expectedApprenticeshipEmployerType)
    {
        var actualApprenticeshipEmployerType = await _registrationSqlDataHelper.GetAccountApprenticeshipEmployerType(_registrationDataHelper.RandomEmail);

        Assert.AreEqual(expectedApprenticeshipEmployerType, actualApprenticeshipEmployerType);
    }

    [When(@"Signs the Agreement from Account HomePage Panel")]
    public async Task WhenSignsTheAgreementFromAccountHomePagePanel()
    {
        var page = await AccountCreationStepsHelper.SignAgreementFromHomePage(_homePage);
        
        await page.ClickOnViewYourAccountButton();
    }

    //[Then(@"'Start adding apprentices now' task link is displayed under Tasks pane")]
    //public async Task ThenTaskLinkIsDisplayedUnderTasksPane() => new TasksHomePage(_context).VerifyStartAddingApprenticesNowTaskLink();

    //[Then(@"'These details are already in use' page is displayed when Another Employer tries to register the account with the same Aorn and Paye details")]
    //public async Task ThenPageIsDisplayedWhenAnotherEmployerTriesToRegisterTheAccountWithTheSameAornAndPayeDetails()
    //{
    //    await _accountCreationStepsHelper.SignOut();

    //    _objectContext.SetRegisteredEmail(_registrationDataHelper.AnotherRandomEmail);

    //    _addAPAYESchemePage = await _accountCreationStepsHelper.RegisterUserAccount();

    //    _theseDetailsAreAlreadyInUsePage = AccountCreationStepsHelper.ReEnterAornDetails(_addAPAYESchemePage);
    //}

    //[Then(@"'Add a PAYE Scheme' page is displayed when Employer clicks on 'Use different details' button")]
    //[Then(@"'AddPayeSchemeUsingGGDetails' page is displayed when Employer clicks on 'Use different details' button")]
    //public async Task ThenAddAPAYESchemePageIsDisplayedWhenEmployerClicksOnUseDifferentDetailsButton() =>
    //    _addPayeSchemeUsingGGDetailsPage = _theseDetailsAreAlreadyInUsePage.CickUseDifferentDetailsButtonInTheseDetailsAreAlreadyInUsePage();

    //[Then(@"'Add a PAYE Scheme' page is displayed when Employer clicks on Back link on the 'PAYE scheme already in use' page")]
    //public async Task ThenAddAPAYESchemePageIsDisplayedWhenEmployerClicksOnBackLinkOnThePage()
    //{
    //    var page = await _addPayeSchemeUsingGGDetailsPage.ClickBackButton();

    //    _enterYourPAYESchemeDetailsPage = _addPayeSchemeUsingGGDetailsPage.ClickBackButton().CickBackLinkInTheseDetailsAreAlreadyInUsePage().ReEnterTheSameAornDetailsAndContinue().CickBackLinkInTheseDetailsAreAlreadyInUsePage();
    //}
    

    //[When(@"the User is on the 'Check your details' page after adding PAYE details through AORN route")]
    //public async Task WhenTheUserIsOnTheCheckYourDetailsPageAfterAddingPAYEDetailsThroughAORNRoute()
    //{
    //    _tprSqlDataHelper.CreateSingleOrgAornData();
    //    _checkYourDetailsPage = AccountCreationStepsHelper.AddPayeDetailsForSingleOrgAornRoute(_addAPAYESchemePage);
    //}

    //[Then(@"choosing to change the AORN number displays 'Enter your PAYE scheme details' page")]
    //public async Task ThenChoosingToChangeTheAORNNumberDisplaysPage() =>
    //    _checkYourDetailsPage = _checkYourDetailsPage.ClickAornChangeLink().EnterAornAndPayeDetailsForSingleOrgScenarioAndContinue();

    //[Then(@"choosing to change the PAYE scheme displays 'Enter your PAYE scheme details' page")]
    //public async Task ThenChoosingToChangeThePAYESchemeDisplaysEnterYourPAYESchemeDetailsPage() =>
    //    _checkYourDetailsPage = _checkYourDetailsPage.ClickPayeSchemeChangeLink().AddAORN().EnterAornAndPayeDetailsForSingleOrgScenarioAndContinue();

    //[Then(@"choosing to change the Organisation selected displays 'Search for your Organisation' page")]
    //public async Task ThenChoosingToChangeTheOrganisationSelectedDisplaysSearchForYourOrganisationPage() =>
    //    _checkYourDetailsPage.ClickOrganisationChangeLink();

    [When(@"the User is on the 'Add a PAYE Scheme' page")]
    public async Task WhenTheUserIsOnThePage() => _enterYourPAYESchemeDetailsPage = await _addAPAYESchemePage.AddAORN();

    //[Then(@"choosing to Continue with (BlankAornAndBlankPaye|BlankAornValidPaye|BlankPayeValidAorn|InvalidAornAndInvalidPaye) displays relevant Error text")]
    //public async Task ThenChoosingToContinueWithBlankAornValidPayeDisplaysRelevantErrorText(string errorCase)
    //{
    //    string blankAornFieldErrorMessage = EnterYourPAYESchemeDetailsPage.BlankAornFieldErrorMessage;
    //    string blankPayeFieldErrorMessage = EnterYourPAYESchemeDetailsPage.BlankPayeFieldErrorMessage;
    //    string aornInvalidFormatErrorMessage = EnterYourPAYESchemeDetailsPage.AornInvalidFormatErrorMessage;
    //    string payeInvalidFormatErrorMessage = EnterYourPAYESchemeDetailsPage.PayeInvalidFormatErrorMessage;

    //    switch (errorCase)
    //    {
    //        case "BlankAornAndBlankPaye":
    //            _enterYourPAYESchemeDetailsPage.Continue();
    //            Assert.AreEqual(blankAornFieldErrorMessage, _enterYourPAYESchemeDetailsPage.GetErrorMessageAboveAornTextBox());
    //            Assert.AreEqual(blankPayeFieldErrorMessage, _enterYourPAYESchemeDetailsPage.GetErrorMessageAbovePayeTextBox());
    //            break;
    //        case "BlankAornValidPaye":
    //            _enterYourPAYESchemeDetailsPage.EnterAornAndPayeAndContinue("", _objectContext.GetGatewayPaye(0));
    //            Assert.AreEqual(blankAornFieldErrorMessage, _enterYourPAYESchemeDetailsPage.GetErrorMessageAboveAornTextBox());
    //            break;
    //        case "BlankPayeValidAorn":
    //            _tprSqlDataHelper.CreateSingleOrgAornData();
    //            _enterYourPAYESchemeDetailsPage.EnterAornAndPayeAndContinue(_registrationDataHelper.AornNumber, "");
    //            Assert.AreEqual(blankPayeFieldErrorMessage, _enterYourPAYESchemeDetailsPage.GetErrorMessageAbovePayeTextBox());
    //            break;
    //        case "InvalidAornAndInvalidPaye":
    //            _enterYourPAYESchemeDetailsPage.EnterAornAndPayeAndContinue("InvalidAorn", "InvalidPaye");
    //            Assert.AreEqual(aornInvalidFormatErrorMessage, _enterYourPAYESchemeDetailsPage.GetErrorMessageAboveAornTextBox());
    //            Assert.AreEqual(payeInvalidFormatErrorMessage, _enterYourPAYESchemeDetailsPage.GetErrorMessageAbovePayeTextBox());
    //            break;
    //    }
    //}

    //[Then(@"choosing to enter AORN and PAYE details in the right format but non existing ones for 3 times displays 'Sorry Account disabled' Page")]
    //public async Task ThenChoosingToEnterAORNAndPAYEDetailsInTheRightFormatButNonExistingOnesForTimesDisplaysPage()
    //{
    //    string InvalidErrorMessage1stAttempt = EnterYourPAYESchemeDetailsPage.InvalidAornAndPayeErrorMessage1stAttempt;
    //    string InvalidErrorMessage2ndAttempt = EnterYourPAYESchemeDetailsPage.InvalidAornAndPayeErrorMessage2ndAttempt;

    //    EnterInvalidAornAndPaye();
    //    Assert.AreEqual(InvalidErrorMessage1stAttempt, _enterYourPAYESchemeDetailsPage.GetInvalidAornAndPayeErrorMessage());
    //    EnterInvalidAornAndPaye();
    //    Assert.AreEqual(InvalidErrorMessage2ndAttempt, _enterYourPAYESchemeDetailsPage.GetInvalidAornAndPayeErrorMessage());
    //    EnterInvalidAornAndPaye();

    //    _usingYourGovtGatewayDetailsPage = new WeCouldNotVerifyYourDetailsPage(_context).ClickAddViaGGLink();
    //}

    [Then(@"Employer is able to complete registration through GG route")]
    public async Task ThenEmployerIsAbleToCompleteRegistrationThroughGGRoute()
    {
        var page = await _usingYourGovtGatewayDetailsPage.ContinueToGGSignIn();

        _searchForYourOrganisationPage = await page.SignInTo(0);

        await AddOrganisationDetails();

        await SignTheAgreementAndAddProviderLater();
    }

    [Then(@"the Employer is able to rename the Account")]
    public async Task ThenTheEmployerIsAbleToRenameTheAccount()
    {
        var newOrgName = _objectContext.GetOrganisationName() + "_Renamed";

        var page = await _homePage.GoToRenameAccountPage();

        _homePage = await page.EnterNewNameAndContinue(newOrgName);

        await _homePage.VerifyAccountName(newOrgName);

    }

    [When(@"the User is on the 'Check your details' page after adding PAYE and Company Type Org details")]
    public async Task WhenTheUserIsOnTheCheckYourDetailsPageAfterAddingPAYEAndCompanyTypeOrgDetails()
    {
        _searchForYourOrganisationPage = await CreateAnUserAcountAndAddPaye();

        _checkYourDetailsPage = await AccountCreationStepsHelper.SearchAndSelectOrg(_searchForYourOrganisationPage, OrgType.Company);
    }

    //[Then(@"the User is able to choose a different Company by clicking on Change Organisation")]
    //public async Task ThenTheUserIsAbleToChooseADifferentCompanyByClickingOnChangeOrganisation()
    //{
    //    _searchForYourOrganisationPage = _checkYourDetailsPage.ClickOrganisationChangeLink();
    //    _checkYourDetailsPage = AccountCreationStepsHelper.SearchAndSelectOrg(_searchForYourOrganisationPage, OrgType.Company2);
    //    Assert.AreEqual(_objectContext.GetOrganisationName(), _checkYourDetailsPage.GetOrganisationName());
    //}

    //[Then(@"the User is able to choose a different PAYE scheme by clicking on Change PAYE scheme and complete registation journey")]
    //public async Task ThenTheUserIsAbleToChooseADifferentPAYESchemeByClickingOnChangePAYESchemeAndCompleteRegistationJourney()
    //{
    //    _addAPAYESchemePage = _checkYourDetailsPage.ClickPayeSchemeChangeLink();
    //    _searchForYourOrganisationPage = AccountCreationStepsHelper.AddADifferentPaye(_addAPAYESchemePage);
    //    _checkYourDetailsPage = AccountCreationStepsHelper.SearchAndSelectOrg(_searchForYourOrganisationPage, OrgType.Company2);
    //    Assert.AreEqual(_objectContext.GetGatewayPaye(1), await _checkYourDetailsPage.GetPayeScheme());
    //}

    [When(@"the Employer logsout of the Account")]
    public async Task WhenTheEmployerLogsoutOfTheAccount() => _indexPage = await _accountCreationStepsHelper.SignOut();

    [Then(@"an Employer is able to create another Account with the same PublicSector Type Org but with a different PAYE")]
    public async Task ThenAnEmployerIsAbleToCreateAnotherAccountWithTheSamePublicSectorTypeOrgButWithADifferentPAYE()
    {
        _addAPAYESchemePage = await _accountCreationStepsHelper.CreateAnotherUserAccount(_indexPage);

        await AddPayeDetails(1);

        await AddOrganisationTypeDetails(OrgType.PublicSector);
    }

    //[Then(@"the Employer is able to Add Another NonLevy PAYE scheme to the Account")]
    //[Then(@"the Employer is able to Add Another Levy PAYE scheme to the Account")]
    //public async Task ThenTheEmployerIsAbleToAddAnotherPAYESchemeToTheAccount() =>
    //    _homePage = AccountCreationStepsHelper.AddAnotherPayeSchemeToTheAccount(_homePage);

    //[Then(@"the Employer is able to Remove the second PAYE scheme added from the Account")]
    //public async Task ThenTheEmployerIsAbleToRemoveTheSecondPAYESchemeAddedFromTheAccount() =>
    //    AccountCreationStepsHelper.RemovePayeSchemeFromTheAccount(_homePage);

    [Then(@"the Employer is able to add another Account with (Company|PublicSector|Charity) Type Org to the same user login")]
    public async Task ThenTheEmployerIsAbleToAddAnotherAccountToTheSameUserLogin(OrgType orgType) =>
        _homePage = await _accountCreationStepsHelper.AddNewAccount(_homePage, 1, orgType);

    [Then(@"the Employer is able to switch between the Accounts")]
    public async Task ThenTheEmployerIsAbleToSwitchBetweenTheAccounts()
    {
        await OpenAccount(_objectContext.GetOrganisationName());

        await OpenAccount(_objectContext.GetAdditionalOrganisationName(1));
    }
    private async Task CreateUserAccountAndAddOrg(OrgType orgType)
    {
        await CreateAnUserAcountAndAddPaye();

        await AddOrganisationTypeDetails(orgType);
    }

    //private async Task EnterInvalidAornAndPaye() =>
    //    _enterYourPAYESchemeDetailsPage.EnterAornAndPayeAndContinue(AornDataHelper.InvalidAornNumber, RegistrationDataHelper.InvalidPaye);

    private async Task<SearchForYourOrganisationPage> CreateAnUserAcountAndAddPaye()
    {
        await AUserAccountIsCreated();

        return await AddPayeDetails();
    }

    private async Task<SearchForYourOrganisationPage> AddPayeDetails(int payeIndex)
    {
        var page = await _addAPAYESchemePage.AddPaye();

        var page1 = await page.ContinueToGGSignIn();

        return _searchForYourOrganisationPage = await page1.SignInTo(payeIndex);
    }
        

    private async Task<HomePage> OpenAccount(string orgName)
    {
        var page = await _homePage.GoToYourAccountsPage();

        return _homePage = await page.ClickAccountLink(orgName);
    }
}
