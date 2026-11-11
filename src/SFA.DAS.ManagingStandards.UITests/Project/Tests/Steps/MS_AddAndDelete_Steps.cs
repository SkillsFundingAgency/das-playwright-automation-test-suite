using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages.AddManageStandards;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_AddAndDelete_Steps(ScenarioContext context)
{
    private string StandardName;

    [When(@"^the provider is able to add the standard delivered in one of the training locations$")]
    public async Task WhenTheProviderIsAbleToAddTheStandardDeliveredInOneOfTheTrainingLocations()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_ActuaryLevel7;

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

    [When("^the provider is able to add the standard using new contact details$")]
    public async Task WhenTheProviderIsAbleToAddTheStandardUsingNewContactDetails()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_ActuaryLevel7;

        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessAddStandard();

        var page2 = await page1.SelectAStandardAndContinue(StandardName);

        var page3 = await page2.YesStandardIsCorrectAndContinue();

        var page4 = await page3.NoDontUseExistingContactDetails();

        var page5 = await page4.AddNewContactInformation();

        var page6 = await page5.ConfirmAtOneofYourTrainingLocations_AddStandard();

        var page7 = await page6.AccessSeeANewTrainingVenue_AddStandard();

        var page8 = await page7.ChooseTheVenueDeliveryAndContinue();

        var page9 = await page8.Save_NewTrainingVenue_Continue(StandardName);

        await page9.Save_NewStandard_Continue();
    }

    [When("^the provider is able to add the standard delivered nationally$")]
    public async Task WhenTheProviderIsAbleToAddTheStandardDeliveredNationally()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_ActuaryLevel7;

        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessAddStandard();

        var page2 = await page1.SelectAStandardAndContinue(StandardName);

        var page3 = await page2.YesStandardIsCorrectAndContinue();

        var page4 = await page3.NoDontUseExistingContactDetails();

        var page5 = await page4.AddNewContactInformation();

        var page6 = await page5.ConfirmAtAnEmployersLocation_AddStandard();

        var page7 = await page6.YesDeliverAnyWhereInEngland_AddStandard(StandardName);

        await page7.Save_NewStandard_Continue();
    }

    [When(@"^the provider is able to delete the standard$")]
    public async Task WhenTheProviderIsAbleToDeleteTheStandard()
    {
        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessActuaryLevel7(StandardName);

        var page2 = await page1.ClickDeleteAStandard();

        await page2.DeleteStandard();
    }
    [When(@"^the provider is able to delete the ApprenticeshipUnit$")]
    public async Task WhenTheProviderIsAbleToDeleteTheApprenticeshipUnit()
    {
        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessActuaryLevel7(StandardName);

        var page2 = await page1.ClickDeleteAStandard();

        await page2.DeleteStandard();
    }

    [When(@"^the provider is able to add the ApprenticeshipUnit delivered in one of the training locations$")]
    public async Task WhenTheProviderIsAbleToAddTheApprenticeshipUnitDeliveredInOneOfTheTrainingLocations()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_AIleadership;

        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

        var page3 = await page2.AccessAddApprenticeshipUnit();

        var page4 = await page3.SelectAStandardAndContinue(StandardName);

        var page5 = await page4.YesStandardIsCorrectAndContinue_ApprenticeshipUnit();

        var page6 = await page5.YesUseExistingContactDetails_ApprenticeshipUnit();

        var page7 = await page6.Add_ContactInformation_ApprenticeshipUnit();

        var page8 = await page7.ConfirmAtOneofYourTrainingLocations_AddApprenticeshipUnit();

        var page9 = await page8.ChooseTheVenueDeliveryAndContinue_ApprenticeshipUnit(StandardName);

        await page9.Save_NewApprenticeshipUnit_Continue();
    }

    [When(@"^the provider is able to add the ApprenticeshipUnit delivered in employers locations$")]
    public async Task WhenTheProviderIsAbleToAddTheApprenticeshipUnitDeliveredInEmployersLocations()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_AIleadership;

        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();
        
        var page2 = await page1.AccessStandards_ApprenticeshipsUnits();

        var page3 = await page2.AccessAddApprenticeshipUnit();

        var page4 = await page3.SelectAStandardAndContinue(StandardName);

        var page5 = await page4.YesStandardIsCorrectAndContinue_ApprenticeshipUnit();

        var page6 = await page5.YesUseExistingContactDetails_ApprenticeshipUnit();

        var page7 = await page6.Add_ContactInformation_ApprenticeshipUnit();

        var page8 = await page7.ConfirmAtEmployersLocations_AddApprenticeshipUnit();

        var page9 = await page8.YesDeliverAnyWhereInEngland_AddApprenticeshipUnit(StandardName);

        await page9.Save_NewApprenticeshipUnit_Continue();
    }

    [When(@"^the provider is able to add the ApprenticeshipUnit delivered online$")]
    public async Task WhenTheProviderIsAbleToAddTheApprenticeshipUnitDeliveredOnline()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Apprenticeshipunit_AIleadership;

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
}
