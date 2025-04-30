using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

[Binding]
public class MaSteps(ScenarioContext context)
{
    private ChallengePage _challengePage;

    private FinancePage _financePage;

    private TeamMembersPage _teamMembersPage;

    [Then(@"the user can search by Hashed account id, account name or PAYE scheme")]
    public async Task ThenTheUserCanSearchByHashedAccountIdAccountNameOrPAYEScheme()
    {
        var searchHomePage = new SearchHomePage(context);

        await searchHomePage.GoToEmployerAccountSearchHomePage();
        await searchHomePage.SearchByPublicAccountIdAndViewAccount();
        await searchHomePage.GoBackToSearchHomePage();
        await searchHomePage.SearchByPayeSchemeAndViewAccount();
        await searchHomePage.GoBackToSearchHomePage();
    }

    [Then(@"the user can search by name or email address")]
    public async Task ThenTheUserCanSearchByNameOrEmailAddress()
    {
        var searchHomePage = new SearchHomePage(context);

        await searchHomePage.GoToEmployerUserSearchHomePage();
        await searchHomePage.SearchByEmailAddressAndView();
        await searchHomePage.GoBackToSearchHomePage();
    }

    [When(@"the user navigates to finance page")]
    public async Task WhenTheUserNavigatesToFinancePage() => await new AccountOverviewPage(context).ClickFinanceMenuLink();

    [Then(@"the user is redirected to a challenge page")]
    public async Task ThenTheUserIsRedirectedToAChallengePage() => _challengePage = await VerifyPageHelper.VerifyPageAsync(() => new ChallengePage(context));

    [When(@"the user enters invalid payscheme")]
    public async Task WhenTheUserEntersInvalidPayscheme() => await _challengePage.EnterIncorrectPaye();

    [When(@"enters correct levybalance")]
    public async Task WhenEntersCorrectLevybalance() => await _challengePage.EnterCorrectLevybalance();

    [When(@"the user submits the challenge")]
    public async Task WhenTheUserSubmitsTheChallenge() => await _challengePage.Submit();

    [Then(@"the user should see the error message (.*)")]
    public async Task ThenTheUserShouldSeeTheErrorMessage(string message) => await _challengePage.VerifyChallengeResponseErrorMessage(message);

    [When(@"the user enters valid payscheme and levybalance")]
    public async Task WhenTheUserEntersValidPayschemeAndLevybalance()
    {
        await _challengePage.EnterCorrectPaye();

        await _challengePage.EnterCorrectLevybalance();
    }

    [Then(@"the user is redirected to finance page")]
    public async Task ThenTheUserIsRedirectedToFinancePage() => _financePage = await VerifyPageHelper.VerifyPageAsync(() => new FinancePage(context));

    [Then(@"the user can view levy declarations")]
    public async Task ThenTheUserCanViewLevyDeclarations() => await _financePage.ViewLevyDeclarations();

    [Then(@"the user can view transactions")]
    public async Task ThenTheUserCanViewTransactions() => await _financePage.ViewTransactions();

    [When(@"the user navigates to team members page")]
    public async Task WhenTheUserNavigatesToTeamMembersPage() => _teamMembersPage = await new AccountOverviewPage(context).ClickTeamMembersLink();

    [Then(@"the user can view employer user information")]
    public async Task ThenTheUserCanViewEmployerUserInformation() => await _teamMembersPage.GoToUserInformationOverviewPage();
}