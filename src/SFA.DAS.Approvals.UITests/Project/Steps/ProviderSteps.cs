using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class ProviderSteps
    {
        private readonly ScenarioContext context;
        private readonly SLDDataPushHelpers sldDataPushHelpers;
        private ProviderStepsHelper providerStepsHelper;

        public ProviderSteps(ScenarioContext _context)
        {
            context = _context;
            providerStepsHelper = new ProviderStepsHelper(context);
            sldDataPushHelpers = new(context);
        }


        [Given(@"the provider logs into portal")]
        [When("Provider logs into Provider-Portal")]
        public async Task GivenTheProviderLogsIntoPortal() => await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);


        [When(@"creates an apprentice request \(cohort\) by selecting same apprentices")]
        public async Task WhenCreatesAnApprenticeRequestCohortBySelectingSameApprentices()
        {
            var page = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page1 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();
            var page2 = await page1.SelectOptionCreateANewCohort();
            var page3 = await providerStepsHelper.SelectEmployer(page2);
            var page4 = await page3.ConfirmEmployer();
            var page5 = await providerStepsHelper.AddFirstApprenticeFromILRList(page4);
            await providerStepsHelper.AddOtherApprenticesFromILRList(page5);
        }


        [Then("Provider can send it to the Employer for approval")]
        public async Task ThenProviderCanSendItToTheEmployerForApproval()
        {
            var page = new ApproveApprenticeDetailsPage(context);
            await providerStepsHelper.ProviderApproveCohort(page);
        }


        [When("creates reservations for each learner")]
        public async Task WhenCreatesReservationsForEachLearner()
        {
            await providerStepsHelper.ProviderReserveFunds();
        }


        [When(@"creates an apprentice request \(cohort\) by selecting apprentices from ILR list via reservations")]
        public async Task WhenCreatesAnApprenticeRequestCohortBySelectingApprenticesFromILRListViaReservations()
        {
            var page = await providerStepsHelper.ProviderAddsFirstApprenitceUsingReservation();
            await providerStepsHelper.ProviderAddsOtherApprentices(page);

        }

        [Given(@"Provider sends an apprentice request \(cohort\) to an employer")]
        public async Task GivenProviderSendsAnApprenticeRequestCohortToAnEmployer()
        {
            //create apprenticeships object
            var listOfApprenticeship = await new ApprenticeDataHelper(context).CreateNewApprenticeshipDetails(EmployerType.Levy, 1, null);
            context.Set(listOfApprenticeship);

            //recreate SLD pushing ILR data to AS
            var academicYear = listOfApprenticeship.FirstOrDefault().TrainingDetails.AcademicYear;
            var listOfLearnerDataList = await sldDataPushHelpers.ConvertToLearnerDataAPIDataModel(listOfApprenticeship);
            await sldDataPushHelpers.PushDataToAS(listOfLearnerDataList, academicYear);

            // create cohort using ILR data
            var page = await ProviderCreateAndApproveACohortViaIlrRoute();

            //Provider verify that cohort is under 'Apprentice requests > With employers' section
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;
            await page.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.WithEmployers, cohortRef);

        }


        private async Task<ApprenticeRequests_ProviderPage> ProviderCreateAndApproveACohortViaIlrRoute()
        {
            providerStepsHelper = new ProviderStepsHelper(context);

            var page = await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page1 = await new ProviderHomePage(context).GotoSelectJourneyPage();
            var page2 = await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();
            var page3 = await page2.SelectOptionCreateANewCohort();
            var page4 = await providerStepsHelper.SelectEmployer(page3);
            var page5 = await page4.ConfirmEmployer();
            var page6 = await providerStepsHelper.AddFirstApprenticeFromILRList(page5);
            await providerStepsHelper.AddOtherApprenticesFromILRList(page6);

            return await providerStepsHelper.ProviderApproveCohort(page6);
        }




    }
}
