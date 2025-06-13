using SFA.DAS.ManagingStandards.UITests.Project.Tests.Pages;

namespace SFA.DAS.ManagingStandards.UITests.Project.Tests.Steps;

[Binding]
public class MS_TrainingVenue_Steps(ScenarioContext context)
{
    [Then(@"the provider is able to add a new training venue")]
    public async Task ThenTheProviderIsAbleToAddANewTrainingVenue()
    {
        var page = new ManagingStandardsProviderHomePage(context);

        var page1 = await page.NavigateToYourStandardsAndTrainingVenuesPage();

        var page2 = await page1.AccessTrainingLocations();

        var page3 = await page2.AccessAddANewTrainingVenue();

        var page4 = await page3.EnterPostcodeAndContinue();

        var page5 = await page4.AddVenueDetailsAndSubmit();

    }

    [Then(@"the provider is able to update the new training venue")]
    public async Task ThenTheProviderIsAbleToUpdateTheNewTrainingVenue()
    {
        var page = new TrainingVenuesPage(context);

        var page1 = await page.AccessNewTrainingVenue_Added();

        var page2 = await page1.Click_ChangeVenueName();

        await page2.UpdateVenueDetailsAndSubmit();
    }
}