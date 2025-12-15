using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.Approvals.UITests.Project.Steps;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    public class ProviderStepsHelper
    {
        private readonly ScenarioContext context;
        private readonly ObjectContext objectContext;
        private readonly ApprenticeDataHelper apprenticeDataHelper;
        private readonly LearnerDataOuterApiSteps learnerDataOuterApiSteps;
        private readonly EmployerStepsHelper employerStepsHelper;
        private List<Apprenticeship> listOfApprenticeship;

        public ProviderStepsHelper(ScenarioContext _context)
        {
            context = _context;
            objectContext = context.Get<ObjectContext>();
            apprenticeDataHelper = new ApprenticeDataHelper(context);
            learnerDataOuterApiSteps = new LearnerDataOuterApiSteps(context);
            employerStepsHelper = new EmployerStepsHelper(context);
        }

        internal async Task<ConfirmEmployerPage> SelectEmployer(ChooseAnEmployerPage chooseAnEmployerPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var firstApprentice = listOfApprenticeship.FirstOrDefault();
            var agreementId = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().EmployerDetails.AgreementId;
            return await chooseAnEmployerPage.ChooseAnEmployer(agreementId);
        }

        internal async Task<ApproveApprenticeDetailsPage> AddFirstApprenticeFromILRList(SelectLearnerFromILRPage selectApprenticeFromILRPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await selectApprenticeFromILRPage.SelectApprenticeFromILRList(apprenticeship);
            await page.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
            await page.ClickAddButton();

            ApproveApprenticeDetailsPage page2;

            if (apprenticeship.TrainingDetails.StandardCode is 805 or 806 or 807 or 808 or 809 or 810 or 811)       //RPL check does not appear for foundation courses
            {
                page2 = new ApproveApprenticeDetailsPage(context);
            }
            else
            {
                var page1 = new RecognitionOfPriorLearningPage(context);
                page2 = await page1.SelectNoForRPL();
            }

            await page2.GetCohortId(apprenticeship);

            objectContext.SetDebugInformation($"Cohort Ref is: {apprenticeship.Cohort.Reference}");
            return await page2.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<CheckApprenticeDetailsPage> TryAddFirstApprenticeFromILRList(SelectLearnerFromILRPage selectApprenticeFromILRPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await selectApprenticeFromILRPage.SelectApprenticeFromILRList(apprenticeship);
            await page.ClickAddButton();
            return await page.VerifyPageAsync(() => new CheckApprenticeDetailsPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> AddOtherApprenticesFromILRList(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprenticeship in listOfApprenticeship.Skip(1))
            {
                var page1 = await approveApprenticeDetailsPage.ClickOnAddAnotherApprenticeLink();
                var page2 = await page1.SelectApprenticeFromILRList(apprenticeship);
                await page2.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
                await page2.ClickAddButton();
                var page3 = new RecognitionOfPriorLearningPage(context);
                var page4 = await page3.SelectNoForRPL();
                await page4.GetCohortId(apprenticeship);
            }

            return approveApprenticeDetailsPage;
        }

        internal async Task<ApprenticeRequests_ProviderPage> ProviderApproveCohort(ApproveApprenticeDetailsPage approveApprenticeDetailsPage, string cohortStatus= "New request")
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprenticeship in listOfApprenticeship)
            {
                await approveApprenticeDetailsPage.VerifyCohort(apprenticeship, cohortStatus);
            }

            var page = await approveApprenticeDetailsPage.ProviderApproveCohort();
            await page.VerifyCohortApprovedAndSentToEmployer(listOfApprenticeship.FirstOrDefault());
            return await page.GoToApprenticeRequests();

        }

        internal async Task<ApprenticeRequests_ProviderPage> ProviderSavesTheCohort(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            return await approveApprenticeDetailsPage.ClickOnSaveAndExitLink();
        }

        internal async Task ProviderReserveFunds()
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var page = await new ProviderHomePage(context).GoToManageYourFunding();
                await new FundingForNonLevyEmployersPage(context).ClickOnReserveMoreFundingLink();
                var page1 = await new ReserveFundingForNonLevyEmployersPage(context).ClickOnReserveFundingButton();
                var page2 = await SelectEmployer(page1);
                var page3 = await page2.ConfirmNonLevyEmployer();
                var page4 = await page3.ReserveFundsAsync("Abattoir worker", DateTime.Now);
                var page5 = await page4.ClickConfirmButton();
                await page5.GetReservationIdFromUrl(apprenticeship);
                var page6 = await page5.SelectGoToHomePageAndContinue();
                
            }
        }

        internal async Task<ApproveApprenticeDetailsPage> ProviderAddsFirstApprenitceUsingReservation()
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await new ProviderHomePage(context).GoToManageYourFunding();
            var page1 = await new FundingForNonLevyEmployersPage(context).SelectReservationToAddApprentice(apprenticeship);
            //var page2 = await page1.SelectOptionToAddApprenticesFromILRList_NonLevyRoute();
            var page3 = await page1.SelectApprenticeFromILRList(apprenticeship);            
            await page3.ClickAddButton();
            var page4 = new RecognitionOfPriorLearningPage(context);
            var page5 = await page4.SelectNoForRPL();
            await page5.GetCohortId(apprenticeship);
            return await page5.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> ProviderAddsOtherApprenticesUsingReservation(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            if (listOfApprenticeship.Count > 1)
            {
                foreach (var apprenticeship in listOfApprenticeship.Skip(1))
                {
                    var page = await approveApprenticeDetailsPage.ClickOnAddAnotherApprenticeLink_SelectReservationRoute();
                    //var page1 = await page.SelectOptionToAddApprenticesFromILRList_SelectReservationRoute();
                    var page2 = await page.SelectReservation(apprenticeship.ReservationID);
                    var page3 = await page2.SelectApprenticeFromILRList(apprenticeship);
                    await page3.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
                    await page3.ClickAddButton();
                    var page4 = new RecognitionOfPriorLearningPage(context);
                    var page5 = await page4.SelectNoForRPL();
                    await page5.GetCohortId(apprenticeship);
                }
            }

            return await approveApprenticeDetailsPage.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<CheckApprenticeDetailsPage> ProviderCreateACohortViaIlrRouteWithInvalidDoB()
        {
            var page = await GoToSelectApprenticeFromILRPage();
            var page1 = await TryAddFirstApprenticeFromILRList(page);

            return await page1.VerifyPageAsync(() => new CheckApprenticeDetailsPage(context));
        }

        internal async Task<ApprenticeRequests_ProviderPage> ProviderCreateAndApproveACohortViaIlrRoute()
        {
            var page = await GoToSelectApprenticeFromILRPage();
            var page1 = await AddFirstApprenticeFromILRList(page);
            await AddOtherApprenticesFromILRList(page1);

            return await ProviderApproveCohort(page1);
        }

        internal async Task<ApprenticeRequests_ProviderPage> ProviderCreateADraftCohortViaIlrRoute()
        {
            var page = await GoToSelectApprenticeFromILRPage();
            var page1 = await AddFirstApprenticeFromILRList(page);
            await AddOtherApprenticesFromILRList(page1);

            return await ProviderSavesTheCohort(page1);
        }

        internal async Task<SelectLearnerFromILRPage> GoToSelectApprenticeFromILRPage(bool login=true)
        {
            if (login) { await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false); }

            var page1 = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page2 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();
            var page3 = await page2.SelectOptionCreateANewCohort();
            var page4 = await SelectEmployer(page3);
            var page5 = await page4.ConfirmEmployer();

            return await page5.VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

        internal async Task<ApprenticeDetails_ProviderPage> ProviderSearchOpenApprovedApprenticeRecord(ManageYourApprentices_ProviderPage manageYourApprenticesPage, string uln, string name)
        {
            await manageYourApprenticesPage.SearchApprentice(uln);
            return await manageYourApprenticesPage.OpenFirstItemFromTheList(name);
        }

        internal async Task TryEditApprenticeAgeAndValidateError(ApprenticeDetails_ProviderPage apprenticeDetailsPage, DateTime dateOfBirth)
        {
            string expectedErrorMessage = "The apprentice must be younger than 25 years old at the start of their training";
            await apprenticeDetailsPage.ClickOnEditApprenticeDetailsLink();
            var page = new EditApprenticeDetails_ProviderPage(context);
            await page.EditDoB(dateOfBirth);
            await page.ClickUpdateDetailsButton();
            await page.ValidateErrorMessage(expectedErrorMessage, "DateOfBirth");
        }

        internal async Task ProviderVerifyLearnerNotAvailableForSelection()
        {
            var selectLearnerFromILRPage = await GoToSelectApprenticeFromILRPage();

            foreach (var apprenticeship in listOfApprenticeship)
            {
                var uln = apprenticeship.ApprenticeDetails.ULN.ToString();

                await selectLearnerFromILRPage.SearchULN(uln);
                await selectLearnerFromILRPage.VerifyNoResultsFound();
                await selectLearnerFromILRPage.ClearSearch();
            }
        }

        internal async Task<ApproveApprenticeDetailsPage> ProviderAddApprencticesFromIlrRoute()
        {
            var page = await GoToSelectApprenticeFromILRPageForExistingCohort();
            var page1 = await AddFirstApprenticeFromILRListWithRPLDetails(page);
           return await AddOtherApprenticesFromILRListWithRPL(page1);
        }

        internal async Task<SelectLearnerFromILRPage> GoToSelectApprenticeFromILRPageForExistingCohort()
        {
            var page = await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page1 = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page2 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();
            var page3 = await page2.SelectOptionUseExistingCohort();
            var page4 = await SelectanExistingEmployer(page3);
            var page5 = await page4.ClickOnAddAnotherApprenticeLink();

            return await page5.VerifyPageAsync(() => new SelectLearnerFromILRPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> SelectanExistingEmployer(ChooseACohortPage chooseAnExistingEmployerPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var firstApprentice = listOfApprenticeship.FirstOrDefault();
            var cohortReference = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;
           return await chooseAnExistingEmployerPage.ChooseAnExistingEmployer(cohortReference);
        }

        internal async Task<ApproveApprenticeDetailsPage> AddOtherApprenticesFromILRListWithRPL(ApproveApprenticeDetailsPage approveApprenticeDetailsPage, int NoOfApprenticesToSkip=1)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprenticeship in listOfApprenticeship.Skip(NoOfApprenticesToSkip))
            {
                var page = await approveApprenticeDetailsPage.ClickOnAddAnotherApprenticeLink();
                var page1 = await page.SelectApprenticeFromILRList(apprenticeship);
                await page1.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
                await page1.ClickAddButton();
                var page2 = new RecognitionOfPriorLearningPage(context);
                var page3 = await page2.SelectYesForRPL();
                var page4 = await page3.EnterRPLDataAndContinue(apprenticeship);
               await page4.GetCohortId(apprenticeship);
            }

            return approveApprenticeDetailsPage;
        }

        internal async Task<ApproveApprenticeDetailsPage> AddFirstApprenticeFromILRListWithRPLDetails(SelectLearnerFromILRPage selectApprenticeFromILRPage)
        {
            listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await selectApprenticeFromILRPage.SelectApprenticeFromILRList(apprenticeship);
            await page.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
            await page.ClickAddButton();

            ApproveApprenticeDetailsPage page2;

            if (apprenticeship.TrainingDetails.StandardCode is 805 or 806 or 807 or 808 or 809 or 810 or 811)       //RPL check does not appear for foundation courses
            {
                page2 = new ApproveApprenticeDetailsPage(context);
            }
            else
            {
                var page1 = new RecognitionOfPriorLearningPage(context);
                var page3 = await page1.SelectYesForRPL();
                page2 = await page3.EnterRPLDataAndContinue(apprenticeship);
            }

            await page2.GetCohortId(apprenticeship);

            objectContext.SetDebugInformation($"Cohort Ref is: {apprenticeship.Cohort.Reference}");
            return await page2.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> ProviderOpenTheCohort(string cohortRerefence)
        {
            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
            var page = await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            await page.SelectCohort(cohortRerefence);
            return new ApproveApprenticeDetailsPage(context);
        }

        internal async Task<ApproveApprenticeDetailsPage> UpdateDobAndReprocessData(int lowerAgeLimit, int upperAgeLimit)
        {
            var currentDate = DateTime.Now;
            var listOfApprenticeship = context.Get<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);

            foreach (var apprentice in listOfApprenticeship)
            {
                var newDoB = RandomDataGenerator.GenerateRandomDate(currentDate.AddYears(-upperAgeLimit), currentDate.AddYears(-lowerAgeLimit));
                apprentice.ApprenticeDetails.DateOfBirth = newDoB;
            }
            context["listOfApprenticeship"] = listOfApprenticeship;

            await learnerDataOuterApiSteps.SLDPushDataIntoAS();

            var page = await GoToSelectApprenticeFromILRPage();
            return await AddFirstApprenticeFromILRList(page);

        }
    }

}
