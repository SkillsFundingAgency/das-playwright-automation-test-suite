using SFA.DAS.EPAO.UITests.Project.Helpers;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Steps;

[Binding]
public class AdminSteps(ScenarioContext context) : EPAOBaseSteps(context)
{
    private ConfirmReasonBasePage confirmReasonBasePage;
    private ConfirmationAmendReprintBasePage confirmationAmendReprintBasePage;

    [Then(@"the admin can add organisation")]
    public async Task ThenTheAdminCanAddOrganisation() => await AdminStepshelper.AddOrganisation(await GoToEpaoAdminHomePage());

    [Then(@"the admin can make organisation to be live")]
    public async Task ThenTheAdminCanMakeOrganisationToBeLive()
    {
        var epaoid = EPAOAdminDataHelper.MakeLiveOrganisationEpaoId;

        await ePAOAdminSqlDataHelper.UpdateOrgStatusToNew(epaoid);

        objectContext.SetOrganisationIdentifier(epaoid);

        organisationDetailsPage = await AdminStepshelper.MakeEPAOOrganisationLive(await GoToEpaoAdminHomePage());
    }

    [Then(@"the admin can edit the organisation")]
    public async Task ThenTheAdminCanEditTheOrganisation()
    {
        var page = await organisationDetailsPage.EditOrganisation();

        organisationDetailsPage = await page.EditDetails();

        await organisationDetailsPage.VerifyOrganisationCharityNumber();

        await organisationDetailsPage.VerifyOrganisationCompanyNumber();
    }

    [Then(@"the admin can search using organisation name")]
    public async Task ThenTheAdminCanSearchUsingOrganisationName() => await SearchEpaoRegister(EPAOAdminDataHelper.OrganisationName);

    [Then(@"the admin can search using organisation epao id")]
    public async Task ThenTheAdminCanSearchUsingOrganisationEpaoId() => await SearchEpaoRegister(EPAOAdminDataHelper.OrganisationEpaoId);

    [Then(@"the admin can add contact details")]
    public async Task ThenTheAdminCanAddContactDetails()
    {
        var page = await organisationDetailsPage.AddNewContact();

        var page1 = await page.AddContact();

        var page2 = await page1.ReturnToOrganisationDetailsPage();

        var page3 = await page2.SelectContact();

        organisationDetailsPage = await page3.ReturnToOrganisationDetailsPage();
    }


    [Then(@"the admin can search using organisation ukprn")]
    public async Task ThenTheAdminCanSearchUsingOrganisationUkprn() => await SearchEpaoRegister(EPAOAdminDataHelper.OrganisationUkprn);

    [Given(@"the (Admin all roles user) is logged into the Admin Service Application")]
    public async Task GivenTheAdminIsLoggedIntoTheAdminServiceApplication(string _) => staffDashboardPage = await GoToEpaoAdminHomePage();

    [Given(@"the Admin can search using learner uln")]
    [When(@"the Admin can search using learner uln")]
    [Then(@"the Admin can search using learner uln")]
    public async Task GivenWhenThenTheAdminCanSearchUsingUln() => certificateDetailsPage = await AdminStepshelper.SearchAssessments(staffDashboardPage, ePAOAdminDataHelper.LearnerUln);

    [When(@"the Admin amends the certificate with ticket reference '(.*)' and selects reason '(.*)'")]
    public async Task WhenTheAdminAmendsTheCertificateAndEnteresAReasonForAmendment(string ticketReference, string amendReason) => await EnterTicketRefeferenceAndSelectReason(await certificateDetailsPage.ClickAmendCertificateLink(), ticketReference, amendReason);

    [When(@"the Admin reprints the certificate with ticket reference '(.*)' and selects reason '(.*)'")]
    public async Task WhenTheAdminReprintTheCertificateAndEnteresAReasonForAmendment(string ticketReference, string reprintReason) => await EnterTicketRefeferenceAndSelectReason(await certificateDetailsPage.ClickReprintCertificateLink(), ticketReference, reprintReason);

    [Then(@"the SendTo can be changed from '(employer|apprentice)' to '(employer|apprentice)'")]
    public async Task ThenTheSendToCanBeChangedFrom(string currentSentTo, string newSendTo)
    {
        var page = await checkAndSubmitAssessmentDetailsPage.ClickChangeSendToLink();

        certificateAddressPage = await page.ChangeSendToRadioButtonAndContinue(currentSentTo, newSendTo);
    }

    [Then(@"the new address can be entered without employer name or recipient")]
    public async Task ThenTheNewAddressCanBeEnteredWithoutEmployerNameOrRecipient() => await EnterNewAdddress(string.Empty, string.Empty);

    [Then(@"the new address can be entered with employer name '(.*)' and recipient '(.*)'")]
    public async Task ThenThenNewAddressCanBeEnteredWithEmployerNameAndRecipient(string employer, string recipient) => await EnterNewAdddress(employer, recipient);

    [Then(@"the recipient's name on the check page is '(.*)'")]
    public async Task ThenTheRecipientIsTheNewOneWhichWasEntered(string recipient) => await checkAndSubmitAssessmentDetailsPage.VerifyRecipient(recipient);

    [Then(@"the recipient is defaulted to the apprentice name")]
    public async Task ThenTheRecipientIsDefaultedToTheApprenticeName() => await checkAndSubmitAssessmentDetailsPage.VerifyRecipientIsApprentice();

    [Then(@"the address contains the employer name '(.*)'")]
    public async Task ThenTheAddressContainsTheEmployerName(string employer) => await checkAndSubmitAssessmentDetailsPage.VerifyEmployer(employer);

    [When(@"the Admin amends the certificate")]
    public async Task WhenTheAdminAmendsTheCertificate() => confirmReasonBasePage = await certificateDetailsPage.ClickAmendCertificateLink();

    [When(@"the Admin reprints the certificate")]
    public async Task WhenTheAdminReprintsTheCertificate() => confirmReasonBasePage = await certificateDetailsPage.ClickReprintCertificateLink();

    [Then(@"the ticket reference '(.*)' and reason for amend '(.*)' can be entered")]
    public async Task ThenTheReasonForAmendCanBeEntered(string ticketReference, string reasonForAmend) => checkAndSubmitAssessmentDetailsPage = await confirmReasonBasePage.EnterTicketRefeferenceAndSelectReason(ticketReference, reasonForAmend);

    [Then(@"the ticket reference '(.*)' and reason for reprint '(.*)' can be entered")]
    public async Task ThenTheReasonForReprintCanBeEntered(string ticketReference, string reasonForReprint) => checkAndSubmitAssessmentDetailsPage = await confirmReasonBasePage.EnterTicketRefeferenceAndSelectReason(ticketReference, reasonForReprint);

    [Then(@"the amend can be confirmed")]
    public async Task ThenTheAmendCanBeConfirmed() => confirmationAmendReprintBasePage = await checkAndSubmitAssessmentDetailsPage.ClickConfirmAmend();

    [Then(@"the reprint can be confirmed")]
    public async Task ThenTheReprintCanBeConfirmed() => confirmationAmendReprintBasePage = await checkAndSubmitAssessmentDetailsPage.ClickConfirmReprint();

    [Then(@"the certificate history contains the incident number '(.*)' and amend reason '(.*)'")]
    public async Task ThenTheCertificateHistoryContainsTheReasonForAmending(string incidentNumber, string amendReason)
    {
        var page = await SelectACertificate();

        await page.VerifyActionHistoryItem(1, "AmendReason");

        await page.VerifyIncidentNumber(1, incidentNumber);

        await page.VerifyFirstReason(1, amendReason);
    }

    [Then(@"the certificate history contains the incident number '(.*)' and reprint reason '(.*)'")]
    public async Task ThenTheCertificateHistoryContainsTheReasonForReprinting(string incidentNumber, string reprintReason)
    {
        var page = await SelectACertificate();

        await page.VerifyActionHistoryItem(1, "Reprint");

        await page.VerifyActionHistoryItem(2, "ReprintReason");

        await page.VerifyIncidentNumber(2, incidentNumber);

        await page.VerifyFirstReason(2, reprintReason);
    }

    [Then(@"the admin can search batches")]
    public async Task ThenTheAdminCanSearchBatches()
    {
        var page = await GoToEpaoAdminHomePage();

        var page1 = await page.SearchEPAOBatch();

        var page2 = await page1.SearchBatches();

        await page2.VerifyingBatchDetails();

        await page2.SignOut();
    }

    private async Task<CertificateDetailsPage> SelectACertificate()
    {
        var page = await confirmationAmendReprintBasePage.ClickSearchAgain();

        var page1 = await page.SearchFor(ePAOAdminDataHelper.LearnerUln);

        return await page1.SelectACertificate();
    }

    private async Task<CheckAndSubmitAssessmentDetailsPage> EnterNewAdddress(string employer, string recipient)
    {
        await certificateAddressPage.EnterRecipientName(recipient);

        await certificateAddressPage.EnterEmployerName(employer);

        await certificateAddressPage.EnterAddress();

        return checkAndSubmitAssessmentDetailsPage = await certificateAddressPage.EnterReasonForChangeAndContinue(string.IsNullOrEmpty(employer) && string.IsNullOrEmpty(recipient) ? "Employer Name and Recipient details are not updated" : "Employer Name and Recipient details updated");

    }

    private async Task<CheckAndSubmitAssessmentDetailsPage> EnterTicketRefeferenceAndSelectReason(ConfirmReasonBasePage page, string ticketReference, string reason)
    {
        return checkAndSubmitAssessmentDetailsPage = await page.EnterTicketRefeferenceAndSelectReason(ticketReference, reason);
    }

    private async Task<StaffDashboardPage> GoToEpaoAdminHomePage() => await ePAOHomePageHelper.LoginToEpaoAdminHomePage(false);

    private async Task SearchEpaoRegister(string value) { objectContext.SetOrganisationIdentifier(value); organisationDetailsPage = await AdminStepshelper.SearchEpaoRegister(await GoToEpaoAdminHomePage()); }
}