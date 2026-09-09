namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAARandomSearchStepDefinitions(ScenarioContext context)
{
    private readonly ScenarioContext _context = context;

    [When(@"^the user does a search without populating search fields$")]
    public async Task WhenTheUserDoesASearchWithoutPopulatingSearchFields()
    {
        var page = new FAASignedInLandingBasePage(_context);

        await page.VerifyPage();

        await page.SearchAtRandom();
    }

    [Then(@"^the user is presented with sort order as '([^']*)'$")]
    public async Task ThenTheUserIsPresentedWithSortOrderAsNew(string expectedSortOrder)
    {
        var page = new FAASearchResultPage(_context);

        await page.VerifyPage();

        await page.VerifySortOrder(expectedSortOrder);
    }
}
