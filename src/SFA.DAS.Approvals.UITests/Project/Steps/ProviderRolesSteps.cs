using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers;
using SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel;
using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Pages;
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
    internal class ProviderRolesSteps
    {
        private readonly ScenarioContext context;
        private readonly ProviderHomePageStepsHelper providerHomePageStepsHelper;
        private readonly ProviderStepsHelper providerStepsHelper;
        private readonly DbSteps dbSteps;
        private readonly SldIlrSubmissionSteps sldIlrSubmissionSteps;

        public ProviderRolesSteps(ScenarioContext _context)
        {
            context = _context;
            providerHomePageStepsHelper = new ProviderHomePageStepsHelper(context);
            providerStepsHelper = new ProviderStepsHelper(context);
            dbSteps = new DbSteps(context);
            sldIlrSubmissionSteps = new SldIlrSubmissionSteps(context);
        }

        [Then("the user can view apprentice details from items under section: \"(.*)\"")]
        public async Task ThenTheUserCanViewApprenticeDetailsFromItemsUnderSection(string sectionName)
        {
            var apprenticeRequests_ProviderPage = new ApprenticeRequests_ProviderPage(context);
            IPageWithABackLink page;

            switch (sectionName)
            {
                case "Ready for review":
                    await apprenticeRequests_ProviderPage.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.ReadyForReview);
                    page = await apprenticeRequests_ProviderPage.OpenEditableCohortAsync(null);
                    break;
                case "With employers":
                    await apprenticeRequests_ProviderPage.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.WithEmployers);
                    page = await apprenticeRequests_ProviderPage.OpenNonEditableCohortAsync(null);
                    break;
                case "Drafts":
                    await apprenticeRequests_ProviderPage.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.Drafts);
                    page = await apprenticeRequests_ProviderPage.OpenEditableCohortAsync(null);
                    break;
                case "With transfer sending employers":
                    await apprenticeRequests_ProviderPage.NavigateToBingoBoxAndVerifyCohortExists(ApprenticeRequests.WithTransferSendingEmployers);
                    page = await apprenticeRequests_ProviderPage.OpenNonEditableCohortAsync(null);
                    break;
                default:
                    throw new ArgumentException($"Unknown section name: {sectionName}");

            }
            await page.ClickOnBackLinkAsync();
        }

        [Then("the user can create a cohort by selecting learners from ILR")]
        public async Task ThenTheUserCanCreateACohortBySelectingLearnersFromILR()
        {
            await dbSteps.FindAvailableLearner();
            var page = await providerStepsHelper.GoToSelectApprenticeFromILRPage(false);
            await providerStepsHelper.AddFirstApprenticeFromILRList(page);
        }


        [Then("the user can edit email address of the apprentice before approval")]
        public async Task ThenTheUserCanEditEmailAddressOfTheApprenticeBeforeApprovalAsync()
        {
            var apprentice = context.GetValue<List<Apprenticeship>>().FirstOrDefault();
            var page = await new ApproveApprenticeDetailsPage(context).ClickOnEditApprenticeLink(apprentice.ApprenticeDetails.FullName);
            var page1 = await page.UpdateEmail(apprentice.ApprenticeDetails.Email + ".uk");
            var page3 = await page1.SelectNoForRPL();
        }

        [Then("the user can send a cohort to employer")]
        public async Task ThenTheUserCanSendACohortToEmployer()
        {
            await new ApproveApprenticeDetailsPage(context).VerifyCohortCanBeApproved();
        }

        [Then("the user can delete an apprentice in a cohort")]
        public async Task ThenTheUserCanDeleteAnApprenticeInACohort()
        {
            var page = await new ApproveApprenticeDetailsPage(context).ClickOnDeleteApprenticeLink("");
            var page1 = await page.ConfirmDeletion();
            await page1.VerifyBanner("Apprentice record deleted");
        }

        [Then("the user can delete a cohort")]
        public async Task ThenTheUserCanDeleteACohort()
        {
            var page = await new ApproveApprenticeDetailsPage(context).ClickOnDeleteCohortLink();
            await page.ConfirmDeletion();
        }

        [Then("the user cannot start add apprentice journey")]
        public async Task ThenTheUserCannotStartAddApprenticeJourney()
        {
            await new ApprenticeRequests_ProviderPage(context).ClickOnNavBarLinkAsync("Home");
            await new ProviderHomePage(context).AddNewApprentices();
            var page = new ProviderAccessDeniedPage(context);
            await page.VerifyPage();
            await page.GoBackToTheServiceHomePage();
        }

        [Then("the user cannot edit apprentice details in an existing cohort")]
        public async Task ThenTheUserCannotEditApprenticeDetailsInAnExistingCohort()
        {
            var page = await new ProviderHomePage(context).GoToApprenticeRequestsPage();
            await page.OpenLastCohortFromTheList();
            var page1 = await new ApproveApprenticeDetailsPage(context).TryOpenLink("Edit");
            await page1.NavigateBrowserBack();
        }

        [Then("the user cannot add another apprentice to a cohort")]
        public async Task ThenTheUserCannotAddAnotherApprenticeToACohort()
        {
            var page = await new ApproveApprenticeDetailsPage(context).ClickOnAddAnotherApprenticeLink_ToSelectEntryMthodPage();
            await page.SelectOptionToAddApprenticeFromILRAndContinue();
            var page1 = new ProviderAccessDeniedPage(context);
            await page1.VerifyPage();
            await page1.NavigateBrowserBack();
            await page.NavigateBrowserBack();
        }

        [Then("the user cannot delete an apprentice in an existing cohort")]
        public async Task ThenTheUserCannotDeleteAnApprenticeInAnExistingCohort()
        {
            var page1 = await new ApproveApprenticeDetailsPage(context).TryOpenLink("Delete");
            await page1.NavigateBrowserBack();
        }

        [Then("the user cannot delete an existing cohort")]
        public async Task ThenTheUserCannotDeleteAnExistingCohort()
        {
            var page1 = await new ApproveApprenticeDetailsPage(context).TryOpenLink("Delete this cohort");
            await page1.NavigateBrowserBack();
        }

        [Then("the user cannot send an existing cohort to employer")]
        public async Task ThenTheUserCannotSendAnExistingCohortToEmployer()
        {
            await new ApproveApprenticeDetailsPage(context).SelectFirstRadioButtonAndSubmit();
            var page = new ProviderAccessDeniedPage(context);
            await page.VerifyPage();
            await page.GoBackToTheServiceHomePage();
        }

    }
}
