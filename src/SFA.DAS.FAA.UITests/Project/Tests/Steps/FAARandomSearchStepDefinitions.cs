using SFA.DAS.FAA.UITests.Project.Tests.Pages;

namespace SFA.DAS.FAA.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FAARandomSearchStepDefinitions(ScenarioContext context)
    {
        private readonly ScenarioContext _context = context;

        [When(@"the user does a search without populating search fields")]
        public async Task WhenTheUserDoesASearchWithoutPopulatingSearchFields()
        {
            var page = new FAASignedInLandingBasePage(_context);

            await page.VerifyPage();

            await page.SearchAtRandom();
        }
    }
}
