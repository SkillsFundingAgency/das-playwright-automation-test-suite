using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions
{
    [Binding, Scope(Tag = "employer")]
    public class CampaignsEmployerSteps(ScenarioContext context)
    {
        private readonly CampaignsStepsHelper _stepsHelper = new(context);

        [Given(@"^the user navigates to (?:the )?Hire An Apprentice page$")]
        public async Task GivenTheUserNavigatesToTheHireAnApprenticePage() =>
            await _stepsHelper.GoToEmployerHubPage();

        [When(@"^the user clicks on the employer card ""(.*)""$")]
        public async Task WhenTheUserClicksOnTheEmployerCard(string cardName)
        {
            var page = await _stepsHelper.GoToEmployerHubPage();

            await page.NavigateToEmployerCard(cardName);
        }
    }
}