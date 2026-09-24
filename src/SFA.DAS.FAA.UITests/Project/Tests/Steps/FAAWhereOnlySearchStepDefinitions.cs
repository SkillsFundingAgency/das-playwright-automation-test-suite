namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAAWhereOnlySearchStepDefinitions(ScenarioContext context)
{
    private readonly ScenarioContext _context = context;

    [When(@"^the user does a where only search '([^']*)'$")]
    public async Task WhenTheUserDoesAWhereOnlySearch(string whereText)
    {
        await new FAASignedInLandingBasePage(_context).SearchByWhere(whereText);
    }

    [When(@"^the user does a where only search on search results page for '([^']*)'$")]
    public async Task WhenTheUserDoesAWhereOnlySearchOnSearchResultsPageFor(string whereText)
    {
        await new FAASearchResultPage(_context).SearchByWhereOnSearchResultsPage(whereText);
    }
}
