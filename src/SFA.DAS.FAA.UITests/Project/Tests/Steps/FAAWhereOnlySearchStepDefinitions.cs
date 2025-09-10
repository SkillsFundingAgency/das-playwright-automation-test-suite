namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAAWhereOnlySearchStepDefinitions(ScenarioContext context)
{
    private readonly ScenarioContext _context = context;

    [When(@"the user does a where only search '([^']*)'")]
    public async Task WhenTheUserDoesAWhereOnlySearch(string whereText)
    {
        await new FAASignedInLandingBasePage(_context).SearchByWhere(whereText);
    }
}
