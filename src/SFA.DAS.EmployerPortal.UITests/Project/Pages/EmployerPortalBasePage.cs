using SFA.DAS.EmployerPortal.UITests.Project.Helpers;

namespace SFA.DAS.EmployerPortal.UITests.Project.Pages;

public abstract class EmployerPortalBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly EmployerPortalDataHelper employerPortalDataHelper = context.GetValue<EmployerPortalDataHelper>();
    #endregion

    public async Task<HomePage> GoToHomePage()
    {
        await page.GetByLabel("Service information").GetByRole(AriaRole.Link, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<HomePage> GoBackToHomePage()
    {
        await ClickBackLink();

        return await VerifyPageAsync(() => new HomePage(context));
    }


    public async Task<YouHaveBeenSignedOutPage> SignOut()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new YouHaveBeenSignedOutPage(context));
    }

    public async Task Continue() => await page.GetByRole(AriaRole.Link, new() { Name = "Continue" }).ClickAsync();

    protected async Task ClickBackLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
    }
}
