using Azure;
using Microsoft.Playwright;
using Polly;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using SFA.DAS.ManagingStandards.UITests.Project.Helpers;
using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FAT_ProviderSteps(ScenarioContext context)
    {
        private string StandardName;

        [When("^the provider adds the Electrical Fitting course to the App Units list$")]
        public async Task WhenTheProviderAddsTheElectricalFittingCourseToTheAppUnitsList()
        {
            StandardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_ElectricalFitting;

            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();
        
            var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

            var page3 = await page2.AccessAddApprenticeshipUnit();

            var page4 = await page3.SelectAStandardAndContinue(StandardName);

            var page5 = await page4.YesStandardIsCorrectAndContinue_ApprenticeshipUnit();

            var page6 = await page5.YesUseExistingContactDetails_ApprenticeshipUnit();

            var page7 = await page6.Add_ContactInformation_ApprenticeshipUnit();

            var page8 = await page7.ConfirmOnline_AddApprenticeshipUnit(StandardName);

            await page8.Save_NewApprenticeshipUnit_Continue();  
        }

        [When("^the provider updates the course to be available at learners and provider location$")]
        public async Task WhenTheProviderUpdatesTheCourseToBeAvailableAtLearnersAndProviderLocation()
        {
            StandardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_ElectricalFitting;

            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();
        
            var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

            var page3 = await page2.ManageAnAppUnitPage(StandardName);

            var page4 = await page3.EditFromOnlineTo();

            var page5 = await page4.UpdateToEmpAndProLocationOnly_AprenticeshipUnit();

            var page6 = await page5.ChooseTheVenueDeliveryAndContinueToDeliver_ApprenticeshipUnit();

            await page6.EditDeliverAnyWhereInEnglandToYes(StandardName);
        }


        [When("^the provider deletes the Electrical Fitting course from the App Units list$")]
        public async Task WhenTheProviderDeletesTheElectricalFittingCourseFromTheAppUnitsList()
        {
            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

            var page3 = await page2.ManageAnAppUnitPage(StandardName);

            var page4 = await page3.ClickDeleteAnApprenticeshipUnit();

            var page5 = await page4.DeleteAppUnit();

            await page5.VerifyAppUnitIsDeleted();
        }

        [When("^the provider adds the Craft Plasterer course to the standards list$")]
        public async Task WhenTheProviderAddsTheCraftPlastererCourseToTheStandardsList()
        {
            StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_CraftPlastererlevel;

            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessStandards_Apprenticeships();

            var page3 = await page2.AccessAddStandard();

            var page4 = await page3.SelectAStandardAndContinue(StandardName);

            var page5 = await page4.YesStandardIsCorrectAndContinue();

            var page6 = await page5.YesUseExistingContactDetails();

            var page7 = await page6.Add_ContactInformation();

            var page8 = await page7.ConfirmAtOneofYourTrainingLocations_AddStandard();

            var page9 = await page8.AccessSeeANewTrainingVenue_AddStandard();

            var page10 = await page9.ChooseTheVenueDeliveryAndContinue();

            var page11 = await page10.Save_NewTrainingVenue_Continue(StandardName);

            await page11.Save_NewStandard_Continue();
        }

        [When("^the provider deletes the Craft Plasterer course from the standards list$")]
        public async Task WhenTheProviderDeletesTheCraftPlastererCourseFromTheStandardsList()
        {
            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessStandards_Apprenticeships();

            var page3 = await page2.AccessActuaryLevel7(StandardName);

            var page4 = await page3.ClickDeleteAStandard();

            await page4.DeleteStandard();
        }
    }
}
