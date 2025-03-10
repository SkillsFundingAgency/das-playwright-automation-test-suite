using SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Steps;

[Binding]
class OrganisationDetailsChangeSteps
{
    private readonly ScenarioContext _context;
    private readonly ObjectContext _objectContext;
    private readonly EmployerPortalSqlDataHelper _employerPortalSqlDataHelper;
    private YourAgreementsWithTheEducationAndSkillsFundingAgencyPage _yourAgreementsWithTheEducationAndSkillsFundingAgencyPage;
    private ReviewYourDetailsPage _reviewYourDetailsPage;

    public OrganisationDetailsChangeSteps(ScenarioContext context)
    {
        _context = context;
        _objectContext = _context.Get<ObjectContext>();
        _employerPortalSqlDataHelper = context.Get<EmployerPortalSqlDataHelper>();
    }

    [When(@"the Employer reviews Agreement page")]
    public async Task WhenTheEmployerReviewsAgreementPage() => _yourAgreementsWithTheEducationAndSkillsFundingAgencyPage = await ClickViewAgreementLinkInYourOrganisationsAndAgreementsPage();

    [Then(@"clicking on 'Update these details' link displays 'Review your details' page showing (These details are the same as those previously held|We've retrieved the most up-to-date details we could find for your organisation)")]
    public async Task ThenClickingOnUpdateTheseDetailsLinkDisplaysReviewYourDetailsPageShowingExpectedMessage(string expectedMessage)
    {
        _reviewYourDetailsPage = await _yourAgreementsWithTheEducationAndSkillsFundingAgencyPage.ClickUpdateTheseDetailsLinkInReviewYourDetailsPage();

        await _reviewYourDetailsPage.VerifyInfoTextInReviewYourDetailsPage(expectedMessage);
    }

    [When(@"the Employer revisits the Agreement page during change in Organisation name scenario")]
    public async Task WhenTheEmployerRevisitsTheAgreementPageDuringChangeInOrganisationNameScenario()
    {
        var loginUser = _objectContext.GetLoginCredentials();

        await _employerPortalSqlDataHelper.UpdateLegalEntityName(loginUser.Username);

        _yourAgreementsWithTheEducationAndSkillsFundingAgencyPage = await ClickViewAgreementLinkInYourOrganisationsAndAgreementsPage();
    }

    [Then(@"continuing by choosing 'Update details' option displays 'Details updated' page showing (You've successfully updated your organisation details)")]
    public async Task ThenContinuingByChoosingOptionDisplaysPageShowing(string _)
    {
        var page = await _reviewYourDetailsPage.SelectUpdateMyDetailsRadioOptionAndContinueInReviewYourDetailsPage();

        await page.SelectGoToHomePageOptionAndContinueInDetailsUpdatedPage();
    }

    private async Task<YourAgreementsWithTheEducationAndSkillsFundingAgencyPage> ClickViewAgreementLinkInYourOrganisationsAndAgreementsPage()
    {
        var page = await new HomePage(_context, true).GoToHomePage();

        var page1 = await page.GoToYourOrganisationsAndAgreementsPage();

        return await page1.ClickViewAgreementLink();
    }


    [Then(@"the 'Update these details' link is not displayed for PublicSector Type Org")]
    public async Task ThenTheUpdateTheseDetailsLinkIsNotDisplayedForPublicSectorTypeOrg() => await _yourAgreementsWithTheEducationAndSkillsFundingAgencyPage.VerifyIfUpdateTheseDetailsLinkIsHidden();
}
