using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    internal class EmployerStepsHelper
    {
        private readonly ScenarioContext context;
        protected readonly EmployerPortalLoginHelper employerLoginHelper;
        protected readonly EmployerHomePageStepsHelper employerHomePageHelper;
        private List<Apprenticeship> listOfApprenticeship;

        public EmployerStepsHelper(ScenarioContext _context)
        {
            context = _context;
            employerLoginHelper = new EmployerPortalLoginHelper(context);
            employerHomePageHelper = new EmployerHomePageStepsHelper(context);
            listOfApprenticeship = _context.GetValue<List<Apprenticeship>>();
        }

        internal async Task ApproveCohort(ApprenticeRequestsPage apprenticeRequestsPage)
        {
            var apprenticeship = listOfApprenticeship.FirstOrDefault();

            var page = await apprenticeRequestsPage.OpenApprenticeRequestReadyForReview(apprenticeship.CohortReference);

            await page.VerifyCohort(apprenticeship);

            var page1 = await page.EmployerApproveCohort();
        }

        internal async Task<HomePage> EmployerLogInToEmployerPortal()
        {
            await employerHomePageHelper.NavigateToEmployerApprenticeshipService(true);

            var employerType = listOfApprenticeship.FirstOrDefault().EmployerDetails.EmployerType.ToString();

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
