using SFA.DAS.FAA.UITests.Project.Tests.Pages;

namespace SFA.DAS.FAA.UITests.Project.Tests.StepDefinitions
{
    [Binding]
    public class FAAWhatWhereSearchStepDefinitions(ScenarioContext context)
    {
        private readonly ScenarioContext _context = context;

        [When(@"the user does a what and where search '(.*)','(.*)'")]
        public async Task WhenTheUserDoesAWhatAndWhereSearch(string whatText, string whereText)
        {
            var page = new FAASignedInLandingBasePage(_context);
            
            await page.SearchByWhatWhere(whatText, whereText);
        }

        [Then(@"the user is presented with search results")]
        public async Task ThenTheUserIsPresentedWithSearchResults()
        {
            await new FAASearchResultPage(_context).VerifySuccessfulResults();
        }

        [Then(@"the user signs out")]
        public async Task ThenTheUserSignsOut()
        {
            await new FAASearchResultPage(_context).ClickSignout();
        }
    }
}