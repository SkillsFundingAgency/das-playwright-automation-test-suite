using Azure;
using Microsoft.Playwright;
using Polly;
using SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper
{
    public class ProviderStepsHelper
    {
        private readonly ScenarioContext context;
        private List<Apprenticeship> listOfApprenticeship;
        private readonly ObjectContext objectContext;

        public ProviderStepsHelper(ScenarioContext _context)
        {
            context = _context;
            listOfApprenticeship = _context.GetValue<List<Apprenticeship>>();
            objectContext = context.Get<ObjectContext>();
        }

        internal async Task<ConfirmEmployerPage> SelectEmployer(ChooseAnEmployerPage chooseAnEmployerPage)
        {
            var agreementId = listOfApprenticeship.FirstOrDefault().EmployerDetails.AgreementId;
            return await chooseAnEmployerPage.ChooseAnEmployer(agreementId);
        }

        internal async Task<ApproveApprenticeDetailsPage> AddFirstApprenticeFromILRList(SelectApprenticeFromILRPage selectApprenticeFromILRPage)
        {
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await selectApprenticeFromILRPage.SelectApprenticeFromILRList(apprenticeship);
            await page.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
            var page1 = await page.ClickAddButton();
            var page2 = await page1.SelectNoForRPL();
            await page2.GetCohortId(apprenticeship);

            objectContext.SetDebugInformation($"Cohort Ref is: {apprenticeship.CohortReference}");
            return await page2.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<AddApprenticeDetailsPage> TryAddFirstApprenticeFromILRList(SelectApprenticeFromILRPage selectApprenticeFromILRPage)
        {
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await selectApprenticeFromILRPage.SelectApprenticeFromILRList(apprenticeship);
            await page.ClickAddButtonLeadToError();
            return await page.VerifyPageAsync(() => new AddApprenticeDetailsPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> AddOtherApprenticesFromILRList(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {

            foreach (var apprenticeship in listOfApprenticeship.Skip(1))
            {
                var page = await approveApprenticeDetailsPage.ClickOnAddAnotherApprenticeLink();
                var page1 = await page.SelectOptionToAddApprenticesFromILRList_AddAnotherApprenticeRoute();
                var page2 = await page1.SelectApprenticeFromILRList(apprenticeship);
                await page2.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
                var page3 = await page2.ClickAddButton();
                var page4 = await page3.SelectNoForRPL();
                await page4.GetCohortId(apprenticeship);
            }

            return approveApprenticeDetailsPage;
        }

        internal async Task<ApprenticeRequests_ProviderPage> ProviderApproveCohort(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            foreach (var apprenticeship in listOfApprenticeship)
            {
                await approveApprenticeDetailsPage.VerifyCohort(apprenticeship);
            }

            var page = await approveApprenticeDetailsPage.ProviderApproveCohort();
            await page.VerifyCohortApprovedAndSentToEmployer(listOfApprenticeship.FirstOrDefault());
            return await page.GoToApprenticeRequests();

        }

        internal async Task ProviderReserveFunds()
        {
            foreach (var apprenticeship in listOfApprenticeship)
            {
                var page = await new ProviderHomePage(context).GoToManageYourFunding();
                var page1 = await new FundingForNonLevyEmployersPage(context).ClickOnReserveMoreFundingLink();
                var page2 = await page1.ClickOnReserveFundingButton();
                var page3 = await SelectEmployer(page2);
                var page4 = await page3.ConfirmNonLevyEmployer();
                var page5 = await page4.ReserveFundsAsync("Abattoir worker", DateTime.Now);
                var page6 = await page5.ClickConfirmButton();
                await page6.GetReservationIdFromUrl(apprenticeship);
                var page7 = await page6.SelectGoToHomePageAndContinue();
                
            }
        }

        internal async Task<ApproveApprenticeDetailsPage> ProviderAddsFirstApprenitceUsingReservation()
        {         
            var apprenticeship = listOfApprenticeship.FirstOrDefault();
            var page = await new ProviderHomePage(context).GoToManageYourFunding();
            var page1 = await new FundingForNonLevyEmployersPage(context).SelectReservationToAddApprentice(apprenticeship);
            var page2 = await page1.SelectOptionToAddApprenticesFromILRList_NonLevyRoute();
            var page3 = await page2.SelectApprenticeFromILRList(apprenticeship);
            var page4 = await page3.ClickAddButton();
            var page5 = await page4.SelectNoForRPL();
            await page5.GetCohortId(apprenticeship);
            return await page5.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<ApproveApprenticeDetailsPage> ProviderAddsOtherApprentices(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            if (listOfApprenticeship.Count > 1)
            {
                foreach (var apprenticeship in listOfApprenticeship.Skip(1))
                {
                    var page = await approveApprenticeDetailsPage.ClickOnAddAnotherApprenticeLink();
                    var page1 = await page.SelectOptionToAddApprenticesFromILRList_SelectReservationRoute();
                    var page2 = await page1.SelectReservation(apprenticeship.ReservationID);
                    var page3 = await page2.SelectApprenticeFromILRList(apprenticeship);
                    await page3.ValidateApprenticeDetailsMatchWithILRData(apprenticeship);
                    var page4 = await page3.ClickAddButton();
                    var page5 = await page4.SelectNoForRPL();
                    await page5.GetCohortId(apprenticeship);
                }
            }

            return await approveApprenticeDetailsPage.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
        }

        internal async Task<AddApprenticeDetailsPage> ProviderCreateACohortViaIlrRouteWithInvalidDate()
        {
            var page = await GoToSelectApprenticeFromILRPage();
            var page1 = await TryAddFirstApprenticeFromILRList(page);

            return await page1.VerifyPageAsync(() => new AddApprenticeDetailsPage(context));
        }

        internal async Task<ApprenticeRequests_ProviderPage> ProviderCreateAndApproveACohortViaIlrRoute()
        {
            var page = await GoToSelectApprenticeFromILRPage();
            var page1 = await AddFirstApprenticeFromILRList(page);
            await AddOtherApprenticesFromILRList(page1);

            return await ProviderApproveCohort(page1);
        }

        private async Task<SelectApprenticeFromILRPage> GoToSelectApprenticeFromILRPage()
        {
            var page = await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page1 = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page2 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();
            var page3 = await page2.SelectOptionCreateANewCohort();
            var page4 = await SelectEmployer(page3);
            var page5 = await page4.ConfirmEmployer();

            return await page5.VerifyPageAsync(() => new SelectApprenticeFromILRPage(context));
        }


    }
}
