namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Steps;

[Binding]
public class CommitmentsTrainingProviderHistorySteps : CommitmentsCohortDetailsBaseSteps
{
    public CommitmentsTrainingProviderHistorySteps(ScenarioContext context) : base(context) => cohortDetails = config.CohortWithTrainingProviderHistory;

    [When(@"the User searches for a Cohort with Training provider history")]
    public async Task WhenTheUserSearchesForACohortWithTrainingProviderHistory() => await SearchesForACohort();

    [When(@"the User clicks on 'View this cohort' button with Training provider history")]
    public async Task WhenTheUserClicksOnButtonWithTrainingProviderHistory() => await ViewThisCohort();

    [When(@"the ULN details page is displayed with Training provider history")]
    public async Task ThenTheULNDetailsPageIsDisplayedWithTrainingProviderHistory()
    {
        var page = await GetUlnDetailsPage();

        await page.ClickTrainingProviderHistoryTab();
    }

    [When(@"the user chooses to view Uln of the Cohort with Training provider history")]
    public async Task WhenTheUserChoosesToViewUlnOfTheCohortWithTrainingProviderHistory() => await ViewCohortUln();

    [Then(@"the Training provider history is displayed")]
    public async Task TheTrainingProviderHistoryIsDisplayed()
    {
        var page = await GetUlnDetailsPage();

        await page.TrainingProviderHistoryIsDisplayed();
    }

    private async Task<UlnDetailsPageWithTrainingProviderHistory> GetUlnDetailsPage() => await VerifyPageHelper.VerifyPageAsync(() => new UlnDetailsPageWithTrainingProviderHistory(_context, cohortDetails)); 
}