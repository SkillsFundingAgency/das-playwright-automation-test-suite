using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using static SFA.DAS.EmployerPortal.UITests.Project.Helpers.EnumHelper;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]
public class AddOrRemoveOrgSteps(ScenarioContext context)
{
    private readonly EmployerPortalDataHelper _employerPortalDataHelper = context.Get<EmployerPortalDataHelper>();
    private HomePage _homePage;
    private CheckYourDetailsPage _checkYourDetailsPage;
    private YourOrganisationsAndAgreementsPage _yourOrganisationsAndAgreementsPage;

    [Then(@"the Employer is Not allowed to Remove the first Org added")]
    public async Task ThenTheEmployerIsNotAllowedToRemoveTheFirstOrgAdded()
    {
        var page = await new HomePage(context).GoToYourOrganisationsAndAgreementsPage();

        await page.VerifyRemoveLinkHidden();
    }

    [Given(@"the Employer initiates adding another Org of (Company|PublicSector|Charity|Charity2) Type")]
    [When(@"the Employer initiates adding another Org of (Company|PublicSector|Charity|Charity2) Type")]
    public async Task WhenTheEmployerInitiatesAddingAnotherOrgType(OrgType orgType)
    {
        _employerPortalDataHelper.SetAccountNameAsOrgName = false;

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
        await VerifyOrgDetails(_employerPortalDataHelper.CharityTypeOrg1Number, _employerPortalDataHelper.CharityTypeOrg1Name);

        await ThenTheNewOrgAddedIsShownInTheAccountOrganisationsList();
    }

    [Then(@"the Employer is able check the details of the 2nd Charity Org added are displayed in the 'Check your details' page and Continue")]
    public async Task ThenTheEmployerIsAbleToCheckTheDetailsOfThe2ndCharityOrgAddedAreDisplayedInThePageAndContinue()
    {
        await VerifyOrgDetails(_employerPortalDataHelper.CharityTypeOrg2Number, _employerPortalDataHelper.CharityTypeOrg2Name);

        await ThenTheNewOrgAddedIsShownInTheAccountOrganisationsList();
    }

    [Then(@"Employer is Allowed to remove the second Org added from the account")]
    public async Task ThenEmployerIsAllowedToRemoveTheSecondOrgAddedFromTheAccount()
    {
        var page = await _yourOrganisationsAndAgreementsPage.ClickOnRemoveAnOrgFromYourAccountLink();

        var page1 = await page.SelectYesRadioOptionAndClickContinueInRemoveOrganisationPage();

        await page1.VerifyOrgRemovedMessageInHeader();
    }

    [When(@"the Employer adds another Org to the Account")]
    public async Task WhenTheEmployerAddsAnotherOrgToTheAccount() => await AddOrgToTheAccount(OrgType.Company);

    [Then(@"the Sign Agreement journey from the Account home page shows Accepted Agreement page")]
    public async Task ThenTheSignAgreementJourneyFromTheAccountHomePageShowsAcceptedAgreementPage()
    {
        var page = await SignAdditionalAgreementFromHomePage();

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
        var page = await SignAdditionalAgreementFromHomePage();

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

    private async Task<YouHaveAcceptedTheEmployerAgreementPage> SignAdditionalAgreementFromHomePage() => await AccountCreationStepsHelper.SignAgreementFromHomePage(_homePage);
}
