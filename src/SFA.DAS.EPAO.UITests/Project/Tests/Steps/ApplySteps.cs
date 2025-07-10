using SFA.DAS.EPAO.UITests.Project.Tests.Pages;
using SFA.DAS.EPAO.UITests.Project.Tests.Pages.AssessmentService;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Steps;

[Binding]
public class ApplySteps(ScenarioContext context) : EPAOBaseSteps(context)
{
    private readonly ScenarioContext _context = context;

    [Given(@"the (Apply User) is logged into Assessment Service Application")]
    [When(@"the (Apply User) is logged into Assessment Service Application")]
    public async Task GivenTheUserIsLoggedIntoAssessmentServiceApplication(string _) => searchForYourOrganisationPage = await ePAOHomePageHelper.LoginInAsApplyUser(ePAOApplyUser);

    [Then(@"the User Name is displayed in the Logged In Home page")]
    public async Task ThenTheUserNameIsDisplayedInTheLoggedInHomePage() => await new AS_LoggedInHomePage(_context).VerifySignedInUserName(ePAOApplyUser.FullName);

    [Then(@"the Apply User is able to Signout from the application")]
    public async Task ThenTheApplyUserIsAbleToSignoutFromTheApplication()
    {
        var page = await new AS_LoggedInHomePage(_context).ClickSignOutLink();
        
        await page.ClickSignBackInLink();
    }

    [Then(@"no matches are shown for Organisation searches with Invalid search term")]
    public async Task ThenNoMatchesAreShownForOrganisationSearchesWithInvalidSearchTerm()
    {
        var page = await new AP_PR1_SearchForYourOrganisationPage(_context).EnterInvalidOrgNameAndSearchInSearchForYourOrgPage(EPAOApplyDataHelper.InvalidOrgNameWithAlphabets);

        await page.VerifyInvalidSearchResultText();

        await page.EnterInvalidOrgNameAndSearchInSearchResultsForPage(EPAOApplyDataHelper.InvalidOrgNameWithNumbers);

        await page.EnterInvalidOrgNameAndSearchInSearchResultsForPage(EPAOApplyDataHelper.InvalidOrgNameWithAWord);
    }
}
