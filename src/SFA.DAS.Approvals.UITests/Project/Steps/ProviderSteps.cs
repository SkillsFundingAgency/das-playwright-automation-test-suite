using Azure;
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


        [When(@"Provider sends an apprentice request \(cohort\) to the employer by selecting same apprentices")]
        public async Task WhenProviderSendsAnApprenticeRequestCohortToTheEmployerBySelectingSameApprentices()
        {
            await providerStepsHelper.ProviderCreateAndApproveACohortViaIlrRoute();
        }

        [When("creates reservations for each learner")]
        public async Task WhenCreatesReservationsForEachLearner()
        {
            await providerStepsHelper.ProviderReserveFunds();
        }

        [When(@"sends an apprentice request \(cohort\) to the employer by selecting apprentices from ILR list and reservations")]
        public async Task WhenSendsAnApprenticeRequestCohortToTheEmployerBySelectingApprenticesFromILRListAndReservations()
        {
            var page = await providerStepsHelper.ProviderAddsFirstApprenitceUsingReservation();
            var page1 = await providerStepsHelper.ProviderAddsOtherApprentices(page);
            await providerStepsHelper.ProviderApproveCohort(page1);
        }


        [Given(@"Provider sends an apprentice request \(cohort\) to an employer")]
        public async Task GivenProviderSendsAnApprenticeRequestCohortToAnEmployer()
        {
            //create apprenticeships object
            var listOfApprenticeship = await new ApprenticeDataHelper(context).CreateApprenticeshipAsync(EmployerType.Levy, 1, null);
            context.Set(listOfApprenticeship);

            //recreate SLD pushing ILR data to AS
            var academicYear = listOfApprenticeship.FirstOrDefault().TrainingDetails.AcademicYear;
            var listOfLearnerDataList = await sldDataPushHelpers.ConvertToLearnerDataAPIDataModel(listOfApprenticeship);
            await sldDataPushHelpers.PushDataToAS(listOfLearnerDataList, academicYear);

            // create cohort using ILR data
            var page = await new ProviderStepsHelper(context).ProviderCreateAndApproveACohortViaIlrRoute();

            //Provider verify that cohort is under 'Apprentice requests > With employers' section
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;
            await page.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.WithEmployers, cohortRef);

        }

        [Then("return the cohort back to the Provider")]
        public async Task ThenReturnTheCohortBackToTheProvider()
        {
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();

            await new ApprenticeRequests_ProviderPage(context).NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.ReadyForReview, cohortRef);
        }

        [Then("Provider can access live apprentice records under Manager Your Apprentices section")]
        public async Task ThenProviderCanAccessLiveApprenticeRecordsUnderManagerYourApprenticesSection()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
            await new ProviderHomePage(context).GoToProviderManageYourApprenticePage();
            var page = new ManageYourApprentices_ProviderPage(context);

            foreach (var apprentice in listOfApprenticeship)
            {
                var uln = apprentice.ApprenticeDetails.ULN.ToString();
                var name = apprentice.ApprenticeDetails.FirstName + " " + apprentice.ApprenticeDetails.LastName;

                await page.VerifyApprenticeFound(uln, name);
            }

        }

        [Given("Provider adds an apprentice using Foundation level standard")]
        public async Task GivenProviderAddsAnApprenticeUsingFoundationLevelStandard()
        {
            //create apprenticeships object
            var listOfApprenticeship = await new ApprenticeDataHelper(context).CreateApprenticeshipAsync(EmployerType.Levy, 1, null);
            context.Set(listOfApprenticeship);

            //recreate SLD pushing ILR data to AS
            var academicYear = listOfApprenticeship.FirstOrDefault().TrainingDetails.AcademicYear;
            var listOfLearnerDataList = await sldDataPushHelpers.ConvertToLearnerDataAPIDataModel(listOfApprenticeship);
            await sldDataPushHelpers.PushDataToAS(listOfLearnerDataList, academicYear);


        }



    }
}
