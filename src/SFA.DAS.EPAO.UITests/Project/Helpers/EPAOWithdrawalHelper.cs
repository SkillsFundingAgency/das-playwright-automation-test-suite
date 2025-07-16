using SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.EPAOWithdrawalPages;

namespace SFA.DAS.EPAO.UITests.Project.Helpers;

public class EPAOWithdrawalHelper(ScenarioContext context)
{
    private readonly EPAOApplySqlDataHelper _ePAOSqlDataHelper = context.Get<EPAOApplySqlDataHelper>();

    public async Task StartOfStandardWithdrawalJourney()
    {
        AS_LoggedInHomePage aS_LoggedInHomePage = new(context);

        if (await _ePAOSqlDataHelper.HasWithdrawals(context.GetUser<EPAOWithdrawalUser>().Username))
        {
            var page = await aS_LoggedInHomePage.ClickWithdrawFromAStandardLink();

            var page1 = await page.ClickContinueOnWithdrawFromStandardsPageWhenWithdrawalsExist();

            var page2 = await page1.ClickStartNewWithdrawalNotification();

            var page3 = await page2.ClickAssessingASpecificStandard();

            var page4 = await page3.ClickASpecificStandardToWithdraw();

            await page4.ContinueWithWithdrawalRequest();
        }
        else
        {

            var page = await aS_LoggedInHomePage.ClickWithdrawFromAStandardLink();

            var page1 = await page.ClickContinueOnWithdrawFromStandardsPageWhenNoWithdrawalsExist();

            var page2 = await page1.ClickAssessingASpecificStandard();

            var page3 = await page2.ClickASpecificStandardToWithdraw();

            await page3.ContinueWithWithdrawalRequest();
        }
    }

    public async Task StartOfRegisterWithdrawalJourney()
    {
        AS_LoggedInHomePage aS_LoggedInHomePage = new(context);

        if (await _ePAOSqlDataHelper.HasWithdrawals(context.GetUser<EPAOWithdrawalUser>().Username))
        {
            var page = await aS_LoggedInHomePage.ClickWithdrawFromTheRegisterLink();

            var page1 = await page.ClickContinueOnWithdrawFromStandardsPageWhenWithdrawalsExist();

            var page2 = await page1.ClickStartNewWithdrawalNotification();

            var page3 = await page2.ClickWithdrawFromRegister();

            await page3.ContinueWithWithdrawalRequest();
        }
        else
        {
            var page = await aS_LoggedInHomePage.ClickWithdrawFromTheRegisterLink();

            var page1 = await page.ClickContinueOnWithdrawFromStandardsPageWhenNoWithdrawalsExist();

            var page2 = await page1.ClickWithdrawFromRegister();

            await page2.ContinueWithWithdrawalRequest();
        }
    }

    public async Task StandardApplicationFinalJourney()
    {
        var page = await new AS_WithdrawalRequestOverviewPage(context).ClickGoToStandardWithdrawalQuestions();

        var page1 = await page.ClickGoToReasonForWithdrawingQuestionLink();

        var page2 = await page1.ClickExternalQualityAssuranceProviderHasChanged();

        var page3 = await page2.ClickYesAndContinue();

        var page4 = await page3.EnterSupportingInformationForStandardWithdrawal();

        var page5 = await page4.EnterDateToWithdraw();

        var page6 = await page5.VerifyAndReturnToApplicationOverviewPage();

        await page6.AcceptAndSubmit();
    }

    public async Task RegisterWithdrawalQuestions()
    {
        var page = await new AS_WithdrawalRequestOverviewPage(context).ClickGoToRegisterWithdrawalQuestions();

        var page1 = await page.ClickGoToReasonForWithdrawingFromRegisterQuestionLink();

        var page2 = await page1.ClickAssessmentPlanHasChangedAndEnterOptionalReason();

        var page3 = await page2.ClickNoAndContinue();

        var page4 = await page3.EnterAnswerForHowWillYouSupportLearnerYouAreNotGoingToAssess();

        var page5 = await page4.EnterSupportingInformationForRegisterWithdrawal();

        var page6 = await page5.EnterDateToWithdraw();

        var page7 = await page6.VerifyWithSupportingLearnersQuestionAndReturnToApplicationOverviewPage();

        await page7.AcceptAndSubmitWithHowWillYouSuportQuestion();
    }

    public async Task VerifyStandardSubmitted() => await VerifyPageHelper.VerifyPageAsync(() => new AS_WithdrawalApplicationSubmittedPage(context));

    public async Task VerifyTheInProgressStatus()
    {
        var page = await new AS_LoggedInHomePage(context).ClickWithdrawFromAStandardLink();

        var page1 = await page.ClickContinueOnWithdrawFromStandardsPageWhenWithdrawalsExist();

        await page1.ValidateStatus("In progress");
    }

    public async Task VerifyInProgressViewLinkNavigatesToApplicationOverviewPage()
    {
        await new AS_YourWithdrawalRequestsPage(context).ClickOnViewLinkForInProgressApplication();
    }

    public static async Task<AD_YouhaveApprovedThisWithdrawalNotification> ApproveAStandardWithdrawal(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.GoToNewWithdrawalApplications();

        var page1 = await page.GoToStandardWithdrawlApplicationOverivewPage();

        var page2 = await page1.GoToWithdrawalRequestQuestionsPage();

        var page3 = await page2.MarkCompleteAndGoToWithdrawalApplicationOverviewPage();

        var page4 = await page3.ClickCompleteReview();

        var page5 = await page4.ContinueWithWithdrawalRequest();

        return await page5.ClickApproveApplication();

    }

    public static async Task<AD_YouhaveApprovedThisWithdrawalNotification> ApproveARegisterWithdrawal(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.GoToNewWithdrawalApplications();

        var page1 = await page.GoToRegisterWithdrawlApplicationOverviewPage();

        var page2 = await page1.GoToWithdrawalRequestQuestionsPage();

        var page3 = await page2.MarkCompleteAndGoToWithdrawalApplicationOverviewPage();

        var page4 = await page3.ClickCompleteReview();

        var page5 = await page4.ContinueWithWithdrawalRequest();

        return await page5.ClickApproveApplication();
    }

    public static async Task<AD_WithdrawalApplicationsPage> AddFeedbackToARegisterWithdrawalApplication(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.GoToNewWithdrawalApplications();

        var page1 = await page.GoToRegisterWithdrawlApplicationOverviewPage();

        var page2 = await page1.GoToWithdrawalRequestQuestionsPage();

        var page3 = await page2.ClickAddFeedbackToHowWillYouSupportLearnersQuestion();

        var page4 = await page3.AddFeedbackMessage();

        var page5 = await page4.MarkCompleteAndGoToWithdrawalApplicationOverviewPage();

        var page6 = await page5.ClickCompleteReview();

        var page7 = await page6.ContinueWithWithdrawalRequest();

        var page8 = await page7.ClickAddFeedback();

        return await page8.ReturnToWithdrawalApplications();
    }
    public async Task ReturnToWithdrawalApplicationsPage()
    {
        await new AD_YouhaveApprovedThisWithdrawalNotification(context).ReturnToWithdrawalApplications();
    }

    public async Task VerifyApplicationMovedFromNewToFeedback() => await new AD_WithdrawalApplicationsPage(context).VerifyAnApplicationAddedToFeedbackTab();

    public async Task VerifyApplicationIsMovedToApprovedTab() => await new AD_WithdrawalApplicationsPage(context).VerifyApprovedTabContainsRegisterWithdrawal();


    public async Task AmmendWithdrawalApplication()
    {
        var page = await new AS_LoggedInHomePage(context).ClickWithdrawFromTheRegisterLink();

        var page1 = await page.ClickContinueOnWithdrawFromStandardsPageWhenWithdrawalsExist();

        var page2 = await page1.ClickViewOnRegisterWithdrawalWithFeedbackAdded();

        var page3 = await page2.ClickContinueButton();

        var page4 = await page3.ClickSupportingCurrentLearnersFeedback();

        var page5 = await page4.UpdateAnswerForHowWillYouSupportLearnersYouAreNotGoingToAssess();

        await page5.SubmitUpdatedAnswers();
    }

    public static async Task<AD_YouhaveApprovedThisWithdrawalNotification> ApproveAmmendedRegisterWithdrawal(StaffDashboardPage staffDashboardPage)
    {
        var page = await staffDashboardPage.GoToFeedbackWithdrawalApplications();

        var page1 = await page.GoToAmmendedWithdrawalApplicationOverviewPage();

        await page1.VerifyAnswerUpdatedTag();

        var page2 = await page1.GoToWithdrawalRequestQuestionsPage();

        var page3 = await page2.MarkCompleteAndGoToWithdrawalApplicationOverviewPage();

        var page4 = await page3.ClickCompleteReview();

        var page5 = await page4.ContinueWithWithdrawalRequest();

        return await page5.ClickApproveApplication();
    }

    public async Task<AD_WithdrawalApplicationsPage> VerifyWithdrawalFromRegisterApproved()
    {
        var approvedPage = new AD_YouhaveApprovedThisWithdrawalNotification(context);

        await approvedPage.VerifyRegisterWithdrawalBodyText();

        return await approvedPage.ReturnToWithdrawalApplications();
    }
}
