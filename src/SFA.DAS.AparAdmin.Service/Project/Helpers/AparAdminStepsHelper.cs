
using SFA.DAS.AparAdmin.UITests.Project.Tests.Pages;

namespace SFA.DAS.AparAdmin.Service.Project.Helpers;

public class AparAdminStepsHelper(ScenarioContext context)
{
    protected readonly ScenarioContext context = context;

    public async Task<AparAdminHomePage> GoToRoatpAdminHomePage()
    {
        await new DfeAdminLoginStepsHelper(context).NavigateAndLoginToASAdmin();

        return await VerifyPageHelper.VerifyPageAsync(() => new AparAdminHomePage(context));
    }
}
