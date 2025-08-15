using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.Framework;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using Microsoft.VisualBasic;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;

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

        [When(@"Employer approves the apprentice request \(cohort\)")]
        public async Task WhenEmployerApprovesTheApprenticeRequestCohort()
        {
            var page = await employerStepsHelper.OpenCohort();

            await page.EmployerApproveCohort();

        }

        [When("Employer does not take any action on that cohort for more than 2 weeks")]
        public async Task WhenEmployerDoesNotTakeAnyActionOnThatCohortForMoreThan2Weeks()
        {
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;
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


        [Then("Employer can access live apprentice records under Manager Your Apprentices section")]
        public async Task ThenEmployerCanAccessLiveApprenticeRecordsUnderManagerYourApprenticesSection()
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
            var apprentice = context.GetValue<List<Apprenticeship>>().FirstOrDefault();
            var uln = apprentice.ApprenticeDetails.ULN.ToString();
            var name = apprentice.ApprenticeDetails.FullName;
            var DoB = apprentice.ApprenticeDetails.DateOfBirth.AddYears(-10);

            var apprenticeDetailsPage = await employerStepsHelper.EmployerSearchOpenApprovedApprenticeRecord(new ApprenticesHomePage(context), uln, name);
            await employerStepsHelper.TryEditApprenticeAgeAndValidateErrors(apprenticeDetailsPage, DoB);
        }





    }

}
