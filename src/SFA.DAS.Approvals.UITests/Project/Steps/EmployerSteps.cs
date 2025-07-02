using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
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
            await employerStepsHelper.EmployerLogInToEmployerPortal();

            await new InterimApprenticesHomePage(context, false).VerifyPage();

            var page = await new ApprenticesHomePage(context).GoToApprenticeRequests();

            await employerStepsHelper.ApproveCohort(page);           

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
                TimeSpan.FromMinutes(4)
            );
        }

        

    }

}
