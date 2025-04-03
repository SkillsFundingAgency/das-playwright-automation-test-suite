using SFA.DAS.SupportTools.UITests.Project.Helpers;
using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

public abstract class CommitmentsCohortDetailsBaseSteps(ScenarioContext context)
{
    protected readonly ScenarioContext _context = context;
    private readonly StepsHelper _stepsHelper = new(context);
    private CohortDetailsPage cohortDetailsPage;
    protected SupportConsoleConfig config = context.GetSupportConsoleConfig<SupportConsoleConfig>();
    protected CohortDetails cohortDetails;

    protected async Task<CohortDetailsPage> SearchesForACohort() => cohortDetailsPage = await _stepsHelper.SearchForCohort(cohortDetails.CohortRef);

    protected async Task ViewCohortUln()
    {
        AssertCohortRef(await cohortDetailsPage.GetCohortRefNumber(), "Cohort reference mismatch in CohortDetailsPage");

        await cohortDetailsPage.ClickViewUlnLink(cohortDetails.Uln);
    }

    private void AssertCohortRef(string actual, string message) => StringAssert.Contains(cohortDetails.CohortRef, actual, message);
}
