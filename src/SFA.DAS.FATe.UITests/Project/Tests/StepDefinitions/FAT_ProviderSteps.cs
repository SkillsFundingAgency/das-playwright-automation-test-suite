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

        [When("the provider adds the Electrical Fitting course to the App Units list")]
        public async Task WhenTheProviderAddsTheElectricalFittingCourseToTheAppUnitsList()
        {
            StandardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_ElectricalFitting;

            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessTrainingTypesPage();
        
            var page3 = await page2.AccessStandards_ApprenticeshipsUnits();

            var page4 = await page3.AccessAddApprenticeshipUnit();

            var page5 = await page4.SelectAStandardAndContinue(StandardName);

            var page6 = await page5.YesStandardIsCorrectAndContinue_ApprenticeshipUnit();

            var page7 = await page6.YesUseExistingContactDetails_ApprenticeshipUnit();

            var page8 = await page7.Add_ContactInformation_ApprenticeshipUnit();

            var page9 = await page8.ConfirmOnline_AddApprenticeshipUnit(StandardName);

            await page9.Save_NewApprenticeshipUnit_Continue();  
        }


        [When("the provider deletes the Electrical Fitting course from the App Units list")]
        public async Task WhenTheProviderDeletesTheElectricalFittingCourseFromTheAppUnitsList()
        {
            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessTrainingTypesPage();

            var page3 = await page2.AccessStandards_ApprenticeshipsUnits();

            var page4 = await page3.ManageAnAppUnitPage(StandardName);

            var page5 = await page4.ClickDeleteAnApprenticeshipUnit();

            var page6 = await page5.DeleteAppUnit();

            await page6.VerifyAppUnitIsDeleted();
        }

        [When("the provider adds the Craft Plasterer course to the standards list")]
        public async Task WhenTheProviderAddsTheCraftPlastererCourseToTheStandardsList()
        {
            StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_CraftPlastererlevel;

            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessTrainingTypesPage();

            var page3 = await page2.AccessStandards_Apprenticeships();

            var page4 = await page3.AccessAddStandard();

            var page5 = await page4.SelectAStandardAndContinue(StandardName);

            var page6 = await page5.YesStandardIsCorrectAndContinue();

            var page7 = await page6.YesUseExistingContactDetails();

            var page8 = await page7.Add_ContactInformation();

            var page9 = await page8.ConfirmAtOneofYourTrainingLocations_AddStandard();

            var page10 = await page9.AccessSeeANewTrainingVenue_AddStandard();

            var page11 = await page10.ChooseTheVenueDeliveryAndContinue();

            var page12 = await page11.Save_NewTrainingVenue_Continue(StandardName);

            await page12.Save_NewStandard_Continue();
        }

        [When("the provider deletes the Craft Plasterer course from the standards list")]
        public async Task WhenTheProviderDeletesTheCraftPlastererCourseFromTheStandardsList()
        {
            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessTrainingTypesPage();

            var page3 = await page2.AccessStandards_Apprenticeships();

            var page4 = await page3.AccessActuaryLevel7(StandardName);

            var page5 = await page4.ClickDeleteAStandard();

            await page5.DeleteStandard();
        }
    }
}
