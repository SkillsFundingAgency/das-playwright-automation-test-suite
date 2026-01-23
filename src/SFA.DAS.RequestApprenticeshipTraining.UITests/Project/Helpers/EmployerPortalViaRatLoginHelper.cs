

namespace SFA.DAS.RequestApprenticeshipTraining.UITests.Project.Helpers;

public class EmployerPortalViaRatLoginHelper(ScenarioContext context) : EmployerPortalLoginHelper(context)
{
    public async Task<AskIfTrainingProvidersCanRunThisCoursePage> LoginViaRat(RatEmployerBaseUser loginUser)
    {
        SetCredentials(loginUser, true);

        var page = await new StubSignInEmployerPage(context).Login(loginUser);

        await page.Continue();

        return await VerifyPageHelper.VerifyPageAsync(context, () => new AskIfTrainingProvidersCanRunThisCoursePage(context));
    }
}
