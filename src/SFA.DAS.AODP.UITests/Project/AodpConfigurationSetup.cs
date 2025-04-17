using System.Threading.Tasks;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;

namespace SFA.DAS.AODP.UITests.Project;

[Binding]
public class AodpConfigurationSetup(ScenarioContext context)
{
    private const string DfeAdminsConfig = "DfeAdminsConfig";

    private readonly ConfigSection _configSection = context.Get<ConfigSection>();

    [BeforeScenario(Order = 2)]
    public async Task SetUsers()
    {

        await context.SetAodpLoginUser(
            [
         _configSection.GetConfigSection<AodpPortalDfeUserUser>(),
         _configSection.GetConfigSection<AodpPortalAoUserUser>()
        ]);

        // await context.SetEasLoginUser([_configSection.GetConfigSection<AddMultiplePayeAodpUser>()]);

    }
}
