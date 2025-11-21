
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;


namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class UpdatingCohortViaIlrSteps
    {
        private readonly ScenarioContext context;
        private readonly CommonStepsHelper commonStepsHelper;
        private readonly ProviderHomePageStepsHelper providerHomePageStepsHelper;
        private readonly EmployerStepsHelper employerStepsHelper;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly DbSteps dbSteps;
        private readonly LearnerDataOuterApiSteps learnerDataOuterApiSteps;

        public UpdatingCohortViaIlrSteps(ScenarioContext _context)
        {
            context = _context;
            commonStepsHelper = new CommonStepsHelper(context);
            employerStepsHelper = new EmployerStepsHelper(context);
            providerHomePageStepsHelper = new ProviderHomePageStepsHelper(context);
            providerStepsHelper = new ProviderStepsHelper(context);
            dbSteps = new DbSteps(context);
            learnerDataOuterApiSteps = new LearnerDataOuterApiSteps(context);
        }


        [Given(@"a cohort created via ILR exists in (.*) section")]
        public async Task GivenACohortCreatedViaILRExistsInSection(ApprenticeRequests cohortStatus)
        {
            //check db if an existing cohorts can be used
            var listOfApprenticeship = new List<Apprenticeship>();
            Apprenticeship apprenticeship = await new ApprenticeDataHelper(context).CreateEmptyCohortAsync(EmployerType.Levy);
            apprenticeship = await dbSteps.FindUnapprovedCohortReference(apprenticeship, cohortStatus);

            if (apprenticeship.Cohort.Reference == null)
            {
                context.Get<ObjectContext>().SetDebugInformation($"No unapproved cohort found in Commitments Db for Ukprn: {apprenticeship.ProviderDetails.Ukprn} and AccountLegalEntityId: {apprenticeship.EmployerDetails.AccountLegalEntityId}. Hence creating data using UI journey ...");
                await learnerDataOuterApiSteps.ProviderSubmitsAnILRRecord(2, EmployerType.Levy.ToString());
                await learnerDataOuterApiSteps.SLDPushDataIntoAS();
                switch (cohortStatus)
                {
                    case ApprenticeRequests.ReadyForReview:
                        await employerStepsHelper.AddEmptyCohort();
                        await providerHomePageStepsHelper.GoToProviderHomePage(true);
                        var page = await providerStepsHelper.ProviderAddApprencticesFromIlrRoute();
                        await page.ClickOnSaveAndExitLink();
                        break;
                    case ApprenticeRequests.WithEmployers:
                        await providerStepsHelper.ProviderCreateAndApproveACohortViaIlrRoute();
                        break;
                    case ApprenticeRequests.Drafts:
                        await providerStepsHelper.ProviderCreateADraftCohortViaIlrRoute();
                        break;
                    case ApprenticeRequests.WithTransferSendingEmployers:
                        //to be implemented later
                        break;                   
                }
            }
            else
            {
                listOfApprenticeship.Add(apprenticeship);
                context.Set(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
                await providerHomePageStepsHelper.GoToProviderHomePage(false);
                context.GetValue<ObjectContext>().SetDebugInformation($"Cohort found in the database with status: {cohortStatus}");
            }

            var cohortRef = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault().Cohort.Reference;
            context.GetValue<ObjectContext>().SetDebugInformation($"Cohort Reference is: {cohortRef}");
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            var page1 = new ApprenticeRequests_ProviderPage(context);
            await page1.NavigateToBingoBox(cohortStatus);
            await page1.VerifyCohortExistsAsync(cohortRef);
        }



        [Then("a banner is displayed on the cohort for provider to accept changes")]
        public async Task ThenABannerIsDisplayedOnTheCohortForProviderToAcceptChanges()
        {
            var updatedApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfUpdatedApprenticeship).FirstOrDefault();
            var apprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship).FirstOrDefault();
            var cohortRef = apprenticeship.Cohort.Reference;
            var fullName = apprenticeship.ApprenticeDetails.FullName;
            string bannerTitle = "Records need updating";
            string bannerMessage = $@"Select 'View' to update details for the following apprentices: {fullName}";

            await providerHomePageStepsHelper.GoToProviderHomePage(false);
            await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            var page = new ApprenticeRequests_ProviderPage(context);

            if(context.ScenarioInfo.Title.Contains("Drafts"))
                await page.NavigateToBingoBox(ApprenticeRequests.Drafts);
            else
                await page.NavigateToBingoBox(ApprenticeRequests.ReadyForReview);

            var page2 = await page.OpenEditableCohortAsync(cohortRef);
            await page2.VerifyBanner(bannerTitle, bannerMessage);
        }

        [Then("Provider cannot approve the cohort")]
        public async Task ThenProviderCannotApproveTheCohort()
        {
            var page = new ApproveApprenticeDetailsPage(context);
            await page.CanCohortBeApproved(false);
        }

        [When("Provider reviews and accepts the changes")]
        public async Task WhenProviderReviewsAndAcceptsTheChanges()
        {
            var listOfApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfApprenticeship);
            var listOfUpdatedApprenticeship = context.GetValue<List<Apprenticeship>>(ScenarioKeys.ListOfUpdatedApprenticeship);
            
            var page = new ApproveApprenticeDetailsPage(context);
            await page.ClickOnLink(listOfApprenticeship.FirstOrDefault().ApprenticeDetails.FullName);
            
            var page2 = new ViewApprenticeDetails_ProviderPage(context);
            await page2.VerifyBanner("Important", "Apprentice details have been changed in the ILR. To continue you need to update now");
            await page2.ClickOnButton("update now");
            await page2.VerifyBanner("Success", "Learner data has been successfully updated.");
            await page2.VerifyApprenticeshipDetails(listOfUpdatedApprenticeship.FirstOrDefault());
            await page2.ClickOnButton("Continue");

            var page3 = new RecognitionOfPriorLearningPage(context);
            await page3.SelectNoForRPL();

            //update the original apprenticeship list with the updated details for further steps:
            listOfApprenticeship.Clear();
            listOfApprenticeship = listOfUpdatedApprenticeship.CloneApprenticeships();
            context.Set<List<Apprenticeship>>(listOfApprenticeship, ScenarioKeys.ListOfApprenticeship);
            listOfUpdatedApprenticeship.Clear();
        }

        [Then("Provider can approve the cohort")]
        public async Task ThenProviderCanApproveTheCohort()
        {
            var page = new ApproveApprenticeDetailsPage(context);
            await page.CanCohortBeApproved(true);
            string cohortStats = context.ScenarioInfo.Title.Contains("Drafts") ? "New request" : "Ready for review";
            await providerStepsHelper.ProviderApproveCohort(page, cohortStats);
        }



    }
}
