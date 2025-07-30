using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Apprentice;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Apprentice;

public abstract class Apprentice_BaseSteps(ScenarioContext context) : AppEmp_BaseSteps(context)
{
    protected BeforeYouStartPage beforeYouStartPage;

    protected CheckYourAnswersPage checkYourAnswersPage;

    protected RegistrationCompletePage applicationSubmittedPage;

    protected ShutterPage shutterPage;

    protected Apprentice_NetworkHubPage networkHubPage;

    protected async Task<SignInPage> GetSignInPage()
    {
        var page = new SignInPage(context);

        await page.VerifyPage();

        return page;
    }

    protected async Task<Apprentice_NetworkHubPage> SubmitUserDetails_OnboardingJourneyComplete(AanBaseUser user)
    {
        var page = await GetSignInPage();

        return await page.SubmitUserDetails_OnboardingJourneyComplete(user);
    }
}
