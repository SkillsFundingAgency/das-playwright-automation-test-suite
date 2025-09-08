namespace SFA.DAS.FAA.UITests.Project.Tests.Steps;

[Binding]
public class FAABrowseByInterestsSteps(ScenarioContext context)
{
    private readonly ScenarioContext _context = context;

    [When(@"the user searches for vacancies by '(.*)' option in the Browse Your Interests route")]
    public async Task WhenTheUserNavigatesToBrowseYourInterestsPage(string locationOption)
    {
        var page = new FAASignedInLandingBasePage(_context);

        var page1 = await page.ClickBrowseByYourInterests();

        var page2 = await page1.SelectCategoriesCheckBoxes();

        await page2.EnterLocationDetails(locationOption);
    }

}
