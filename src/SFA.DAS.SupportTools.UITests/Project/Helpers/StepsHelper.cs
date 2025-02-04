
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Helpers;

public class StepsHelper(ScenarioContext context)
{
    public async Task<ToolSupportHomePage> ValidUserLogsinToSupportSCPTools(bool reLogin) => await LoginToSupportTools(context.GetUser<SupportToolScpUser>(), reLogin);

    public async Task<ToolSupportHomePage> ValidUserLogsinToSupportSCSTools() => await LoginToSupportTools(context.GetUser<SupportToolScsUser>(), false);

    private async Task<ToolSupportHomePage> LoginToSupportTools(DfeAdminUser loginUser, bool reLogin)
    {
        await LoginToDfeSignIn(loginUser, reLogin);

        return await BasePage.VerifyPageAsync(() => new ToolSupportHomePage(context));
    }

    public async Task LoginToDfeSignIn(DfeAdminUser loginUser, bool reLogin)
    {
        var helper = new DfeAdminLoginStepsHelper(context);

        if (reLogin)
        {
            var driver = context.Get<Driver>();

            var url = UrlConfig.SupportTools_BaseUrl;

            context.Get<ObjectContext>().SetDebugInformation(url);

            await driver.Page.GotoAsync(url);

            await helper.CheckAndLoginToSupportTool(loginUser);
        }
        else
        {
            await helper.LoginToSupportTool(loginUser);
        }
    }
}
