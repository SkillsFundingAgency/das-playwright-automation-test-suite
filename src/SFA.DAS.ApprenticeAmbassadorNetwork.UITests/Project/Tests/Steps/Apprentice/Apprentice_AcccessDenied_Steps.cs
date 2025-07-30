using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Apprentice;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Apprentice;

[Binding, Scope(Tag = "@aanaprentice")]
public class Apprentice_AcccessDenied_Steps(ScenarioContext context) : Apprentice_BaseSteps(context)
{
    [Given(@"the non Private beta apprentice logs into AAN portal")]
    public async Task GivenTheNonPrivateBetaApprenticeLogsIntoAANPortal()
    {
        await new SignInPage(context).NonPrivateBetaUserDetails(context.Get<AanApprenticeNonBetaUser>());
    }

    [Then(@"an Access Denied page should be displayed")]
    public async Task ThenAccessDeniedPageShouldBeDisplayed()
    {
        var page = await new BeforeYouStartPage(context).StartApprenticeOnboardingJourney();

        var page1 = await page.AcceptTermsAndConditions();

        var page2 = await page1.YesHaveApprovalFromMaanagerAndNonPrivateBetaUser();

        await page2.VerifyHomeLink();
    }
}
