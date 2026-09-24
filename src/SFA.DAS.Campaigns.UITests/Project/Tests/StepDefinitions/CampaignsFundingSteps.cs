using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions;

[Binding]
public class CampaignsFundingSteps(ScenarioContext context)
{
    private readonly CampaignsStepsHelper _stepsHelper = new(context);
    private UnderstandingApprenticeshipBenefitsFundingPage _fundingPage;

    [Given(@"the employer is on the Understanding apprenticeship benefits and funding page")]
    public async Task GivenTheEmployerIsOnTheUnderstandingApprenticeshipBenefitsAndFundingPage()
    {
        var hubPage = await _stepsHelper.GoToEmployerHubPage();
        _fundingPage = await hubPage.NavigateToUnderstandingApprenticeshipBenefitsAndFunding();
    }

    [When(@"the employer calculates funding selecting ""(.*)""")]
    public async Task WhenTheEmployerCalculatesFundingSelecting(string payrollOption)
    {
        if (payrollOption.Equals("Over £3 million", StringComparison.OrdinalIgnoreCase))
        {
            await _fundingPage.SelectOver3Million();
        }
        else
        {
            await _fundingPage.SelectUnder3Million();
        }
    }

    [Then(@"the estimated funding result should be calculated successfully")]
    public async Task ThenTheEstimatedFundingResultShouldBeCalculatedSuccessfully()
    {
        await _fundingPage.VerifyLinks();
    }
}