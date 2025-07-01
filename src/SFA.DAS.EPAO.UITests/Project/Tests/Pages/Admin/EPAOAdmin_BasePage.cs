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

    //protected IWebElement GetData(string headerName)
    //{
    //    foreach (var row in pageInteractionHelper.FindElements(TRows))
    //    {
    //        if (row.FindElement(THeader).Text == headerName)
    //        {
    //            return row.FindElement(TData);
    //        }
    //    }
    //    throw new NotFoundException($"{headerName} not found");
    //}

    //protected StaffDashboardPage ReturnToDashboard()
    //{
    //    formCompletionHelper.ClickElement(() => pageInteractionHelper.FindElement(ReturnToDashboardlink));
    //    return new(context);
    //}

}
