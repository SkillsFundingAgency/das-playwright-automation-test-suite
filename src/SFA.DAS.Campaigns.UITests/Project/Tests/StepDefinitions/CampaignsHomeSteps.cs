namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions;

[Binding]
public class CampaignsHomeSteps(ScenarioContext context)
{
    private readonly CampaignsStepsHelper _stepsHelper = new(context);

    [Given(@"the user navigates to (?:the )?Home page$")]
    [Given(@"the user navigates to Home page and verifies the content$")]
    public async Task GivenTheUserNavigatesToTheHomePage() =>
        await _stepsHelper.GoToCampaingnsHomePage();

    [When(@"the user clicks on the homepage card ""(.*)""$")]
    public async Task WhenTheUserClicksOnTheHomepageCard(string cardName)
    {
        var page = await _stepsHelper.GoToCampaingnsHomePage();

        await page.NavigateToCard(cardName);
    }
}