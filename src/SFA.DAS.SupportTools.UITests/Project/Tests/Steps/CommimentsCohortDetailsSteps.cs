using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

[Binding]
public class CommimentsCohortDetailsSteps : CommitmentsCohortDetailsBaseSteps
{
    public CommimentsCohortDetailsSteps(ScenarioContext context) : base(context) => cohortDetails = config.CohortDetails;

    [When(@"the User searches for a Cohort")]
    public async Task WhenTheUserSearchesForACohort() => await SearchesForACohort();

    [When(@"the User clicks on 'View this cohort' button")]
    public async Task WhenTheUserClicksOnButton() => await ViewThisCohort();

    [When(@"the user chooses to view Uln of the Cohort")]
    public async Task WhenTheUserChoosesToViewUlnOfTheCohort() => await ViewCohortUln();

    [Then(@"the ULN details page is displayed")]
    public async Task ThenTheULNDetailsPageIsDisplayed() => await new UlnDetailsPage(_context, cohortDetails).VerifyUlnDetailsPageHeaders();
}