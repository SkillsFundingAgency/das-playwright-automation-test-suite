using Microsoft.VisualBasic;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var warningMsg = "! Warning Check apprentices are eligible for foundation apprenticeships If someone is aged between 22 and 24, to be funded for a foundation apprenticeship they must either: have an Education, Health and Care (EHC) plan be or have been in the care of their local authority be a prisoner or have been in prison";
            
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
            Apprenticeship apprenticeship = await new ApprenticeDataHelper(context).CreateEmptyCohortAsync(EmployerType.NonLevyUserAtMaxReservationLimit);
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

        [When("the Employer tries to create reservation")]
        public async Task WhenTheEmployerTriesToCreateReservation()
        {
            await employerStepsHelper.EmployerNavigateToReservationsPage();
        }

        [Then("the Employer is blocked with a shutter page")]
        public async Task ThenTheEmployerIsBlockedWithAShutterPage()
        {
            var page = new YourFundingReservationsPage(context);
            await page.TryClickOnReserveMoreFundingLink();
        }




    }

}
