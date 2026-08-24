using Reqnroll;
using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using SFA.DAS.Login.Service.Project;
using SFA.DAS.Login.Service.Project.Helpers;
using System.Threading.Tasks;

namespace SFA.DAS.DigiCerts.UITests.Project;

[Binding]
public class DigiCertConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario]
    public async Task SetUpDigiCertConfiguration()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpDigiCertConfiguration Order = 13 hook");

        var configSection = context.Get<ConfigSection>();

        await context.SetDigiCertUser(
        [
            configSection.GetConfigSection<DigiCertStandardUser>()
        ]);
        await context.SetDigiCertUser(
        [
           configSection.GetConfigSection<DigiCertFrameworkUser>()
        ]);
        await context.SetDigiCertUser(
        [
            configSection.GetConfigSection<DigiCertMultiStandardUser>()
        ]);
        await context.SetDigiCertUser(
        [
            configSection.GetConfigSection<DigiCertMultiFrameworkUser>()
        ]);
    }
}
