using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

[Binding]
public class CommitmentsPendingChangingSteps : CommitmentsCohortDetailsBaseSteps
{
    public CommitmentsPendingChangingSteps(ScenarioContext context) : base(context) => cohortDetails = config.CohortWithPendingChanges;

    [When(@"the User searches for a Cohort with pending changes")]
    public async Task WhenTheUserSearchesForACohortWithPendingChanges() => await SearchesForACohort();

    [When(@"the User clicks on 'View this cohort' button with pending changes")]
    public async Task WhenTheUserClicksOnButtonWithPendingChanges() => await ViewThisCohort();

    [When(@"the ULN details page is displayed with pending changes")]
    public async Task ThenTheULNDetailsPageIsDisplayedWithPendingChanges()
    {
        var page = await GetUlnDetailsPage();

        await page.ClickPendingChangesTab();
    }

    [When(@"the user chooses to view Uln of the Cohort with pending changes")]
    public async Task WhenTheUserChoosesToViewUlnOfTheCohortWithPendingChanges() => await ViewCohortUln();

    [Then(@"the pending changes are displayed")]
    public async Task ThePendingUpdateChangesAreDisplayed()
    {
        var page = await GetUlnDetailsPage();
        
        await page.PendingChangesAreDisplayed();
    }

    private async Task<UlnDetailsPageWithPendingChanges> GetUlnDetailsPage() => await VerifyPageHelper.VerifyPageAsync(() => new UlnDetailsPageWithPendingChanges(_context, cohortDetails));
}

