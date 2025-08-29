using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.SqlHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages;
using SFA.DAS.Approvals.UITests.Project.Pages.Employer;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.FrameworkHelpers;
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
        private readonly ProviderHomePageStepsHelper providerHomePageStepsHelper;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly DbSteps dbSteps;
        private readonly SldIlrSubmissionSteps sldIlrSubmissionSteps;

        public ProviderSteps(ScenarioContext _context)
        {
            context = _context;
            providerHomePageStepsHelper = new ProviderHomePageStepsHelper(context);
            providerStepsHelper = new ProviderStepsHelper(context);
            dbSteps = new DbSteps(context);
            sldIlrSubmissionSteps = new SldIlrSubmissionSteps(context);
        }

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

            await providerHomePageStepsHelper.GoToProviderHomePage(false);
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();

            var page = new ApprenticeRequests_ProviderPage(context);
            await page.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.ReadyForReview);
            await page.VerifyCohortExistsAsync(cohortRef);

        }

        [Then("Provider can access live apprentice records under Manager Your Apprentices section")]
        internal async Task<ManageYourApprentices_ProviderPage> ThenProviderAccessLiveApprenticeRecords()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();

            await providerHomePageStepsHelper.GoToProviderHomePage(true);
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
            await page.VerfiyErrorMessage("DateOfBirth", "The apprentice must be 24 years or under at the start of their training");
            await page.ClickOnNavBarLinkAsync("Home");
        }

        [When("Provider tries to edit live apprentice record by setting age old than 24 years")]
        public async Task WhenProviderTriesToEditLiveApprenticeRecordBySettingAgeOldThanYears()
        {
            await providerHomePageStepsHelper.GoToProviderHomePage(true);
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

        [Then("apprentice\\/learner record is no longer available on SelectLearnerFromILR page")]
        public async Task ThenApprenticeLearnerRecordIsNoLongerAvailableOnSelectLearnerFromILRPage()
        {
            await providerStepsHelper.ProviderVerifyLearnerNotAvailableForSelection();
        }

        [When("Provider tries to add a new apprentice using details from table below")]
        public async Task WhenProviderTriesToAddANewApprenticeUsingDetailsFromTableBelow(Table table)
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>();
            var apprentice = listOfApprenticeship.FirstOrDefault();
            var originalStartDate = apprentice.TrainingDetails.StartDate;
            var originalEndDate = apprentice.TrainingDetails.EndDate;
            var OltdDetails = table.CreateSet<OltdDetails>().ToList();

            foreach (var item in OltdDetails)
            {
                //Update valid apprentice object with new start and end dates. Then push it as new apprentice details on SLD endpoint
                apprentice.TrainingDetails.StartDate = originalStartDate.AddMonths(Convert.ToInt32(item.NewStartDate));
                apprentice.TrainingDetails.EndDate = originalEndDate.AddMonths(Convert.ToInt32(item.NewEndDate));

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


        [When("user navigates to Apprentice requests page")]
        public async Task WhenUserNavigatesToApprenticeRequestsPage()
        {
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();
        }       

        [When(@"the provider adds (.*) apprentices along with RPL details and sends to employer to review")]
        public async Task WhenTheProviderAddsApprenticesAndSendsToEmployerToReview(int numberOfApprentices)
        {
            var cohortRef = context.GetValue<List<Apprenticeship>>().FirstOrDefault().CohortReference;

            await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(true);
            var page1 = await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            await page1.SelectCohort(cohortRef);
            var page2 = await new ProviderStepsHelper(context).ProviderAddApprencticesFromIlrRoute();
            await page2.ProviderSendCohortForEmployerApproval();
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
        public string NewStartDate { get; set; }
        public string NewEndDate { get; set; }
        public bool DisplayOverlapErrorOnStartDate { get; set; }
        public bool DisplayOverlapErrorOnEndDate { get; set; }
    }
}
