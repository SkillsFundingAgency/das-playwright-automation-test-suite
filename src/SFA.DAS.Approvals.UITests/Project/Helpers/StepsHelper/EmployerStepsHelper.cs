using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.InterimPages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    internal class EmployerStepsHelper
    {
        private readonly ScenarioContext context;
        private readonly CommonStepsHelper commonStepsHelper;
        protected readonly EmployerPortalLoginHelper employerLoginHelper;
        protected readonly EmployerHomePageStepsHelper employerHomePageHelper;
        private List<Apprenticeship> listOfApprenticeship;

        public EmployerStepsHelper(ScenarioContext _context)
        {
            context = _context;
            commonStepsHelper = new CommonStepsHelper(context);
            employerLoginHelper = new EmployerPortalLoginHelper(context);
            employerHomePageHelper = new EmployerHomePageStepsHelper(context);
        }

        internal async Task<HomePage> EmployerLogInToEmployerPortal(bool openInNewTab = true)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            await employerHomePageHelper.NavigateToEmployerApprenticeshipService(openInNewTab);

            if (await employerLoginHelper.IsHomePageDisplayed())
                return new HomePage(context);

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
                    await employerLoginHelper.Login(context.GetUser<NonLevyUserAtMaxReservationLimit>(), false);                    
                    break;
                default:
                    throw new ArgumentException($"Unknown employer type: {employerType}");
            }

            return new HomePage(context);
        }

        internal async Task<EmployerApproveApprenticeDetailsPage> OpenCohort(bool validateCohortDetails = true)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            await EmployerLogInToEmployerPortal();

            await new InterimApprenticesHomePage(context, false).VerifyPage();

            var page = await new ApprenticesHomePage(context).GoToApprenticeRequests();

            var apprenticeship = listOfApprenticeship.FirstOrDefault();

            var page1 = await page.OpenApprenticeRequestReadyForReview(apprenticeship.Cohort.Reference);

            if (validateCohortDetails)
                await page1.VerifyCohort(apprenticeship);

            return page1;
        }

        internal async Task<ManageYourApprenticesPage> CheckApprenticeOnManageYourApprenticesPage(bool login = false)
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

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

            return page;
        }

        internal async Task GoToLiveApprenticeshipPageFromDynamicHomePage()
        {
            await employerHomePageHelper.GotoEmployerHomePage(false);
            await new ViewApprenticeDetailsDynamicHomepage(context).ViewApprenticeDetails();
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
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var ukprn = listOfApprenticeship.FirstOrDefault().ProviderDetails.Ukprn;

            await EmployerLogInToEmployerPortal(false);
            await new InterimApprenticesHomePage(context, false).VerifyPage();
            var page = await new ApprenticesHomePage(context).GoToAddAnApprentice();
            var page1 =  await page.ClickStartNowButton();
            var page2 =   await page1.SubmitValidUkprn(ukprn);
            var page3 =   await page2.ConfirmTrainingProviderDetails();
            var page4 = await page3.SelectProviderAddApprencticesAndSend();
            var cohortRef = await page4.GetCohortId();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Ready for review", "Under review with Provider");
        }

        internal async Task AddEmptyCohortFromNonLevyReserveFundsAddApprenticePage()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var ukprn = listOfApprenticeship.FirstOrDefault().ProviderDetails.Ukprn;
 
            var page1 = await new AddApprenticePage(context).ClickStartNowButtonNonLevyFlow();
            var page2 =  await page1.SelectReservedFunds();
            var page3 = await page2.SelectReservation();
            var page4 = await page3.SubmitValidUkprn(ukprn);
            var page5 = await page4.ConfirmTrainingProviderDetails();
            var page6 = await page5.SelectProviderAddApprencticesAndSend();
            
            var cohortRef = await page6.GetCohortId();
            await commonStepsHelper.SetCohortDetails(cohortRef, "Ready for review", "Under review with Provider");
        }

        internal async Task ReadyForReviewCohort(string status)
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var cohort = listOfApprenticeship.FirstOrDefault().Cohort.Reference;

            await employerHomePageHelper.NavigateToEmployerApprenticeshipService(true);

            var page1 = await new ApprenticesHomePage(context).GoToApprenticeRequests();
            var page2 = await page1.OpenApprenticeRequestReadyForReview(cohort);
            await page2.ValidateCohortStatus(status);
        }

        internal async Task ReadyForReviewOnDynamicHomePage(string status)
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var cohort = listOfApprenticeship.FirstOrDefault().Cohort.Reference;

            await employerHomePageHelper.NavigateToEmployerApprenticeshipService(true);

            var page1 = await new ReviewApprenticeDetailsDynamicHomepage(context).ReviewApprenticeDetails();
            await page1.ValidateCohortStatus(status);
        }

        internal async Task LogOutThenLogbackIn()
        {
            await employerHomePageHelper.NavigateToEmployerApprenticeshipService(true);
            await AccountSignOutHelper.SignOut(new HomePage(context));
            await EmployerLogInToEmployerPortal(false);
        }

        internal async Task<YouCannotCreateAnotherFundingReservationPage> EmployerTriesToCreateReservation()
        {
           await employerHomePageHelper.GotoEmployerHomePage(false);
            var page =  new EmployerHomePage(context);
            var page2 = await page.ClickOnFundingReservationsLink();
            return await page2.TryClickOnReserveMoreFundingLink();
        }

        internal async Task<EmployerHomePage> EmployerTriesToCreateReservationOnDynamicHomepage()
        {

            return await ReserveFundsFromDynamicHomepage();
        }

        private async Task<EmployerHomePage> ReserveFundsFromDynamicHomepage()
        {
            var dynamicHomepage = new SetupAnApprenticeshipDynamicHomepage(context);

            var page1 = await dynamicHomepage.StartNow();
            var page2 = await page1.IKnowWhichCourseMyApprenticeWillTake();
            var page3 = await page2.IChooseTrainingProvider();
            var page4 = await page3.StartInSixMonths();
            var page5 = await page4.SetApprenticeshipForNewEmployee();
            var page6 = await page5.YesContinueToReserveFunding();
            var page7 = await page6.YesContinueToReserveFunding();
            var page8 = await page7.ReserveFundsAsync("Associate");
            var page9 = await page8.SelectAlreadyStartedDate();
            var page10 = await page9.ClickConfirmButton();

            return await page10.SelectGoToHomePageAndContinue();
        }
    }
}