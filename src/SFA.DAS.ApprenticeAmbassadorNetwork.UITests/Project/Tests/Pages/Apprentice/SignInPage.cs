
namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.Apprentice;

public class SignInPage(ScenarioContext context) : ApprenticeSignInPage(context)
{
    public async Task<BeforeYouStartPage> SubmitValidUserDetails(AanBaseUser user)
    {
        await SubmitUserDetails(user, true);

        return await VerifyPageAsync(() => new BeforeYouStartPage(context));
    }

    public async Task<BeforeYouStartPage> NonPrivateBetaUserDetails(AanBaseUser user)
    {
        await SubmitUserDetails(user, true);

        return await VerifyPageAsync(() => new BeforeYouStartPage(context));
    }

    public async Task<Apprentice_NetworkHubPage> SubmitUserDetails_OnboardingJourneyComplete(AanBaseUser user)
    {
        await SubmitUserDetails(user, false);

        return await VerifyPageAsync(() => new Apprentice_NetworkHubPage(context));
    }

    private async Task SubmitUserDetails(AanBaseUser user, bool firstlogin)
    {
        if (firstlogin)
        {
            if (tags.Any(x => x == "aanapprenticeonboardingreset")) { await context.Get<AANSqlHelper>().ResetApprenticeOnboardingJourney(user.Username); }

            objectContext.SetLoginCredentials(user);
        }

        await SubmitUserDetails(user);
    }
}
