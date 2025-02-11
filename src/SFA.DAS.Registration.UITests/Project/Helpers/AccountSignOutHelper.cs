using SFA.DAS.Registration.UITests.Project.Pages;

namespace SFA.DAS.Registration.UITests.Project.Helpers;

public class AccountSignOutHelper(ScenarioContext context)
{
    public async Task<CreateAnAccountToManageApprenticeshipsPage> SignOut()
    {
        var page = await new HomePage(context, true).SignOut();

        var page1 = await page.CickSignInInYouveLoggedOutPage();
        
        return await page1.GoManageApprenticeLandingPage();
    }

    public static async Task<YouveLoggedOutPage> SignOut(HomePage page) => await page.SignOut();

    public static async Task<YouveLoggedOutPage> SignOut(AccountUnavailablePage page) => await page.SignOut();
}
