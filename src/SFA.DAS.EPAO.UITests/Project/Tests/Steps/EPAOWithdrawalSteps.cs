using SFA.DAS.EPAO.UITests.Project.Helpers;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Steps
{
    [Binding]
    public class EPAOWithdrawalSteps(ScenarioContext context) : EPAOBaseSteps(context)
    {
        private readonly EPAOWithdrawalHelper _ePAOWithdrawalHelper = new(context);

        [When(@"starts the journey to withdraw a standard")]
        [Given(@"starts the journey to withdraw a standard")]
        public async Task GivenStartsTheJourneyToWithdrawAStandard() => await _ePAOWithdrawalHelper.StartOfStandardWithdrawalJourney();

        [When(@"completes the standard withdrawal notification questions")]
        public async Task WhenCompletesTheStandardWithdrawalNotificationQuestions() => await _ePAOWithdrawalHelper.StandardApplicationFinalJourney();

        [Then(@"application is submitted for review")]
        public async Task ThenApplicationIsSubmittedForReview() => await _ePAOWithdrawalHelper.VerifyStandardSubmitted();

        [Then(@"user verifies the different statuses of the standard withdrawl application")]
        public async Task GivenUserVerifiesTheDifferentStatusesOfTheStandardWithdrawlApplication() => await _ePAOWithdrawalHelper.VerifyTheInProgressStatus();

        [Then(@"user verifies view links navigate to the appropriate corresponding page")]
        public async Task GivenUserVerifiesViewLinksNavigateToTheAppropriateCorrespondingPage() => await _ePAOWithdrawalHelper.VerifyInProgressViewLinkNavigatesToApplicationOverviewPage();

        [Then(@"the admin user logs in to approve the standard withdrawal application")]
        public async Task ThenTheAdminUserLogsInToApproveTheStandardWithdrawalApplication() => await EPAOWithdrawalHelper.ApproveAStandardWithdrawal(await ePAOHomePageHelper.LoginToEpaoAdminHomePage(false));

        [Then(@"the admin user logs in to approve the register withdrawal application")]
        public async Task ThenTheAdminUserLogsInToApproveTheRegisterWithdrawalApplication() => await EPAOWithdrawalHelper.ApproveARegisterWithdrawal(await ePAOHomePageHelper.LoginToEpaoAdminHomePage(true));

        [When(@"starts the journey to withdraw from the register")]
        [Given(@"starts the journey to withdraw from the register")]
        public async Task GivenStartsTheJourneyToWithdrawFromTheRegister() => await _ePAOWithdrawalHelper.StartOfRegisterWithdrawalJourney();

        [When(@"completes the Register withdrawal notification questions")]
        [Given(@"completes the Register withdrawal notification questions")]
        public async Task CompletesTheRegisterWithdrawalNotificationQuestions() => await _ePAOWithdrawalHelper.RegisterWithdrawalQuestions();

        [Then(@"the admin user returns to view withdrawal notifications table")]
        public async Task ReturnToWithdrawalNotificationsPage() => await _ePAOWithdrawalHelper.ReturnToWithdrawalApplicationsPage();

        [Then(@"the admin user logs in and adds feedback to an application")]
        public async Task ThenTheAdminUserAddsFeedbackToAnApplication() => await EPAOWithdrawalHelper.AddFeedbackToARegisterWithdrawalApplication(await ePAOHomePageHelper.LoginToEpaoAdminHomePage(true));

        [Then(@"verify application has moved from new to feedback tab")]
        public async Task VerifyApplicationMovedFromNewToFeedbackTab() => await _ePAOWithdrawalHelper.VerifyApplicationMovedFromNewToFeedback();

        [Then(@"Verify the application is moved to Approved tab")]
        public async Task VerifyApplicationIsMovedToApprovedTab() => await _ePAOWithdrawalHelper.VerifyApplicationIsMovedToApprovedTab();

        [Then(@"the withdrawal user returns to dashboard")]
        [Then(@"the assessor user returns to dashboard")]
        public async Task TheWithdrawalUserReturnsToDashboard()
        {
            var page = await ePAOHomePageHelper.GoToEpaoAssessmentLandingPage(true);
            
            await page.AlreadyLoginGoToLoggedInHomePage();
        }

        [Then(@"the withdrawal user reviews and ammends their application")]
        public async Task AmmendWithdrawalApplication() => await _ePAOWithdrawalHelper.AmmendWithdrawalApplication();

        [Given(@"the admin user returns and reviews the ammended withdrawal notification")]
        public async Task TheAdminUserReturnsAndReviewsTheAmmendedWithdrawalNotification() => await EPAOWithdrawalHelper.ApproveAmmendedRegisterWithdrawal(await ePAOHomePageHelper.LoginToEpaoAdminHomePage(true));

        [Then(@"verify withdrawal from register approved and return to withdrawal applications")]
        public async Task VerifyWithdrawalFromRegisterApproved() => await _ePAOWithdrawalHelper.VerifyWithdrawalFromRegisterApproved();
    }
}
