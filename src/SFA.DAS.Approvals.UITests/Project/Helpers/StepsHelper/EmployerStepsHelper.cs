using Azure;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFA.DAS.EmployerPortal.UITests.Project.Pages.HomePage;

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

        internal async Task<EmployerApproveApprenticeDetailsPage> OpenCohort()
        {
            await EmployerLogInToEmployerPortal();

            await new InterimApprenticesHomePage(context, false).VerifyPage();

            var page = await new ApprenticesHomePage(context).GoToApprenticeRequests();
            
            var apprenticeship = listOfApprenticeship.FirstOrDefault();

            var page1 = await page.OpenApprenticeRequestReadyForReview(apprenticeship.CohortReference);

            await page1.VerifyCohort(apprenticeship);
            
            return page1;
        }


        internal async Task CheckApprenticeOnManageYourApprenticesPage(bool login = false)
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            await (login ? EmployerLogInToEmployerPortal() : employerHomePageHelper.NavigateToEmployerApprenticeshipService(true));

            await new InterimApprenticesHomePage(context, false).VerifyPage();

            await new ApprenticesHomePage(context).GoToManageYourApprentices();

            var page = new Pages.Employer.ManageYourApprenticesPage(context);
            
            foreach (var apprentice in listOfApprenticeship)
            {
                var uln = apprentice.ApprenticeDetails.ULN.ToString();
                var name = apprentice.ApprenticeDetails.FullName;
                
                await page.SearchApprentice(uln, name);
            }        
        
        }

        internal async Task<ApprenticeDetailsPage> EmployerSearchOpenApprovedApprenticeRecord(ApprenticesHomePage apprenticesHomePage, string uln, string name)
        {
            await apprenticesHomePage.GoToManageYourApprentices();
            var page = new Pages.Employer.ManageYourApprenticesPage(context);
            await page.SearchApprentice(uln, name);
            return await page.OpenFirstItemFromTheList(name);
        }

        internal async Task TryEditApprenticeAgeAndValidateError(ApprenticeDetailsPage apprenticeDetailsPage, DateTime dateOfBirth)
        {
            string expectedErrorMessage = "The apprentice must be younger than 25 years old at the start of their training";
            var page = await apprenticeDetailsPage.ClickOnEditApprenticeDetailsLink();
            await page.EditDoB(dateOfBirth);
            await page.ClickUpdateDetailsButton();
            await page.ValidateErrorMessage(expectedErrorMessage, "DateOfBirth");
        }

        internal async Task AddEmptyCohort()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();
            await EmployerLogInToEmployerPortal();

            await new InterimApprenticesHomePage(context, false).VerifyPage();

            await new ApprenticesHomePage(context).GoToAddAnApprentice();

            var trainingProviderPage =  await new AddApprenticePage(context).ClickStartNowButton();

           // await new SelectFundingPage(context).SelectFundingtype();

            var confirmTrainingProvider =   await trainingProviderPage.SubmitValidUkprn();

            var confirmApprentices =   await confirmTrainingProvider.ConfirmTrainingProviderDetails();

            await confirmApprentices.SelectAddApprencticesByProvider();

            await new ConfirmRequestSentPage(context).SetCohortReference(listOfApprenticeship.FirstOrDefault());

        }

        internal async Task EmployerNavigateToApprenticeRequestsPage()
        {
            var apprenticeship = listOfApprenticeship.FirstOrDefault();

            var page =  await GoToEmployerApprenticesHomePage();
            var page1 = await page.GoToApprenticeRequests();
            //var page2 = await page1.ValidateCohortStatus(apprenticeship.CohortReference, ""CohortStatus);         <-- to be implemented
        }

        private async Task<ApprenticesHomePage> GoToEmployerApprenticesHomePage()
        {
            await employerHomePageHelper.GotoEmployerHomePage();
            await new InterimApprenticesHomePage(context, false).VerifyPage();
            return new ApprenticesHomePage(context);
        }
    }
}
