using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign;
using SFA.DAS.DfeAdmin.Service.Project.Helpers.DfeSign.User;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.SupportTools.UITests.Project.Tests.Pages;

namespace SFA.DAS.SupportTools.UITests.Project.Helpers;

public class StepsHelper(ScenarioContext context)
{
    public async Task NavigateToSupportTools()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.SupportTools_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    public async Task<ToolSupportHomePage> ReLoginToSupportSCPTools()
    {
        await NavigateToSupportTools();

        if (!await new ToolSupportHomePage(context).IsPageDisplayed())
        {
            await new DfeAdminLoginStepsHelper(context).CheckAndLoginToSupportTool(context.GetUser<SupportToolScpUser>());
        }

        return await BasePage.VerifyPageAsync(() => new ToolSupportHomePage(context));
    }

    public async Task<ToolSupportHomePage> ValidUserLogsinToSupportSCPTools() => await LoginToSupportTools(context.GetUser<SupportToolScpUser>());

    public async Task<ToolSupportHomePage> ValidUserLogsinToSupportSCSTools() => await LoginToSupportTools(context.GetUser<SupportToolScsUser>());

    private async Task<ToolSupportHomePage> LoginToSupportTools(DfeAdminUser loginUser)
    {
        await new DfeAdminLoginStepsHelper(context).LoginToSupportTool(loginUser);

        return await BasePage.VerifyPageAsync(() => new ToolSupportHomePage(context));
    }
}
