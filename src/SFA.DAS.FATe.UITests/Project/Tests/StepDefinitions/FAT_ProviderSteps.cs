using Azure;
using Microsoft.Playwright;
using Polly;
using SFA.DAS.FATe.UITests.Project.Tests.Pages;
using SFA.DAS.ManagingStandards.UITests.Project.Helpers;
using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;


namespace SFA.DAS.FATe.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FAT_ProviderSteps(ScenarioContext context)
    {
        private string StandardName;

        [When("the provider adds the Craft Plasterer course to the standards list")]
        public async Task WhenTheProviderAddsTheCraftPlastererCourseToTheStandardsList()
        {
            StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_CraftPlastererlevel;

            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessStandards();

            var page3 = await page2.AccessAddStandard();

            var page4 = await page3.SelectAStandardAndContinue(StandardName);

            var page5 = await page4.YesStandardIsCorrectAndContinue();

            var page6 = await page5.Add_ContactInformation();

            var page7 = await page6.ConfirmAtOneofYourTrainingLocations_AddStandard();

            var page8 = await page7.AccessSeeANewTrainingVenue_AddStandard();

            var page9 = await page8.ChooseTheVenueDeliveryAndContinue();

            var page10 = await page9.Save_NewTrainingVenue_Continue(StandardName);

            await page10.Save_NewStandard_Continue();
        }

        [When("the provider deletes the Craft Plasterer course from the standards list")]
        public async Task WhenTheProviderDeletesTheCraftPlastererCourseFromTheStandardsList()
        {
            var page = new ManagingStandardsProviderHomePage(context);

            var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

            var page2 = await page1.AccessStandards();

            var page3 = await page2.AccessActuaryLevel7(StandardName);

            var page4 = await page3.ClickDeleteAStandard();

            await page4.DeleteStandard();
        }
    }
}
