

namespace SFA.DAS.Transfers.UITests.Project;

[Binding]
public class TransfersConfigurationSetup(ScenarioContext context)
{
    private readonly ConfigSection _configSection = context.Get<ConfigSection>();

    [BeforeScenario(Order = 12)]
    public async Task SetUpTransfersConfiguration()
    {
        await context.SetEasLoginUser(
        [
            _configSection.GetConfigSection<AgreementNotSignedTransfersUser>(),
            _configSection.GetConfigSection<TransfersUser>()
        ]);
    }
}
