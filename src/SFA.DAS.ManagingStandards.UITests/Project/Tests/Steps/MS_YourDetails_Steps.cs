using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;
using SFA.DAS.ProviderLogin.Service.Project.Helpers;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_YourDetails_Steps(ScenarioContext context)
{
    [Given(@"the provider logs into portal")]
    public async Task GivenTheProviderLogsIntoPortal() => await new ProviderHomePageStepsHelper(context).GoToProviderHomePage(false);

    [Then(@"the provider verifies organisation details")]
    public async Task ThenTheProviderVerifiesOrganisationDetails()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessTrainingLocations();

        var page3 = await page2.NavigateBackToReviewYourDetails();

        await page3.AccessStandards();
    }

    [Then(@"the provider verifies provider overview")]
    public async Task ThenTheProviderVerifiesProviderOverview()
    {
        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.ReturnToYourStandardsAndTrainingVenues();

        var page2 = await page1.AccessProviderOverview();

        var page3 = await page2.NavigateBackToReviewYourDetails();

        await page3.AccessStandards();
    }

    [Then(@"the provider updates contact details")]
    public async Task ThenTheProviderUpdatesContactDetails()
    {
        var page = new ManageTheStandardsYouDeliverPage(context);

        var page1 = await page.AccessPodiatrist();

        var page2 = await page1.UpdateTheseContactDetails();

        await page2.UpdateContactInformation();
    }

    [When(@"the provider is able to approve regulated standard")]
    public async Task WhenTheProviderIsAbleToApproveRegulatedStandard()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessStandards();

        var page3 = await page2.AccessPodiatrist();

        var page4 = await page3.AccessApprovedByRegulationOrNot();

        var page5 = await page4.ApproveStandard_FromStandardsPage();

    }

    [Then(@"the provider is able to disapprove regulated standard")]
    public async Task ThenTheProviderIsAbleToDisapproveRegulatedStandard()
    {
        var page = new ManageAStandard_TeacherPage(context);

        var page1 = await page.AccessApprovedByRegulationOrNot();

        var page2 = await page1.DisApproveStandard();

        var page3 = await page2.ContinueToTeacher_ManageStandardPage();

        var page4 = await page3.Return_StandardsManagement();

        await page4.VerifyOrangeMoreDetailsNeededTagForStandardAsync("Podiatrist (level 6)", shouldExist: true);

    }

    [When(@"the provider is able to change the standard delivered in one of the training locations")]
    public async Task WhenTheProviderIsAbleToChangeTheStandardDeliveredInOneOfTheTrainingLocations()
    {
        var page = new ManageAStandard_TeacherPage(context);

        var page1 = await page.AccessWhereYouWillDeliverThisStandard();

        var page2 = await page1.ConfirmAtOneofYourTrainingLocations();

        await page2.ConfirmVenueDetailsAndDeliveryMethod_AtOneOFYourTrainingLocation();
    }

    [When(@"the provider is able to add the training locations")]
    public async Task WhenTheProviderIsAbleToAddTheTrainingLocations()
    {
        var page = new ManageAStandard_TeacherPage(context);

        var page1 = await page.AccessEditTrainingLocations();

        var page2 = await page1.AccessSeeTrainingVenue();

        var page3 = await page2.ChooseTheVenueDeliveryAndContinue();

        await page3.NavigateBackToStandardPage();
    }

    [When(@"the provider is able to change the standard delivered at an employers location national provider")]
    public async Task WhenTheProviderIsAbleToChangeTheStandardDeliveredAtAnEmployersLocationNationalProvider()
    {
        var page = new ManageAStandard_TeacherPage(context);

        var page1 = await page.AccessWhereYouWillDeliverThisStandard();

        var page2 = await page1.ConfirmAtAnEmployersLocation();

        await page2.YesDeliverAnyWhereInEngland();
    }

    [When(@"the provider is able to change the standard delivered in both not a national provider")]
    public async Task WhenTheProviderIsAbleToChangeTheStandardDeliveredInBothNotANationalProvider()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessStandards();

        var page3 = await page2.AccessPodiatrist();

        var page4 = await page3.AccessWhereYouWillDeliverThisStandard();

        var page5 = await page4.ConfirmStandardWillDeliveredInBoth();

        var page6 = await page5.ConfirmVenueDetailsAndDeliveryMethod_AtBoth();

        var page7 = await page6.NoDeliverAnyWhereInEngland();

        await page7.SelectDerbyRutlandRegionsAndConfirm();
    }

    [When(@"the provider is able to edit the regions")]
    public async Task WhenTheProviderIsAbleToEditTheRegions()
    {
        var page = new ManageAStandard_TeacherPage(context);

        var page1 = await page.AccessWhereYouWillDeliverThisStandard();

        var page2 = await page1.ConfirmAtAnEmployersLocation();

        var page3 = await page2.NoDeliverAnyWhereInEngland();

        var page4 = await page3.SelectDerbyRutlandRegionsAndConfirm();

        var page5 = await page4.AccessEditTheseRegions();

        await page5.EditRegionsAddLutonEssexAndConfirm();
    }
}
