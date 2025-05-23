﻿using SFA.DAS.SupportTools.UITests.Project.Helpers;
using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Tests.Steps;

[Binding]
public class CommonSteps(ScenarioContext context)
{
    private readonly StepsHelper _stepsHelper = new(context);

    [Given(@"the Tier 1 User is logged into Support Tool")]
    public async Task GivenTheTierUserIsLoggedIntoSupportConsole() => await _stepsHelper.Tier1LoginToSupportTool();

    [Given(@"the User is logged into Support Tool")]
    [Given(@"the Tier 2 User is logged into Support Tool")]
    public async Task GivenTheUserIsLoggedIntoSupportConsole() => await _stepsHelper.Tier2LoginToSupportTool();

    [Given(@"the User is on the Account details page")]
    public async Task GivenTheUserIsOnTheAccountDetailsPage() => await _stepsHelper.SearchAndViewAccount();

    [Given(@"the user navigates to employer support page")]
    public async Task WhenTheUserNavigatesToEmployerSupportPage() => await _stepsHelper.NavigateToSupportSearchPage();
}