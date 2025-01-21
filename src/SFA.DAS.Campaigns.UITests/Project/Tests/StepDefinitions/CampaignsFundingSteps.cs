namespace SFA.DAS.Campaigns.UITests.Project.Tests.StepDefinitions;

[Binding]
public class CampaignsFundingSteps(ScenarioContext context)
{
    private readonly CampaignsStepsHelper _stepsHelper = new(context);

    [Given(@"the user navigates to the Understanding Apprentice benefit and funding page make selection under three million")]
    public async Task GivenTheUserNavigatesToTheUnderstandingApprenticeBenefitAndFundingPage()
    {
        var page = await _stepsHelper.GoToEmployerHubPage();

        var page1 = await page.NavigateToUnderstandingApprenticeshipBenefitsAndFunding();

        await page1.SelectUnder3Million();
    }

    [Given(@"the user navigates to the Understanding Apprentice benefit and funding page make selection over three million")]
    public async Task GivenTheUserNavigatesToTheUnderstandingApprenticeBenefitAndFundingPageMakeSelectionOverThreeMillion()
    {
        var page = await _stepsHelper.GoToEmployerHubPage();

        var page1 = await page.NavigateToUnderstandingApprenticeshipBenefitsAndFunding();

        await page1.SelectOver3Million();
    }
}
