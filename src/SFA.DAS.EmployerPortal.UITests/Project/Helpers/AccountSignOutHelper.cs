using SFA.DAS.EmployerPortal.UITests.Project.Pages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class AccountSignOutHelper(ScenarioContext context)
{
    public async Task<CreateAnAccountToManageApprenticeshipsPage> SignOut()
    {
        var page = await new HomePage(context, true).SignOut();

        await page.CickSignInInYouveLoggedOutPage();
        
        return await new SignInToYourApprenticeshipServiceAccountPage(context).GoManageApprenticeLandingPage();
    }

    public static async Task<YouveLoggedOutPage> SignOut(HomePage page) => await page.SignOut();

    public static async Task<YouveLoggedOutPage> SignOut(AccountUnavailablePage page) => await page.SignOut();
}
