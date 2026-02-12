using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_AddAndDelete_Steps(ScenarioContext context)
{
    private string StandardName;

    [When(@"the provider is able to add the standard delivered in one of the training locations")]
    public async Task WhenTheProviderIsAbleToAddTheStandardDeliveredInOneOfTheTrainingLocations()
    {
        StandardName = context.Get<ManagingStandardsDataHelpers>().Standard_ActuaryLevel7;

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

    [When("the provider is able to add the standard using new contact details")]
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

    [When("the provider is able to add the standard delivered nationally")]
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

    [When(@"the provider is able to delete the standard")]
    public async Task WhenTheProviderIsAbleToDeleteTheStandard()
    {
        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessActuaryLevel7(StandardName);

        var page2 = await page1.ClickDeleteAStandard();

        await page2.DeleteStandard();
    }
}
