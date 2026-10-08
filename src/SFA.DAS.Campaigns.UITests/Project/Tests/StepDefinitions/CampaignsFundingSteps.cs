using SFA.DAS.Campaigns.UITests.Project.Tests.Pages.Employer;

namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions;

[Binding]
public class CampaignsFundingSteps(ScenarioContext context)
{
    private readonly CampaignsStepsHelper _stepsHelper = new(context);
    private ExploreFundingOptionsPage _fundingPage;

    [Given(@"the employer is on the Understanding apprenticeship benefits and funding page")]
    public async Task GivenTheEmployerIsOnTheUnderstandingApprenticeshipBenefitsAndFundingPage()
    {
        var hubPage = await _stepsHelper.GoToEmployerHubPage();
        _fundingPage = await hubPage.NavigateToExploreFundingOptions();
    }

    [When(@"the employer calculates funding selecting ""(.*)""")]
    [When(@"the employer calculates funding selecting ""(.*)"" and standard ""(.*)""")]
    public async Task WhenTheEmployerCalculatesFundingSelecting(string payrollOption, string standard = null)
    {
        // Ensure page object instance is initialized if previous step came from EmployerHubPage
        _fundingPage ??= new ExploreFundingOptionsPage(context);

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
        _fundingPage ??= new ExploreFundingOptionsPage(context);
        await _fundingPage.VerifyLinks();
    }
}