namespace SFA.DAS.ProvideFeedback.UITests.Project;

[Binding]
public class ProvideFeedbackConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 12)]
    public async Task SetUpProvideFeedbackConfigConfiguration()
    {
        var configSection = context.Get<ConfigSection>();

        await context.SetEasLoginUser(
        [
            configSection.GetConfigSection<EmployerFeedbackUser>()
        ]);

        await context.SetApprenticeAccountsPortalUser(
        [
           configSection.GetConfigSection<ApprenticeFeedbackUser>(),
        ]);
    }
}
