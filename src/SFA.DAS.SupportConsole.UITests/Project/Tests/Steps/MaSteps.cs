﻿namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Steps;

[Binding]
public class MaSteps(ScenarioContext context)
{
    private ChallengePage _challengePage;

    private FinancePage _financePage;

    private TeamMembersPage _teamMembersPage;

    [Then(@"the user can search by Hashed account id, account name or PAYE scheme")]
    public async Task ThenTheUserCanSearchByHashedAccountIdAccountNameOrPAYEScheme()
    {
        var page = await new SearchHomePage(context).GoToSearchHomePage();

        var page1 = await page.SearchByHashedAccountIdAndViewAccount();

        var page2 = await page1.GoToSearchHomePage();

        var page3 = await page2.SearchByAccountNameAndViewAccount();

        var page4 = await page3.GoToSearchHomePage();

        var page5 = await page4.SearchByPayeSchemeAndViewAccount();

        await page5.GoToSearchHomePage();
    }

    [Then(@"the user can search by name or email address")]
    public async Task ThenTheUserCanSearchByNameOrEmailAddress()
    {
        var page = await new SearchHomePage(context).GoToSearchHomePage();

        var page1 = await page.SearchByNameAndView();

        var page2 = await page1.GoToSearchHomePage();

        var page3 = await page2.SearchByEmailAddressAndView();

        await page3.GoToSearchHomePage();
    }

    [When(@"the user navigates to finance page")]
    public async Task WhenTheUserNavigatesToFinancePage() => await new AccountOverviewPage(context).ClickFinanceMenuLink();

    [Then(@"the user is redirected to a challenge page")]
    public async Task ThenTheUserIsRedirectedToAChallengePage() => _challengePage = await BasePage.VerifyPageAsync(() => new ChallengePage(context));

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
    public async Task ThenTheUserIsRedirectedToFinancePage() => _financePage = await BasePage.VerifyPageAsync(() => new FinancePage(context));

    [Then(@"the user can view levy declarations")]
    public async Task ThenTheUserCanViewLevyDeclarations() => await _financePage.ViewLevyDeclarations();

    [Then(@"the user can view transactions")]
    public async Task ThenTheUserCanViewTransactions() => await _financePage.ViewTransactions();

    [When(@"the user navigates to team members page")]
    public async Task WhenTheUserNavigatesToTeamMembersPage() => _teamMembersPage = await new AccountOverviewPage(context).ClickTeamMembersLink();

    [Then(@"the user can view employer user information")]
    public async Task ThenTheUserCanViewEmployerUserInformation() => await _teamMembersPage.GoToUserInformationOverviewPage();
}