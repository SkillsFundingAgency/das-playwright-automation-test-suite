using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Apprentice;
using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Steps.Apprentice;

public abstract class Apprentice_BaseSteps(ScenarioContext context) : AppEmp_BaseSteps(context)
{
    protected BeforeYouStartPage beforeYouStartPage;

    protected CheckYourAnswersPage checkYourAnswersPage;

    protected RegistrationCompletePage applicationSubmittedPage;

    protected ShutterPage shutterPage;

    protected Apprentice_NetworkHubPage networkHubPage;

    protected async Task<SignInPage> GetSignInPage() => new(context);

    protected async Task<Apprentice_NetworkHubPage> SubmitUserDetails_OnboardingJourneyComplete(AanBaseUser user)
    {
        var page = await GetSignInPage();

        return await page.SubmitUserDetails_OnboardingJourneyComplete(user);
    }
}
