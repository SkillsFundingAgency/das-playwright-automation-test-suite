namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Steps;

public abstract class CommitmentsCohortDetailsBaseSteps(ScenarioContext context)
{
    protected readonly ScenarioContext _context = context;
    private readonly StepsHelper _stepsHelper = new(context);
    private CohortSummaryPage cohortSummaryPage;
    private CohortDetailsPage cohortDetailsPage;
    protected SupportConsoleConfig config = context.GetSupportConsoleConfig<SupportConsoleConfig>();
    protected CohortDetails cohortDetails;

    protected async Task<CohortSummaryPage> SearchesForACohort() => cohortSummaryPage = await _stepsHelper.SearchForCohort(cohortDetails.CohortRef);

    protected async Task ViewThisCohort()
    {
        AssertCohortRef(await cohortSummaryPage.GetCohortRefNumber(), "Cohort reference mismatch in CohortSummaryPage");

        cohortDetailsPage = await cohortSummaryPage.ClickViewThisCohortButton();
    }

    protected async Task ViewCohortUln()
    {
        AssertCohortRef(await cohortDetailsPage.GetCohortRefNumber(), "Cohort reference mismatch in CohortDetailsPage");

        await cohortDetailsPage.ClickViewUlnLink(cohortDetails.Uln);
    }

    private void AssertCohortRef(string actual, string message) => StringAssert.Contains(cohortDetails.CohortRef, actual, message);
}
