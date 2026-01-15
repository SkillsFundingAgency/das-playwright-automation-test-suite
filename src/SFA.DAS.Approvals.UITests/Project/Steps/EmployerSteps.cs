using Microsoft.VisualBasic;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class EmployerSteps
    {
        protected readonly ScenarioContext context;
        private readonly EmployerStepsHelper employerStepsHelper;
        private readonly CommonStepsHelper commonStepsHelper;
        private readonly CommitmentsDbSqlHelper commitmentsDbSqlHelper;


        public EmployerSteps(ScenarioContext context)
        {
            this.context = context;
            employerStepsHelper = new EmployerStepsHelper(context);
            commonStepsHelper = new CommonStepsHelper(context);
            commitmentsDbSqlHelper = context.Get<CommitmentsDbSqlHelper>();
        }


        [When(@"the Employer approves the apprentice request \(cohort\)")]
        [Then("the Employer can approve the cohort")]
        public async Task WhenTheEmployerApprovesTheApprenticeRequestCohort()
        {
            var page = await employerStepsHelper.OpenCohort();
            await page.EmployerApproveCohort();
        }


        [When("Employer does not take any action on that cohort for more than 2 weeks")]
        public async Task WhenEmployerDoesNotTakeAnyActionOnThatCohortForMoreThan2Weeks()
        {
            var cohortRef = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;
            await commitmentsDbSqlHelper.UpdateCohortLastUpdatedDate(cohortRef, DateAndTime.Now.AddDays(-15));

            //test environments are configured to run web job: "ExpireInactiveCohortsWithEmployerAfter2WeeksSchedule" every 4th minute
            await commonStepsHelper.WaitForStatusUpdateAsync(
                getStatusFunc: async () =>
                {
                    return await context.Get<CommitmentsDbSqlHelper>().GetWithPartyValueFromCommitmentsDb(cohortRef);
                },
                "2",
                TimeSpan.FromMinutes(5)
            );
        }


        [Then("the Employer can access live apprentice records under Manager Your Apprentices section")]
        public async Task ThenTheEmployerCanAccessLiveApprenticeRecordsUnderManagerYourApprenticesSection()
        {
            await employerStepsHelper.CheckApprenticeOnManageYourApprenticesPage();
        }


        [When("Employer reviews the above cohort")]
        public async Task WhenEmployerReviewsTheAboveCohort()
        {
            await employerStepsHelper.OpenCohort();
        }


        [Then("display the warning message for foundation courses")]
        public async Task ThenDisplayTheWarningMessageForFoundationCourses()
        {
            var page = new EmployerApproveApprenticeDetailsPage(context);
            var warningMsg = "! Warning One or more of your apprenticeships have age eligibility criteria. Check the date of birth is correct or go to the funding rules to check who is eligible.";
            
            await page.ValidateWarningMessageForFoundationCourses(warningMsg);
            
            await page.EmployerApproveCohort();
        }


        [When("Employer tries to edit live apprentice record by setting age old than 24 years")]
        public async Task WhenEmployerTriesToEditLiveApprenticeRecordBySettingAgeOldThan24Years()
        {
            await employerStepsHelper.EmployerLogInToEmployerPortal();
            await new InterimApprenticesHomePage(context, false).VerifyPage();

        }


        [Then("the employer is stopped with an error message")]
        public async Task ThenTheEmployerIsStoppedWithAnErrorMessage()
        {
            var apprentice = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var uln = apprentice.ApprenticeDetails.ULN.ToString();
            var name = apprentice.ApprenticeDetails.FullName;
            var DoB = apprentice.ApprenticeDetails.DateOfBirth.AddYears(-10);

            var apprenticeDetailsPage = await employerStepsHelper.EmployerSearchOpenApprovedApprenticeRecord(new ApprenticesHomePage(context), uln, name);
            await employerStepsHelper.TryEditApprenticeAgeAndValidateError(apprenticeDetailsPage, DoB);
        }


        [Given(@"Employer create and send an empty cohort to the training provider to add learner details")]
        [When(@"the employer create and send an empty cohort to the training provider to add learner details")]
        public async Task GivenEmployerCreatesEmptyRequestCohort()
        {
            await employerStepsHelper.AddEmptyCohort();
        }    


        [Then ("the Employer sees the cohort in Ready to review with status of (.*)")]
        public async Task ThenTheEmployerReviewsCohort(string status)
        {
             await employerStepsHelper.ReadyForReviewCohort(status);
        }


        [When ("the Employer approves the cohort and sends to provider")]
        public async Task ThenTheEmployerApprovesCohort()
        {
            var page = new EmployerApproveApprenticeDetailsPage(context);
            await page.EmployerApproveCohort();
        }


        [Then ("verify RPL details")]
        public async Task ThenTheEmployerVerifyRPLDetails()
        {
           var apprenticeships = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
           var page = new EmployerApproveApprenticeDetailsPage(context);
           await page.VerifyRPLDetails(apprenticeships);
        }

        
        [Given("the Employer logins using an existing NonLevy Account which has reached it max reservations limit")]
        public async Task GivenTheEmployerLoginsUsingAnExistingNonLevyAccountWhichHasReachedItMaxReservationsLimit()
        {
            var listOfApprenticeship = new List<Apprenticeship>();
            Apprenticeship apprenticeship = await new ApprenticeDataHelper(context).CreateEmptyCohortObject(EmployerType.NonLevyUserAtMaxReservationLimit);
            apprenticeship = await new DbSteps(context).FindUnapprovedCohortReference(apprenticeship, ApprenticeRequests.WithEmployers);
            listOfApprenticeship.Add(apprenticeship);
            context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);           
        }

        
        [When("the Employer tries to add another apprentice to an existing cohort")]
        public async Task WhenTheEmployerTriesToAddAnotherApprenticeToAnExistingCohort()
        {
            await employerStepsHelper.OpenCohort(false);
            
        }

        
        [Then("the Employer is blocked with a shutter page for existing cohort")]
        public async Task ThenTheEmployerIsBlockedWithAShutterPageForExistingCohort()
        {
            await new EmployerApproveApprenticeDetailsPage(context).TryClickAddAnotherApprenticeLink();
        }


        [Then("the employer is blocked to create new reservations")]
        public async Task ThenTheEmployerIsBlockedToCreateNewReservations()
        {
            await employerStepsHelper.EmployerTriesToCreateReservation();
        }


        [Then("the apprenticeship is marked as Completed")]
        public async Task ThenTheApprenticeshipIsMarkedAsCompleted()
        {
            var apprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var page = await employerStepsHelper.CheckApprenticeOnManageYourApprenticesPage(true);
            var page1 = await page.OpenFirstItemFromTheList(apprenticeship.ApprenticeDetails.FullName);
            await page1.EmployerVerifyApprenticeStatus(ApprenticeshipStatus.Completed, "Completion payment month", DateTime.Now);
            await page1.AssertRecordIsReadOnlyExceptEndDate();
        }


    }

}
