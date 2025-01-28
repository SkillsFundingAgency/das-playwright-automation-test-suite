namespace SFA.DAS.SupportConsole.UITests.Project.Tests.Steps;

[Binding]
public class CommitmentsSteps(ScenarioContext context)
{
    private readonly StepsHelper _stepsHelper = new(context);

    private readonly SupportConsoleConfig _config = context.GetSupportConsoleConfig<SupportConsoleConfig>();

    [When(@"the User searches for an ULN")]
    public async Task WhenTheUserSearchesForAnULN() => await _stepsHelper.SearchForUln(_config.CohortDetails.Uln);

    [Then(@"the ULN details are displayed")]
    public async Task ThenTheULNDetailsAreDisplayed()
    {
        var page = await new UlnSearchResultsPage(context).SelectULN(_config.CohortDetails);

        await page.VerifyUlnDetailsPageHeaders();
    }

    [When(@"the User searches with a invalid ULN")]
    public async Task WhenTheUserSearchesWithAInvalidULN() => await _stepsHelper.SearchWithInvalidUln(false);

    [When(@"the User searches with a invalid ULN having special characters")]
    public async Task WhenTheUserSearchesWithAInvalidULNHavingSpecialCharacters() => await _stepsHelper.SearchWithInvalidUln(true);

    [Then(@"appropriate ULN error message is shown to the user")]
    public async Task ThenAppropriateUlnErrorMessageIsShownToTheUser() =>
        Assert.AreEqual(CommitmentsSearchPage.UlnSearchErrorMessage, await new CommitmentsSearchPage(context).GetCommitmentsSearchPageErrorText(), "Uln search Error message mismatch in CommitmentsSearchPage");

    [When(@"the User searches with a invalid Cohort Ref")]
    public async Task WhenTheUserSearchesWithAInvalidCohortRef() => await _stepsHelper.SearchWithInvalidCohort(false);

    [Then(@"appropriate Cohort error message is shown to the user")]
    public async Task ThenAppropriateCohortErrorMessageIsShownToTheUser() =>
        Assert.AreEqual(CommitmentsSearchPage.CohortSearchErrorMessage, await new CommitmentsSearchPage(context).GetCommitmentsSearchPageErrorText(), "Cohort search Error message mismatch in CommitmentsSearchPage");

    [When(@"the User searches with a invalid Cohort Ref having special characters")]
    public async Task WhenTheUserSearchesWithAInvalidCohortRefHavingSpecialCharacters() => await _stepsHelper.SearchWithInvalidCohort(true);

    [When(@"the user tries to view another Employer's Cohort Ref")]
    public async Task WhenTheUserTriesToViewAnotherEmployerSCohortRef() => await _stepsHelper.SearchWithUnauthorisedCohortAccess();

    [Then(@"unauthorised Cohort access error message is shown to the user")]
    public async Task ThenUnauthorisedCohortAccessErrorMessageIsShownToTheUser() =>
        Assert.AreEqual(CommitmentsSearchPage.UnauthorisedCohortSearchErrorMessage, await new CommitmentsSearchPage(context).GetCommitmentsSearchPageErrorText(), "Cohort search Error message mismatch in CommitmentsSearchPage");
}