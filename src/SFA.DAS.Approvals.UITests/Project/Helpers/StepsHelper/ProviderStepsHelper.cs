using Azure;
using Microsoft.Playwright;
using Polly;
using SFA.DAS.Approvals.APITests.Project.Tests.StepDefinitions;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
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

        public ProviderStepsHelper(ScenarioContext _context)
        {
            context = _context;
            listOfApprenticeship = _context.GetValue<List<Apprenticeship>>();
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

            return await page2.VerifyPageAsync(() => new ApproveApprenticeDetailsPage(context));
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

        internal async Task ProviderApproveCohort(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
        {
            foreach (var apprenticeship in listOfApprenticeship)
            {
                await approveApprenticeDetailsPage.VerifyCohort(apprenticeship);

                bool isLast = apprenticeship.Equals(listOfApprenticeship.Last());

                if (isLast)
                {
                    var page1 = await approveApprenticeDetailsPage.ProviderApproveCohort();
                    await page1.VerifyCohortApprovedAndSentToEmployer(apprenticeship);
                    await page1.GoToApprenticeRequests();
                }
            }

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
                var page5 = await page4.ReserveFunds("Abattoir worker", DateTime.Now);
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

        internal async Task ProviderAddsOtherApprentices(ApproveApprenticeDetailsPage approveApprenticeDetailsPage)
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

            
        }


    }
}
