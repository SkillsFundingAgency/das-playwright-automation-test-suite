using SFA.DAS.Approvals.UITests.Project.Helpers.StepsHelper;
using SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;
using SFA.DAS.Approvals.UITests.Project.Pages.Provider;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.CreateAccount;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;
using SFA.DAS.ProviderPortal.UITests.Project.Helpers;


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


        [When(@"a (.*) Employer (.*) create cohort permission to a provider")]
        [Given(@"a (.*) Employer (.*) create cohort permission to a provider")]
        public async Task WhenALevyEmployerGrantCreateCohortPermissionToAProvider(EmployerType employerType, string permission)
        {
            EasAccountUser employerUser = context.GetUser<ProviderPermissionLevyUser>();
            bool isLevy = true;
            var providerConfig = context.GetProviderPermissionConfig<ProviderPermissionsConfig>();
            context.SetProviderConfig(providerConfig);
            
            if (employerType == EmployerType.NonLevy)
            {
                employerUser = context.GetUser<NonLevyUser>();
                isLevy = false;
            }

            await new EmployerHomePageStepsHelper(context).NavigateToEmployerApprenticeshipService(true);
            await new EmployerPortalLoginHelper(context).Login(employerUser, isLevy);

            if (permission.Equals("grant"))
            {
                await new DeleteProviderRelationinDbHelper(context).DeleteProviderRelation();
                await employerPermissionsStepsHelper.SetAllProviderPermissions(providerConfig);
            }
            else
            {
                await employerPermissionsStepsHelper.RemoveAllProviderPermission(providerConfig);
            }

        }
    
        [Then("Provider can Create Cohort")]
        public async Task ThenProviderCanCreateCohort()
        {
            var selectRoutePage = await GoToSelectOptionPage();

            Assert.IsTrue(await selectRoutePage.IsAddToAnExistingCohortOptionDisplayed(), "Validate Provider can add apprentice to existing cohorts");
            Assert.IsTrue(await selectRoutePage.IsCreateANewCohortOptionDisplayed(), "Validate Provider can create a new cohort");

            await selectRoutePage.ClearCacheAndReload();
            var providerHomePage = await providerHomePageStepsHelper.GoToProviderHomePage(false);            
            await providerHomePage.SignsOut();
        }

        [Then(@"the Provider (.*) create Reservations")]
        public async Task ThenTheProviderCanCreateReservations(string val)
        {
            if (val == "cannot")
            {
                Assert.IsFalse(await SearchEmployerToReserveNewFunding(), "Validate Employer name is not available for Provider to create new reservations");
            }                
            else
            {
                Assert.IsTrue(await SearchEmployerToReserveNewFunding(), "Validate Provider can search employer in create new reservations journey");
                await new ChooseAnEmployerPage(context).ClearCacheAndReload();
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
        }


    }
}
