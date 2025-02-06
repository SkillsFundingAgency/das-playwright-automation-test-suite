using SFA.DAS.Framework;
using SFA.DAS.Registration.UITests.Project.Helpers;

namespace SFA.DAS.Registration.UITests.Project.Pages;

public abstract class RegistrationBasePage(ScenarioContext context) : BasePage(context)
{
    #region Helpers and Context
    protected readonly RegistrationDataHelper registrationDataHelper = context.GetValue<RegistrationDataHelper>();
    #endregion

    public HomePage GoToHomePage() => new(context, true);

    //public HomePage ClickBackLink()
    //{
    //    NavigateBack();
    //    return new HomePage(context);
    //}


    public async Task<YouveLoggedOutPage> SignOut()
    {
        await page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign out" }).ClickAsync();

        return await VerifyPageAsync(() => new YouveLoggedOutPage(context));
    }
}
