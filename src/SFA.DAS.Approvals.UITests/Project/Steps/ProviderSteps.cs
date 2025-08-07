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
using TechTalk.SpecFlow.Assist;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class ProviderSteps
    {
        private readonly ScenarioContext context;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly SldIlrSubmissionSteps sldIlrSubmissionSteps;

        public ProviderSteps(ScenarioContext _context)
        {
            context = _context;            
            providerStepsHelper = new ProviderStepsHelper(context);
            sldIlrSubmissionSteps = new SldIlrSubmissionSteps(context);
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
            var page1 = await providerStepsHelper.ProviderAddsOtherApprenticesUsingReservation(page);
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
        internal async Task<ManageYourApprentices_ProviderPage> ThenProviderAccessLiveApprenticeRecords()
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

            return page;
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

        [When("Provider tries to add a new apprentice using details from table below")]
        public async Task WhenProviderTriesToAddANewApprenticeUsingDetailsFromTableBelow(Table table)
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();
            var listOfValidApprenticeship = listOfApprenticeship;
            var apprentice = listOfValidApprenticeship.FirstOrDefault();

            var OltdDetails = table.CreateSet<OltdDetails>().ToList();

            foreach (var item in OltdDetails)
            {
                //Update valid apprentice object with new start and end dates. Then push it as new apprentice details on SLD endpoint
                apprentice.TrainingDetails.StartDate = DateTime.Now.AddMonths(item.NewStartDate);       //apprentice.TrainingDetails.StartDate.AddMonths(item.NewStartDate);
                apprentice.TrainingDetails.EndDate = DateTime.Now.AddMonths(item.NewEndDate);         //apprentice.TrainingDetails.EndDate.AddMonths(item.NewEndDate);

                listOfApprenticeship[0] = apprentice;
                context.Set(listOfApprenticeship);

                // Push data on SLD end point  
                await new SldIlrSubmissionSteps(context).SLDPushDataIntoAS();

                // Try to add above apprentice and validate error message  
                var page = await providerStepsHelper.GoToSelectApprenticeFromILRPage();
                var page1 = await providerStepsHelper.TryAddFirstApprenticeFromILRList(page);
                var oltdErrorMsg = "The date overlaps with existing dates for the same apprentice";


                if (item.DisplayOverlapErrorOnStartDate)
                    await page1.VerfiyErrorMessage("StartDate", oltdErrorMsg);
                else
                    await page1.VerfiyErrorMessage("StartDate", "");

                if (item.DisplayOverlapErrorOnEndDate)
                    await page1.VerfiyErrorMessage("EndDate", oltdErrorMsg);
                else
                    await page1.VerfiyErrorMessage("EndDate", "");

            }

        }

        [When(@"the provider adds (.*) apprentices and sends to employer to review")]
        public async Task WhenTheProviderAddsApprenticesAndSendsToEmployerToReview(int numberOfApprentices)
        {
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;
            var apprentices = context.GetValue<List<Apprenticeship>>().ToList();

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);
            var page1 = await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            await page1.SelectCohort(cohortRef);

            await new ProviderStepsHelper(context).ProviderAddApprencticesFromIlrRoute();

           var page = await new ApproveApprenticeDetailsPage(context).ProviderSendCohortForEmployerApproval();

        }

        [Then("the provider approves the cohorts")]
        public async Task ThenTheProviderApprovesCohort()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
            var page1 = await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            await page1.SelectCohort(cohortRef);
            var page = await new ApproveApprenticeDetailsPage(context).ProviderApprovesCohortAfterEmployerApproval();
        }



        }  


    public class OltdDetails
    {
        public int NewStartDate { get; set; }
        public int NewEndDate { get; set; }
        public bool DisplayOverlapErrorOnStartDate { get; set; }
        public bool DisplayOverlapErrorOnEndDate { get; set; }
    }
}
