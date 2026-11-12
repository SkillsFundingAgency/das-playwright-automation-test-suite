using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;
using static SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.VenueAndDelivery_ApprenticeshipUnitPage;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_AddAppUnit_Steps(ScenarioContext context)
{
    string standardName = string.Empty;

    [Then(@"^the provider is able to delete the new application unit$")]
    public async Task ThenTheProviderIsAbleToDeleteNewApplicationUnit()
    {
        var page = new ManageAnAppUnitPage(context, standardName);

        var page1 = await page.ClickDeleteAnApprenticeshipUnit();

        var page2 = await page1.DeleteAppUnit();

        await page2.VerifyAppUnitIsDeleted();
    }

    [Then(@"^the provider is able to view the new application unit$")]
    public async Task ThenTheProviderIsAbleToViewNewApplicationUnit()
    {
        var page = new ManageAnAppUnitPage(context, standardName);

        var page1 = await page.GoToManageYourAppUnitPage();

        var page2 = await page1.ManageAnAppUnitPage(standardName);
    }

    [Then(@"^the provider is able to edit the new application unit via confirmation page$")]
    public async Task ThenTheProviderIsAbleToEditNewApplicationUnit()
    {
        var page = new ApprenticeshipUnit_SavedPage(context);

        var page1 = await page.GoToTrainingAndVenues();

        var page2 = await page1.ManageAnAppUnitPage(standardName);

        var page3 = await page2.EditContactDetails();

        var page4 = await page3.Edit_ContactInformation_ApprenticeshipUnit(standardName);

        await page4.VerifyUpdatedWedsite();     

        var page5 = await page4.EditNationalProviderDetails();

        var page6 = await page5.EditDeliverAnyWhereInEnglandToYes(standardName);

        var page7 = await page6.EditNationalProviderDetails();

        var page8 = await page7.NoDeliverAnyWhereInEngland();

        var page9 = await page8.SelectRegionsAndConfirm(["North Yorkshire"], standardName);

        await page9.VerifyUpdatedRegion("North Yorkshire");
    }

    [Then(@"^the provider is able to add a new application unit with different contact$")]
    public async Task ThenTheProviderIsAbleToAddANewApplicationUnitWithDifferentContact()
    {
        standardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_Electricvehicle;

        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();
        
        var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

        var page3 = await page2.AccessAddApprenticeshipUnit();

        var page4 = await page3.SelectAStandardAndContinue(standardName);

        var page5 = await page4.YesStandardIsCorrectAndContinue_ApprenticeshipUnit();

        var page6 = await page5.NoDontUseExistingContactDetails_ApprenticeshipUnit();

        var page7 = await page6.AddAll_ContactInformation_ApprenticeshipUnit();

        var page8 = await page7.ConfirmAllLocations_AddApprenticeshipUnit();

        var page9 = await page8.ChooseTheVenueDeliveryAndContinueToDeliver_ApprenticeshipUnit();

        var page10 = await page9.YesDeliverAnyWhereInEngland_AddApprenticeshipUnit(standardName);

        await page10.Save_NewApprenticeshipUnit_Continue();
    }

    [Then(@"^the provider can add delivery forecast$")]
    public async Task ThenTheProviderCanAddADeliveryForecast()
    {
       var page = new ApprenticeshipUnit_SavedPage(context);

       var page1 = await page.GoToProviderHomePage();

       var page2 = await page1.NavigateToYourStandardsAndTrainingVenuesPage();

       var page3 = await page2.AccessDeliveryForecast();

       var page4 = await page3.SelectAppUnit(standardName);

       await page4.EnterAppUnitForecast();
    }


    [Then(@"^the provider is able to add a new application unit with regions$")]
    public async Task ThenTheProviderIsAbleToAddANewApplicationUnit()
    {
        standardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_Electricvehicle;

        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();
        
        var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

        var page3 = await page2.AccessAddApprenticeshipUnit();

        var page4 = await page3.SelectAStandardAndContinue(standardName);

        var page5 = await page4.YesStandardIsCorrectAndContinue_ApprenticeshipUnit();

        var page6 = await page5.YesUseExistingContactDetails_ApprenticeshipUnit();

        var page7 = await page6.Add_ContactInformation_ApprenticeshipUnit();

        var page8 = await page7.ConfirmAtEmployersLocations_AddApprenticeshipUnit();

        var page9 = await page8.NoDeliverAnyWhereInEngland();

        var page10 = await page9.SelectRegionsAndContinue(["Derby", "Derbyshire", "Lincolnshire"], standardName);

        await page10.Save_NewApprenticeshipUnit_Continue();
    }
   
}