using SFA.DAS.FAA.UITests.Project.Tests.Pages;

namespace SFA.DAS.FAA.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FAAWhatOnlySearchStepDefinitions(ScenarioContext context)
    {
        private readonly ScenarioContext _context = context;

        [When(@"the user does a what only search '([^']*)'")]
        public async Task WhenTheUserDoesAWhatOnlySearch(string whatText)
        {
            var page = new FAASignedInLandingBasePage(_context);
            
            await page.SearchByWhat(whatText);
        }
    }
}
