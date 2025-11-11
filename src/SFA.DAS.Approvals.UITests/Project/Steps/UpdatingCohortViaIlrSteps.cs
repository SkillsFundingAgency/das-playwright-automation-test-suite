using Dynamitey;
using SFA.DAS.Approvals.UITests.Project.Helpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderLogin.Service.Project.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    public class UpdatingCohortViaIlrSteps
    {
        private readonly ScenarioContext context;
        private readonly CommonStepsHelper commonStepsHelper;
        private readonly ProviderHomePageStepsHelper providerHomePageStepsHelper;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly DbSteps dbSteps;
        private readonly LearnerDataOuterApiSteps learnerDataOuterApiSteps;

        public UpdatingCohortViaIlrSteps(ScenarioContext _context)
        {
            context = _context;
            commonStepsHelper = new CommonStepsHelper(context);
            providerHomePageStepsHelper = new ProviderHomePageStepsHelper(context);
            providerStepsHelper = new ProviderStepsHelper(context);
            dbSteps = new DbSteps(context);
            learnerDataOuterApiSteps = new LearnerDataOuterApiSteps(context);
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

            ApprenticeRequests_ProviderPage apprenticeRequests = new ApprenticeRequests_ProviderPage(context);
            await apprenticeRequests.NavigateToBingoBox(ApprenticeRequests.Drafts);
            var page = await apprenticeRequests.OpenEditableCohortAsync(cohortRef);

            await page.VerifyBanner(bannerTitle, bannerMessage);
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
            await providerStepsHelper.ProviderApproveCohort(page);
        }



    }
}
