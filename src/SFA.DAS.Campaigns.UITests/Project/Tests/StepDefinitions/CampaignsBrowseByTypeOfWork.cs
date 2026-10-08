using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Apprentices;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions;

[Binding]
public class CampaignsBrowseByTypeOfWork(ScenarioContext context)
{
    private readonly CampaignsStepsHelper _stepsHelper = new(context);
    private BrowseApprenticeshipPage _browsePage;

    [Given(@"the user is on the Browse by type of work page")]
    public async Task GivenTheUserIsOnTheBrowseByTypeOfWorkPage()
    {
        var apprenticeHubPage = await _stepsHelper.GoToApprenticeshipHubPage();
        _browsePage = await apprenticeHubPage.NavigateToBrowseByTypeOfWork();
    }

    [When(@"the user selects the ""(.*)"" sector")]
    public async Task WhenTheUserSelectsTheSector(string sectorName)
    {
        await _browsePage.NavigateToSectorCard(sectorName);
    }

    [Then(@"the user should be directed to the ""(.*)"" page")]
    public async Task ThenTheUserShouldBeDirectedToThePage(string sectorName)
    {
        await _browsePage.VerifyHeading(sectorName);
    }
}