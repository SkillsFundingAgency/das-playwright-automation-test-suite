using SFA.DAS.EPAO.UITests.Project;
using SFA.DAS.EPAO.UITests.Project.Helpers;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.ManageUsers;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;
using SFA.DAS.Login.Service.Project;
using System.Security;
using System.Threading.Tasks;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Steps;

[Binding]
public class AssessmentServiceSteps(ScenarioContext context) : EPAOBaseSteps(context)
{
    private readonly ScenarioContext _context = context;
    private bool _permissionsSelected;
    private string _newUserEmailId;

    private AS_AssessmentRecordedPage assessmentRecordedPage;

    [Given(@"the (Assessor User|Delete Assessor User|Standard Apply User|Manage User|EPAO Withdrawal User) is logged into Assessment Service Application")]
    public async Task GivenTheUserIsLoggedIn(Func<Task<AS_LoggedInHomePage>> userloginFunc) => loggedInHomePage = await userloginFunc?.Invoke();

    [StepArgumentTransformation(@"(Assessor User|Delete Assessor User|Standard Apply User|Manage User|EPAO Withdrawal User)")]
    public Func<Task<AS_LoggedInHomePage>> GetProviderUserRole(string userRole)
    {
        return true switch
        {
            bool _ when userRole == "Assessor User" => () => ePAOHomePageHelper.LoginInAsNonApplyUser(_context.GetUser<EPAOAssessorUser>()),
            bool _ when userRole == "Delete Assessor User" => () => ePAOHomePageHelper.LoginInAsNonApplyUser(_context.GetUser<EPAODeleteAssessorUser>()),
            bool _ when userRole == "Manage User" => () => ePAOHomePageHelper.LoginInAsNonApplyUser(_context.GetUser<EPAOManageUser>()),
            bool _ when userRole == "Standard Apply User" => () => ePAOHomePageHelper.LoginInAsStandardApplyUser(_context.GetUser<EPAOStandardApplyUser>(), EPAOApplyStandardDataHelper.ApplyStandardCode, EPAOApplyStandardDataHelper.StandardAssessorOrganisationEpaoId),
            bool _ when userRole == "EPAO Withdrawal User" => () => ePAOHomePageHelper.LoginInAsNonApplyUser(_context.GetUser<EPAOWithdrawalUser>()),
            _ => null,
        };
    }

    [When(@"the User certifies an Apprentice as '(pass|fail)' using '(employer|apprentice)' route")]
    public async Task WhenTheUserCertifiesAnApprenticeAsWhoHasEnrolledForStandard(string grade, string route) => await RecordAGrade(grade, route, await SetLearnerDetails(), true);

    [When(@"the User provides the matching uln and invalid Family name for the existing certificate")]
    public async Task WhenTheUserProvidesTheMatchingUlnAndInvalidFamilyNameForTheExistingCertificate()
    {
        var page = await loggedInHomePage.GoToRecordAGradePage();
        
        await page.EnterApprenticeDetailsForExistingCertificateAndContinue();
    }

    [When(@"the User certifies same Apprentice as (pass|PassWithExcellence) using '(employer|apprentice)' route")]
    public async Task WhenTheUserCertifiesSameApprenticeAsPass(string grade, string route) => await RecordAGrade(grade, route, GetLearnerCriteria(), false);

    [Then(@"the Assessment is recorded as '(pass|fail|pass with excellence)'")]
    public async Task ThenTheAssessmentIsRecordedAs(string grade) => await assessmentServiceStepsHelper.VerifyApprenticeGrade(grade, GetLearnerCriteria());

    //[Then(@"the Admin user can delete a certificate that has been incorrectly submitted")]
    //public async Task ThenTheAdminUserCanDeleteACertificateThatHasBeenIncorrectlySubmitted()
    //{
    //    var page = await ePAOHomePageHelper.LoginToEpaoAdminHomePage(true);

    //    await assessmentServiceStepsHelper.DeleteCertificate(page);
    //}

    [Then(@"the User can navigates to record another grade")]
    public async Task ThenTheUserCanNavigatesToRecordAnotherGrade() => await assessmentRecordedPage.ClickRecordAnotherGradeLink();

    [Given(@"the User should be able to Opt In for the new version of the Standard")]
    public async Task GivenTheUserShouldBeAbleToOptInForTheNewVersionOfTheStandard()
    {
        var page = await loggedInHomePage.ApprovedStandardAndVersions();

        var page1 = await page.ClickOnAssociateProjectManagerLink();

        var page2 = await page1.ClickOnAssociateProjectManagerOptInLinkForVersion1_1();

        await page2.ConfirmOptIn();
    }

    [Then(@"'(.*)' message is displayed")]
    public async Task ThenErrorMessageIsDisplayed(string _) => await recordAGradePage.VerifyCantFindApprentice();

    [Then(@"the '(.*)' is displayed")]
    public async Task ThenErrorIsDisplayed(string errorMessage)
    {
        switch (errorMessage)
        {
            case "Family name and ULN missing error":
                await recordAGradePage.VerifyFamilyNameMissingErrorText();
                await recordAGradePage.VerifyULNMissingErrorText();
                break;
            case "Family name missing error":
                await recordAGradePage.VerifyFamilyNameMissingErrorText();
                break;
            case "ULN missing error":
                await recordAGradePage.VerifyULNMissingErrorText();
                break;
            case "ULN validation error":
                await recordAGradePage.VerifyInvalidUlnErrorText();
                break;
        }
    }

    [When(@"the User clicks on the continue button '(.*)'")]
    public async Task WhenTheUserClicksOnTheContinueButton(string scenario)
    {
        string familyName = ePAOAdminDataHelper.FamilyName, uln = ePAOAdminDataHelper.LearnerUln;

        switch (scenario)
        {
            case "with out entering Any details":
                familyName = string.Empty; uln = string.Empty;
                break;
            case "by entering valid Family name and blank ULN":
                uln = string.Empty;
                break;
            case "by entering blank Family name and Valid ULN":
                familyName = string.Empty;
                break;
            case "by entering valid Family name but ULN less than 10 digits":
                uln = EPAODataHelper.GetRandomNumber(9);
                break;
            case "by entering valid Family name and Invalid ULN":
                uln = EPAODataHelper.GetRandomNumber(11);
                break;
        }

        await recordAGradePage.EnterApprenticeDetailsAndContinue(familyName, uln);
    }

    [Given(@"navigates to Assessment page")]
    public async Task GivenNavigatesToAssessmentPage() { await SetLearnerDetails(); recordAGradePage = await loggedInHomePage.GoToRecordAGradePage(); }

    [Given(@"the User certifies an Apprentice as '(pass|fail)' with '(employer|apprentice)' route and records a grade")]
    public async Task WhenTheUserCertifiesAnApprenticeAndRecordsAGrade(string grade, string route)
    {
        var page = await CertifyApprentice(grade, route, await SetLearnerDetails(), true);
        
        await page.ClickContinueInCheckAndSubmitAssessmentPage();
    }

    [When(@"the User certifies an Apprentice as '(pass|fail)' with '(employer|apprentice)' route and lands on Confirm Assessment Page")]
    public async Task WhenTheUserCertifiesAnApprenticeAndLandsOnConfirmAssessmentPage(string grade, string route)
        => checkAndSubmitAssessmentPage = await CertifyApprentice(grade, route, await SetLearnerDetails(), true);

    [Then(@"the Change links navigate to the respective pages")]
    public async Task ThenTheChangeLinksNavigateToTheRespectivePages()
    {
        await CheckCommonLinks();

        var page = await checkAndSubmitAssessmentPage.ClickCertificateAddressChangeLinkvForApprenticeJourney();

        checkAndSubmitAssessmentPage = await page.ClickBackLink();
    }

    [Then(@"the Change links navigate to employer pages")]
    public async Task ThenTheChangeLinksNavigateToTheEmployerPages()
    {
        await CheckCommonLinks();

        var page = await checkAndSubmitAssessmentPage.ClickDepartmentChangeLinkForEmployerJourney();

        checkAndSubmitAssessmentPage = await page.ClickBackLink();

        var page1 = await checkAndSubmitAssessmentPage.ClickCertificateAddressChangeLinkForEmployerJourney();

        checkAndSubmitAssessmentPage = await page1.ClickBackLink();

    }

    [When(@"the User navigates to the Completed assessments tab")]
    public async Task WhenTheUserNavigatesToTheCompletedAssessmentsTab() => await loggedInHomePage.ClickCompletedAssessmentsLink();

    [Then(@"the User is able to view the history of the assessments")]
    public async Task ThenTheUserIsAbleToViewTheHistoryOfTheAssessments() => await new AS_CompletedAssessmentsPage(_context).VerifyTableHeaders();

    [When(@"the User navigates to Organisation details page")]
    public async Task WhenTheUserNavigatesToOrganisationDetailsPage()
    {
        await AssessmentServiceStepsHelper.RemoveChangeOrgDetailsPermissionForTheUser(loggedInHomePage);

        await loggedInHomePage.ClickOrganisationDetailsTopMenuLink();

        await new AS_ChangeOrganisationDetailsPage(_context).ClickAccessButton();
    }

    //[Then(@"the User is able to change the Registered details")]
    //public async Task ThenTheUserIsAbleToChangeTheRegisteredDetails()
    //{
    //    aS_OrganisationDetailsPage = new AS_OrganisationDetailsPage(_context);
    //    aS_OrganisationDetailsPage = await AssessmentServiceStepsHelper.ChangeContactName(aS_OrganisationDetailsPage);
    //    aS_OrganisationDetailsPage = await AssessmentServiceStepsHelper.ChangePhoneNumber(aS_OrganisationDetailsPage);
    //    aS_OrganisationDetailsPage = await AssessmentServiceStepsHelper.ChangeAddress(aS_OrganisationDetailsPage);
    //    aS_OrganisationDetailsPage = await AssessmentServiceStepsHelper.ChangeEmailAddress(aS_OrganisationDetailsPage);
    //    aS_OrganisationDetailsPage = await AssessmentServiceStepsHelper.ChangeWebsiteAddress(aS_OrganisationDetailsPage);
    //}

    [When(@"the User initiates editing permissions of another user")]
    public async Task WhenTheUserInitiatesEditingPermissionsOfAnotherUser()
    {
        var page = await loggedInHomePage.ClickManageUsersLink();

        var page1 = await page.ClickPermissionsEditUserLink();

        editUserPermissionsPage = await page1.ClickEditUserPermissionLink();


        _permissionsSelected = await editUserPermissionsPage.IsChangeOrganisationDetailsCheckBoxSelected();

        if (_permissionsSelected) await editUserPermissionsPage.UnSelectAllPermissionCheckBoxes();
        else await editUserPermissionsPage.SelectAllPermissionCheckBoxes();

        userDetailsPage = await editUserPermissionsPage.ClickSaveButton();

        _permissionsSelected = !_permissionsSelected;
    }

    [Then(@"the User is able to change the permissions")]
    public async Task ThenTheUserIsAbleToChangeThePermissions() 
    {
        var permissions = await userDetailsPage.GetDashboardPermissions();

        Assert.Multiple(() =>
        {
            Assert.AreEqual(true, permissions.Any(x => x.Contains("View dashboard")), "default 'View dashboard' " + AddAssertResultText(true));

            Assert.AreEqual(_permissionsSelected, permissions.Any(x => x.Contains("Change organisation details")), "'Change organisation details' " + AddAssertResultText(_permissionsSelected));

            Assert.AreEqual(_permissionsSelected, permissions.Any(x => x.Contains("Pipeline")), "'Pipeline' " + AddAssertResultText(_permissionsSelected));

            Assert.AreEqual(_permissionsSelected, permissions.Any(x => x.Contains("Completed assessments")), "'Completed assessments' " + AddAssertResultText(_permissionsSelected));

            Assert.AreEqual(_permissionsSelected, permissions.Any(x => x.Contains("Manage standards")), "'Manage standards' " + AddAssertResultText(_permissionsSelected));

            Assert.AreEqual(_permissionsSelected, permissions.Any(x => x.Contains("Manage users")), "'Manage users' " + AddAssertResultText(_permissionsSelected));

            Assert.AreEqual(_permissionsSelected, permissions.Any(x => x.Contains("Record grades and issue certificates")), "'Record grades and issue certificates' " + AddAssertResultText(_permissionsSelected));
        });
    }

        


    [When(@"the User initiates inviting a new user journey")]
    public async Task WhenTheUserInitiatesInvitingANewUserJourney() => _newUserEmailId = await AssessmentServiceStepsHelper.InviteAUser(loggedInHomePage);

    [Then(@"a new User is invited and able to initiate inviting another user")]
    public async Task ThenANewUserIsInvitedAndAbleToInitiateInvitingAnotherUser() => await new AS_UserInvitedPage(_context).ClickInviteSomeoneElseLink();

    [Then(@"the User can remove newly invited user")]
    public async Task ThenTheUserCanRemoveNewlyInvitedUser()
    {
        await loggedInHomePage.ClickHomeTopMenuLink();

        var page = await loggedInHomePage.ClickManageUsersLink();

        var page1 = await page.ClickOnNewlyAddedUserLink(_newUserEmailId);

        var page2 = await page1.ClickRemoveThisUserLinkInUserDetailPage();

        await page2.ClickRemoveUserButtonInRemoveUserPage();
    }

    //[Then(@"the user can apply to assess a standard")]
    //public async Task ThenTheUserCanApplyToAssessAStandard()
    //{
    //    var page = await loggedInHomePage.ApplyToAssessStandard();

    //    var page1 = await page.SelectApplication();

    //    var page2 = await page1.StartApplication();

    //    await applyStepsHelper.ApplyForAStandard(page2, EPAOApplyStandardDataHelper.ApplyStandardName);
    //}


    [Given(@"the certificate is printed")]
    public async Task GivenTheCertificateIsSentToPrinter() => await ePAOAdminSqlDataHelper.UpdateCertificateToPrinted(ePAOAdminDataHelper.LearnerUln);

    private async Task CheckCommonLinks()
    {
        var page = await checkAndSubmitAssessmentPage.ClickGradeChangeLink();

        checkAndSubmitAssessmentPage = await page.ClickBackLink();

        var page1 = await checkAndSubmitAssessmentPage.ClickOptionChangeLink();

        checkAndSubmitAssessmentPage = await page1.ClickBackLink();

        var page2 = await checkAndSubmitAssessmentPage.ClickAchievementDateChangeLink();

        checkAndSubmitAssessmentPage = await page2.ClickBackLink();

        var page3 = await checkAndSubmitAssessmentPage.ClickCertificateReceiverLink();

        checkAndSubmitAssessmentPage = await page3.ClickBackLink();
    }

    private async Task<AS_AssessmentRecordedPage> RecordAGrade(string grade, string route, LearnerCriteria learnerCriteria, bool deleteCertificate) 
    {
        var page = await CertifyApprentice(grade, route, learnerCriteria, deleteCertificate);

        return assessmentRecordedPage = await page.ClickContinueInCheckAndSubmitAssessmentPage();
    }
        

    private async Task<AS_CheckAndSubmitAssessmentPage> CertifyApprentice(string grade, string route, LearnerCriteria learnerCriteria, bool deleteExistingCertificate) =>
        await assessmentServiceStepsHelper.CertifyApprentice(grade, route, learnerCriteria, deleteExistingCertificate);

    private async Task<LearnerCriteria> SetLearnerDetails() => await SetLearnerDetails(async () => await ePAOAdminCASqlDataHelper.GetCATestData(ePAOAdminDataHelper.LoginEmailAddress, GetLearnerCriteria()));

    private async Task<LearnerCriteria> SetLearnerDetails(Func<Task<List<string>>> func)
    {
        var learnerDetails = await func();

        if (string.IsNullOrEmpty(learnerDetails[0])) Assert.Fail("No test data found in the db");

        ePAOAdminDataHelper.LearnerUln = learnerDetails[0];
        ePAOAdminDataHelper.StandardCode = learnerDetails[1];
        ePAOAdminDataHelper.StandardsName = learnerDetails[2];
        ePAOAdminDataHelper.GivenNames = learnerDetails[3];
        ePAOAdminDataHelper.FamilyName = learnerDetails[4];

        objectContext.SetLearnerDetails(learnerDetails[0], learnerDetails[1], learnerDetails[2], learnerDetails[3], learnerDetails[4]);

        return GetLearnerCriteria();
    }

    private LearnerCriteria GetLearnerCriteria() => _context.Get<LearnerCriteria>();

    private static string AddAssertResultText(bool condition) => condition ? "permission selected is not shown in 'User details' page" : "permission selected is shown in 'User details' page";
}
