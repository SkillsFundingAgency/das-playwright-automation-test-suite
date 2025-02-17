using SFA.DAS.Framework;
using SFA.DAS.Registration.UITests.Project.Helpers;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public abstract class RegistrationBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly RegistrationDataHelper registrationDataHelper = context.GetValue<RegistrationDataHelper>();
    #endregion

    public async Task<HomePage> GoToHomePage()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Home" }).ClickAsync();

        return await VerifyPageAsync(() => new HomePage(context));
    }

    public async Task<HomePage> GoBackToHomePage()
    {
        await ClickBackLink();

        return await VerifyPageAsync(() => new HomePage(context));
    }


    public async Task<YouveLoggedOutPage> SignOut()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new YouveLoggedOutPage(context));
    }

    protected async Task ClickBackLink()
    {
        await page.GetByRole(AriaRole.Link, new() { Name = "Back", Exact = true }).ClickAsync();
    }
}
