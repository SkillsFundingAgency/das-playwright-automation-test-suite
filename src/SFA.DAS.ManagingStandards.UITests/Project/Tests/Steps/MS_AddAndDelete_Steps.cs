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

    [When(@"the provider is able to delete the standard")]
    public async Task WhenTheProviderIsAbleToDeleteTheStandard()
    {
        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessActuaryLevel7(StandardName);

        var page2 = await page1.ClickDeleteAStandard();

        await page2.DeleteStandard();
    }
}
