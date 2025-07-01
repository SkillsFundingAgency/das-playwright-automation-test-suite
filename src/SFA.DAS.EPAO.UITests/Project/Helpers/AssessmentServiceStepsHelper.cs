using SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService.OrganisationDetails;

namespace SFA.DAS.EPAO.UITests.Project.Helpers;

public class AssessmentServiceStepsHelper(ScenarioContext context)
{
    private readonly EPAOAdminDataHelper _ePAOAdminDataHelper = context.Get<EPAOAdminDataHelper>();

    public async Task VerifyApprenticeGrade(string grade, LearnerCriteria learnerCriteria)
    {
        var recordAGradePage = await GoToRecordAGradePage();

        if (learnerCriteria.HasMultiStandards)
        {
            var page = await recordAGradePage.SearchApprentice(false);

            var page1 = await page.ViewCertificateHistory();

            await page1.VerifyGrade(grade);
        }
            
        else
        {
            if (grade.ContainsCompareCaseInsensitive("pass"))
            {
                var page = await recordAGradePage.GoToAssesmentAlreadyRecordedPage();

                await page.VerifyGrade(grade);
            }

            else 
            {
                var page = await recordAGradePage.SearchApprentice(false);

                await page.VerifyGrade(grade);
            }
        }
    }

    public async Task<AS_CheckAndSubmitAssessmentPage> CertifyApprentice(string grade, string route, LearnerCriteria learnerCriteria, bool deleteCertificate)
    {
        var page = await GoToRecordAGradePage();

        var page1 = await page.SearchApprentice(deleteCertificate);

        AS_DeclarationPage decPage = await CertifyApprentice(page1, learnerCriteria);

        return await SelectGrade(decPage, grade, route);
    }

    public static async Task RemoveChangeOrgDetailsPermissionForTheUser(AS_LoggedInHomePage loggedInHomePage)
    {
        var page = await loggedInHomePage.ClickManageUsersLink();

        var page1 = await page.ClickManageUserNameLink();

        var page2 = await page1.ClickEditUserPermissionLink();

        await page2.UnSelectChangeOrganisationDetailsCheckBox();

        await page2.ClickSaveButton();
    }

    public static async Task<AS_OrganisationDetailsPage> ChangeContactName(AS_OrganisationDetailsPage organisationDetailsPage)
    {
        var page = await organisationDetailsPage.ClickContactNameChangeLink();

        var page1 = await page.SelectContactNameRadioButtonAndClickSave();

        var page2 = await page1.ClickConfirmButtonInConfirmContactNamePage();

        return await page2.ClickViewOrganisationDetailsLink();
    }

    public static async Task<AS_OrganisationDetailsPage> ChangePhoneNumber(AS_OrganisationDetailsPage organisationDetailsPage)
    {
        var page = await organisationDetailsPage.ClickPhoneNumberChangeLink();

        var page1 = await page.EnterRandomPhoneNumberAndClickUpdate();

        var page2 = await page1.ClickConfirmButtonInConfirmPhoneNumberPage();

        return await page2.ClickViewOrganisationDetailsLink();
    }

    public static async Task<AS_OrganisationDetailsPage> ChangeAddress(AS_OrganisationDetailsPage organisationDetailsPage)
    {
        var page = await organisationDetailsPage.ClickAddressChangeLink();

        var page1 = await page.ClickSearchForANewAddressLink();

        var page2 = await page1.ClickEnterTheAddressManuallyLink();

        var page3 = await page2.EnterEmployerAddressAndClickChangeAddressButton();

        var page4 = await page3.ClickConfirmAddressButtonInConfirmContactAddressPage();

        return await page4.ClickViewOrganisationDetailsLink();
    }

    public static async Task<AS_OrganisationDetailsPage> ChangeEmailAddress(AS_OrganisationDetailsPage organisationDetailsPage)
    {
        var page = await organisationDetailsPage.ClickEmailChangeLink();

        var page1 = await page.EnterRandomEmailAndClickChange();

        var page2 = await page1.ClickConfirmButtonInConfirmEmailAddressPage();

        return await page2.ClickViewOrganisationDetailsLink();
    }

    public static async Task<AS_OrganisationDetailsPage> ChangeWebsiteAddress(AS_OrganisationDetailsPage organisationDetailsPage)
    {
        var page = await organisationDetailsPage.ClickWebsiteChangeLink();

        var page1 = await page.EnterRandomWebsiteAddressAndClickUpdate();

        var page2 = await page1.ClickConfirmButtonInConfirmWebsiteAddressPage();

        return await page2.ClickViewOrganisationDetailsLink();
    }

    public static async Task<string> InviteAUser(AS_LoggedInHomePage _loggedInHomePage)
    {
        var page = await _loggedInHomePage.ClickManageUsersLink();

        var page1 = await page.ClickInviteNewUserButton();

        return await page1.EnterUserDetailsAndSendInvite();
    }

    public async Task DeleteCertificate(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.Search();

        var page1 = await page.SearchFor(_ePAOAdminDataHelper.LearnerUln);

        var page2 = await page1.SelectACertificate();

        var page3 = await page2.ClickDeleteCertificateLink();

        var page4 = await page3.ClickYesAndContinue();

        var page5 = await page4.EnterAuditDetails();

        var page6 = await page5.ClickDeleteCertificateButton();

        await page6.ClickReturnToDashboard();
    }

    private async Task<AS_RecordAGradePage> GoToRecordAGradePage() => await new AS_LoggedInHomePage(context).GoToRecordAGradePage();

    private async Task<AS_CheckAndSubmitAssessmentPage> SelectGrade(AS_DeclarationPage decpage, string grade, string route)
    {
        await decpage.ClickConfirmInDeclarationPage();

        await new AS_WhatGradePage(context).SelectGradeAndEnterDate(grade);

        if (route == "employer") return await SelectGradeEmployerRoute(grade);

        else return await SelectGradeApprenticeRoute(grade);
    }

    private async Task<AS_CheckAndSubmitAssessmentPage> SelectGradeApprenticeRoute(string grade)
    {
        if (grade == "pass")
        {
            var page = await new AS_WhoWouldYouLikeUsToSendTheCertificateToPage(context)
            .ClickAprenticeRadioButton();

            var page1 = await page.ClickEnterAddressManuallyLinkInSearchEmployerPage();

            var page2 = await page1.EnterEmployerAddressAndContinue();

            return await page2.ClickContinueInConfirmEmployerAddressPage();
        }
        else if (grade == "PassWithExcellence") await PassWithExcellence();

        return new AS_CheckAndSubmitAssessmentPage(context);
    }

    private async Task<AS_CheckAndSubmitAssessmentPage> SelectGradeEmployerRoute(string grade)
    {
        if (grade == "pass")
        {
            var page = await new AS_WhoWouldYouLikeUsToSendTheCertificateToPage(context).ClickEmployerRadioButton();

            var page1 = await page.ClickEnterAddressManuallyLinkInSearchEmployerPage();

            var page2 = await page1.EnterEmployerNameAndAddressAndContinue();

            var page3 = await page2.AddRecipientAndContinue();

            return await page3.ClickContinueInConfirmEmployerAddressPage();

        }
        else if (grade == "PassWithExcellence") await PassWithExcellence();

        return new AS_CheckAndSubmitAssessmentPage(context);
    }

    private async Task<AS_CheckAndSubmitAssessmentPage> PassWithExcellence() => await new AS_ConfirmAddressPage(context).ClickContinueInConfirmEmployerAddressPage();

    private static async Task<AS_DeclarationPage> CertifyApprentice(AS_ConfirmApprenticePage confirmApprenticePage, LearnerCriteria learnerCriteria)
    {
        if (learnerCriteria.HasMultipleVersions)
        {
            if (learnerCriteria.VersionConfirmed)
                return await confirmApprenticePage.ConfirmAndContinue();

            else
            {
                var whichVersionPage = await confirmApprenticePage.GoToWhichVersionPage(learnerCriteria.HasMultiStandards);

                if (learnerCriteria.WithOptions)
                {
                    var page = await whichVersionPage.ClickConfirmInConfirmVersionPage();

                    return await page.SelectLearningOptionAndContinue();
                }

                else return await whichVersionPage.ClickConfirmInConfirmVersionPageNoOption();
            }
        }
        else
        {
            if (learnerCriteria.WithOptions) 
            {
                var page = await confirmApprenticePage.GoToWhichLearningOptionPage(learnerCriteria.HasMultiStandards);

                return await page.SelectLearningOptionAndContinue(); 
            }

            else return await confirmApprenticePage.GoToDeclarationPage(learnerCriteria.HasMultiStandards);
        }
    }
}
