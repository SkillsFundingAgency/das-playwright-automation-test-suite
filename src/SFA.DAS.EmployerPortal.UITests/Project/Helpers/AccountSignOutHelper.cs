using SFA.DAS.EmployerPortal.UITests.Project.Pages;
using SFA.DAS.EmployerPortal.UITests.Project.Pages.StubPages;

namespace SFA.DAS.EmployerPortal.UITests.Project.Helpers;

public class AccountSignOutHelper(ScenarioContext context)
{
    public async Task<YouveLoggedOutPage> SignOut() => await new HomePage(context, true).SignOut();     

    public static async Task<YouveLoggedOutPage> SignOut(HomePage page) => await page.SignOut();

    public static async Task<YouveLoggedOutPage> SignOut(AccountUnavailablePage page) => await page.SignOut();
}
