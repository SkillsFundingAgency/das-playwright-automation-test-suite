using SFA.DAS.SupportTools.UITests.Project.Helpers;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

[Binding]
public class CommonSteps(ScenarioContext context)
{
    private readonly StepsHelper _stepsHelper = new(context);

    [Given(@"the Tier 1 User is logged into Support Console")]
    public async Task GivenTheTierUserIsLoggedIntoSupportConsole() => await _stepsHelper.Tier1LoginToSupportConsole();

    [Given(@"the User is logged into Support Console")]
    [Given(@"the Tier 2 User is logged into Support Console")]
    public async Task GivenTheUserIsLoggedIntoSupportConsole() => await _stepsHelper.Tier2LoginToSupportConsole();

    [Given(@"the User is on the Account details page")]
    public async Task GivenTheUserIsOnTheAccountDetailsPage() => await _stepsHelper.SearchAndViewAccount();
}