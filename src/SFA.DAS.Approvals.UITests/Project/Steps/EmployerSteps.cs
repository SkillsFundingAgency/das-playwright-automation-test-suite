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

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class EmployerSteps
    {
        protected readonly ScenarioContext context;

        protected readonly ObjectContext objectContext;

        protected readonly EmployerPortalLoginHelper employerLoginHelper;

        protected readonly EmployerHomePageStepsHelper employerHomePageHelper;

        public EmployerSteps(ScenarioContext context)
        {
            this.context = context;
            this.objectContext = context.Get<ObjectContext>();
            this.employerLoginHelper = new EmployerPortalLoginHelper(context);
            this.employerHomePageHelper = new EmployerHomePageStepsHelper(context);
        }

        [When("Employer approves the cohort")]
        public async Task WhenEmployerApprovesTheCohort()
        {
            await EmployerLogInToEmployerPortal();

            await new InterimApprenticesHomePage(context, false).VerifyPage();

            var page = await new ApprenticesHomePage(context).GoToApprenticeRequests();

            var page1 = await page.OpenApprenticeRequestReadyForReview();


        }


        private async Task<HomePage> EmployerLogInToEmployerPortal()
        {
            await employerHomePageHelper.NavigateToEmployerApprenticeshipService(true);

            var employerType = context.GetValue<Apprenticeship>().EmployerDetails.EmployerType.ToString();

            switch (employerType.ToLower())
            {
                case "levy":                    
                    await employerLoginHelper.Login(context.GetUser<LevyUser>());
                    break;
                case "nonlevy":
                    await employerLoginHelper.Login(context.GetUser<NonLevyUser>());
                    break;
                case "nonlevyuseratmaxreservationlimit":
                    //await employerLoginHelper.Login(context.GetUser<NonLevyUserAtMaxReservationLimit>());
                    break;
                default:
                    throw new ArgumentException($"Unknown employer type: {employerType}");
            }

            return new HomePage(context);
        }


    }
   
}
