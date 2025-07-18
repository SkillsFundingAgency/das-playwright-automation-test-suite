using Azure;
using Polly;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
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
        private ProviderStepsHelper providerStepsHelper;

        public ProviderSteps(ScenarioContext _context)
        {
            context = _context;            
            providerStepsHelper = new ProviderStepsHelper(context);          
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
                var name = apprentice.ApprenticeDetails.FullName;

                await page.VerifyApprenticeFound(uln, name);
            }

        }

        [Then("system does not allow to add apprentice details if their age is below 15 years and over 25 years")]
        public async Task ThenSystemDoesNotAllowToAddApprenticeDetailsIfTheirAgeIsBelow15YearsAndOver25Years()
        {
            var page = await new ProviderStepsHelper(context).ProviderCreateACohortViaIlrRouteWithInvalidDoB();
            await page.VerfiyErrorMessage("DateOfBirth", "The apprentice must be 25 years old or younger at the start of their training");
            await page.ClickNavBarLinkAsync("Home");
        }

        [When("Provider tries to edit live apprentice record by setting age old than 24 years")]
        public async Task WhenProviderTriesToEditLiveApprenticeRecordBySettingAgeOldThanYears()
        {
            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
            await new ProviderHomePage(context).GoToProviderManageYourApprenticePage();
        }

        [Then("the provider is stopped with an error message")]
        public async Task ThenTheProviderIsStoppedWithAnErrorMessage()
        {
            var apprentice = context.GetValue<List<Apprenticeship>>().FirstOrDefault();
            var uln = apprentice.ApprenticeDetails.ULN.ToString();
            var name = apprentice.ApprenticeDetails.FullName;
            var DoB = apprentice.ApprenticeDetails.DateOfBirth.AddYears(-10);

            var apprenticeDetailsPage = await providerStepsHelper.ProviderSearchOpenApprovedApprenticeRecord(new ManageYourApprentices_ProviderPage(context), uln, name);
            await providerStepsHelper.TryEditApprenticeAgeAndValidateError(apprenticeDetailsPage, DoB);
        }






    }
}
