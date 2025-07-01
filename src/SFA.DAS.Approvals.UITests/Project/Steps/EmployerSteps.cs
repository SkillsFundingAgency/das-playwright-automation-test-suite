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

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class EmployerSteps
    {
        protected readonly ScenarioContext context;
        private readonly EmployerStepsHelper employerStepsHelper;


        public EmployerSteps(ScenarioContext context)
        {
            this.context = context;
            employerStepsHelper = new EmployerStepsHelper(context);
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
        public void WhenEmployerDoesNotTakeAnyActionOnThatCohortForMoreThan2Weeks()
        {
            //throw new PendingStepException();
        }



    }

}
