using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Steps
{
    [Binding]
    internal class ProviderPermissionsSteps
    {
        private readonly ScenarioContext context;
        private readonly EmployerStepsHelper employerStepsHelper;
        private readonly EmployerPermissionsStepsHelper employerPermissionsStepsHelper;
        private readonly ProviderHomePageStepsHelper providerHomePageStepsHelper;
        public ProviderPermissionsSteps(ScenarioContext _context)
        {
            context = _context;
            employerStepsHelper = new EmployerStepsHelper(context);
            employerPermissionsStepsHelper = new EmployerPermissionsStepsHelper(context);
            providerHomePageStepsHelper = new ProviderHomePageStepsHelper(context);
        }


        [When("the employer grants provider permissions to add apprentices")]
        public async Task WhenTheEmployerGrantsProviderPermissionsToAddApprentices()
        {
            var providerConfig = context.GetProviderConfig<ProviderConfig>();

            await employerStepsHelper.LogOutThenLogbackIn();
            await employerPermissionsStepsHelper.UpdateProviderPermission(providerConfig, (AddApprenticePermissions.YesAddApprenticeRecords, RecruitApprenticePermissions.NoToRecruitApprentices));
        }


        [Given("Employer grant create cohort permission to a provider")]
        public async Task GivenEmployerGrantCreateCohortPermissionToAProvider()
        {
            var providerConfig = context.GetProviderPermissionConfig<ProviderPermissionsConfig>();
            context.SetProviderConfig(providerConfig);
            var employerUser = context.GetUser<ProviderPermissionLevyUser>();

            await new EmployerPortalLoginHelper(context).Login(employerUser, true);
            await new DeleteProviderRelationinDbHelper(context).DeleteProviderRelation();
            await employerPermissionsStepsHelper.SetAllProviderPermissions(providerConfig);

        }

        [Given(@"a (.*) Employer grant create cohort permission to a provider")]
        public async Task GivenAEmployerGrantCreateCohortPermissionToAProvider(EmployerType employerType)
        {
            var providerConfig = context.GetProviderPermissionConfig<ProviderPermissionsConfig>();
            context.SetProviderConfig(providerConfig);
            EasAccountUser employerUser = (employerType == EmployerType.Levy) ? context.GetUser<ProviderPermissionLevyUser>() : context.GetUser<NonLevyUser>();

            await new EmployerPortalLoginHelper(context).Login(employerUser, true);
            await new DeleteProviderRelationinDbHelper(context).DeleteProviderRelation();
            await employerPermissionsStepsHelper.SetAllProviderPermissions(providerConfig);
        }



        [When("Employer revoke create cohort permission to a provider")]
        public async Task WhenEmployerRevokeCreateCohortPermissionToAProvider()
        {
            var providerConfig = context.GetProviderConfig<ProviderConfig>();

            await new EmployerHomePageStepsHelper(context).NavigateToEmployerApprenticeshipService(true);
            await employerPermissionsStepsHelper.RemoveAllProviderPermission(providerConfig);
        }


        [Then("Provider can Create Cohort")]
        public async Task ThenProviderCanCreateCohort()
        {
            var selectRoutePage = await GoToSelectOptionPage();

            Assert.IsTrue(await selectRoutePage.IsAddToAnExistingCohortOptionDisplayed(), "Validate Provider can add apprentice to existing cohorts");
            Assert.IsTrue(await selectRoutePage.IsCreateANewCohortOptionDisplayed(), "Validate Provider can create a new cohort");

            var providerHomePage = await providerHomePageStepsHelper.GoToProviderHomePage(false);
            await providerHomePage.SignsOut();
        }

        [Then(@"the Provider (.*) create Reservations")]
        public async Task ThenTheProviderCanCreateReservations(string val)
        {
            if (val.Equals("cannot"))
            {
                await Task.Delay(10000); // Adding delay to avoid permissions-synching issue
                Assert.IsFalse(await SearchEmployerToReserveNewFunding(), "Validate Employer name is not available for Provider to create new reservations");
            }                
            else
            {
                Assert.IsTrue(await SearchEmployerToReserveNewFunding(), "Validate Provider can search employer in create new reservations journey");
                var providerHomePage = await providerHomePageStepsHelper.GoToProviderHomePage(false);
                await providerHomePage.SignsOut();
            }                
            
        }



        [Then("Provider cannot Create Cohort")]
        public async Task ThenProviderCannotCreateCohort()
        {
            var selectRoutePage = await GoToSelectOptionPage();

            await selectRoutePage.AssertProviderPermissionsMsg();
            Assert.IsTrue(await selectRoutePage.IsAddToAnExistingCohortOptionDisplayed(), "Validate Provider can add apprentice to existing cohorts");
            Assert.IsFalse(await selectRoutePage.IsCreateANewCohortOptionDisplayed(), "Validate Provider cannot create a new cohort");

        }

        private async Task<AddApprenticeDetails_SelectJourneyPage> GoToSelectOptionPage()
        {
            var providerHomePage = await providerHomePageStepsHelper.GoToProviderHomePage(true);
            var selectJourneyPage = await providerHomePage.GotoSelectJourneyPage();
            return await new AddApprenticeDetails_EntryMothodPage(context).SelectOptionToApprenticesFromILR();
        }

        private async Task<bool> SearchEmployerToReserveNewFunding()
        {
            var providerHomePage = await providerHomePageStepsHelper.GoToProviderHomePage(true);
            var reserveFundingForNonLevyEmployersPage = await providerHomePage.GoToProviderGetFunding();
            await reserveFundingForNonLevyEmployersPage.ClickOnReserveFundingButton();
            
            var agreementId = context.GetUser<NonLevyUser>().UserCreds.AccountDetails[0].AleAgreementid;
            var chooseAnEmployerPage = new  ChooseAnEmployerPage(context);            
            return await chooseAnEmployerPage.EmployerExistsInTheList(agreementId);
            await chooseAnEmployerPage.ClearCacheAndReload();
        }


    }
}
