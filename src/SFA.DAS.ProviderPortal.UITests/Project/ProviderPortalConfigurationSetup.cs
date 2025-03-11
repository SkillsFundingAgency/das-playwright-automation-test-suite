
namespace SFA.DAS.ProviderPortal.UITests.Project;

[Binding]
public class ProviderPortalConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 2)]
    public async Task SetUpProviderPortalConfiguration()
    {
        var configSection = context.Get<ConfigSection>();

        await context.SetEasLoginUser(
        [
            configSection.GetConfigSection<EPRLevyUser>(),
            configSection.GetConfigSection<EPRNonLevyUser>(),
            configSection.GetConfigSection<EPRAcceptRequestUser>(),
            configSection.GetConfigSection<EPRDeclineRequestUser>(),
            configSection.GetConfigSection<EPRMultiAccountUser>(),
            configSection.GetConfigSection<EPRMultiOrgUser>()
        ]);
    }
}
