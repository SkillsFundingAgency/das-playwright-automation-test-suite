using SFA.DAS.DfeAdmin.Service.Project.Tests.LandingPage;

namespace SFA.DAS.EPAO.UITests.Project.Tests.Pages.Admin;

public abstract class EPAOAdmin_BasePage(ScenarioContext context) : EPAO_BasePage(context)
{
    // Clicking Sign out is redirecting to Admin landing page instead of SignedOutPage
    public async Task<ASAdminLandingPage> SignOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new ASAdminLandingPage(context));
    }

}
