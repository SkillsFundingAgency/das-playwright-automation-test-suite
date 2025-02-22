using SFA.DAS.Registration.UITests.Project.Helpers;
using SFA.DAS.Registration.UITests.Project.Pages;
using static SFA.DAS.Registration.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.Registration.UITests.Project.Steps;

[Binding]
public class AddOrRemoveOrgSteps(ScenarioContext context)
{
    private readonly RegistrationDataHelper _registrationDataHelper = context.Get<RegistrationDataHelper>();
    private HomePage _homePage;
    private CheckYourDetailsPage _checkYourDetailsPage;
    private YourOrganisationsAndAgreementsPage _yourOrganisationsAndAgreementsPage;

    //[Then(@"the Employer is Not allowed to Remove the first Org added")]
    //public async Task ThenTheEmployerIsNotAllowedToRemoveTheFirstOrgAdded() =>
    //    Assert.AreEqual(new HomePage(context).GoToYourOrganisationsAndAgreementsPage().IsRemoveLinkBesideNewlyAddedOrg(), false);

    [Given(@"the Employer initiates adding another Org of (Company|PublicSector|Charity|Charity2) Type")]
    [When(@"the Employer initiates adding another Org of (Company|PublicSector|Charity|Charity2) Type")]
    public async Task WhenTheEmployerInitiatesAddingAnotherOrgType(OrgType orgType)
    {
        _registrationDataHelper.SetAccountNameAsOrgName = false;

        var page = await AccountCreationStepsHelper.SearchForAnotherOrg(new HomePage(context, true), orgType);

        _checkYourDetailsPage = await page.SelectYourOrganisation(orgType);
    }

    [Then(@"the new Org added is shown in the Account Organisations list")]
    public async Task ThenTheNewOrgAddedIsShownInTheAccountOrganisationsList()
    {
        var page = await _checkYourDetailsPage.ClickYesContinueButton();

        _yourOrganisationsAndAgreementsPage = await page.GoToYourOrganisationsAndAgreementsPage();

        await _yourOrganisationsAndAgreementsPage.VerifyNewlyAddedOrgIsPresent();
    }

    [Then(@"the Employer is able check the details of the Charity Org added are displayed in the 'Check your details' page and Continue")]
    public async Task ThenTheEmployerIsAbleToCheckTheDetailsOfTheCharityOrgAddedAreDisplayedInThePageAndContinue()
    {
        await VerifyOrgDetails(_registrationDataHelper.CharityTypeOrg1Number, _registrationDataHelper.CharityTypeOrg1Name);

        await ThenTheNewOrgAddedIsShownInTheAccountOrganisationsList();
    }

    [Then(@"the Employer is able check the details of the 2nd Charity Org added are displayed in the 'Check your details' page and Continue")]
    public async Task ThenTheEmployerIsAbleToCheckTheDetailsOfThe2ndCharityOrgAddedAreDisplayedInThePageAndContinue()
    {
        await VerifyOrgDetails(_registrationDataHelper.CharityTypeOrg2Number, _registrationDataHelper.CharityTypeOrg2Name);

        await ThenTheNewOrgAddedIsShownInTheAccountOrganisationsList();
    }

    //[Then(@"Employer is Allowed to remove the second Org added from the account")]
    //public async Task ThenEmployerIsAllowedToRemoveTheSecondOrgAddedFromTheAccount()
    //{
    //    var page = await _yourOrganisationsAndAgreementsPage.ClickOnRemoveAnOrgFromYourAccountLink();

    //    var page1 = await page.SelectYesRadioOptionAndClickContinueInRemoveOrganisationPage();

    //    await page1.VerifyOrgRemovedMessageInHeader();
    //}


    [When(@"the Employer adds another Org to the Account")]
    public async Task WhenTheEmployerAddsAnotherOrgToTheAccount() => await AddOrgToTheAccount(OrgType.Company);

    [Then(@"the Sign Agreement journey from the Account home page shows Accepted Agreement page")]
    public async Task ThenTheSignAgreementJourneyFromTheAccountHomePageShowsAcceptedAgreementPage()
    {
        var page = await SignAgreementFromHomePage();

        await page.ClickOnViewYourAccount();
    }

    [When(@"the Employer adds two additional Orgs to the Account")]
    public async Task WhenTheEmployerAddsTwoAdditionalOrgsToTheAccount()
    {
        await AddOrgToTheAccount(OrgType.Company2);

        await AddOrgToTheAccount(OrgType.Charity);
    }

    [Then(@"the Sign Agreement journey from the Account home page shows Accepted Agreement page with link to review other pending agreements")]
    public async Task ThenTheSignAgreementJourneyFromTheAccountHomePageShowsAcceptedAgreementPageWithLinkToReviewOtherPendingAgreements()
    {
        var page = await SignAgreementFromHomePage();

        await page.ClickOnReviewAndAcceptYourOtherAgreementsLink();
    }

    private async Task VerifyOrgDetails(string orgNumber, string OrgName)
    {
        await _checkYourDetailsPage.VerifyDetails(OrgName);

        await _checkYourDetailsPage.VerifyDetails(orgNumber);
    }

    private async Task AddOrgToTheAccount(OrgType orgType)
    {
        await WhenTheEmployerInitiatesAddingAnotherOrgType(orgType);

        var page = await _checkYourDetailsPage.ClickYesContinueButton();

        _homePage = await page.GoToHomePage();
    }

    private async Task<YouHaveAcceptedTheEmployerAgreementPage> SignAgreementFromHomePage() => await AccountCreationStepsHelper.SignAdditionalAgreementFromHomePage(_homePage);
}
