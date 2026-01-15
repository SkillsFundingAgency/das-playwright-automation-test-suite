namespace SFA.DAS.API.Framework;

[Binding]
public class ApiFrameworkConfigurationSetup(ScenarioContext context)
{
    private readonly ConfigSection _configSection = context.Get<ConfigSection>();

    [BeforeScenario(Order = 2)]
    public void SetUpApiFrameworkConfiguration()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpApiFrameworkConfiguration Order = 2 hook");

        var inner_ApiFrameworkConfig = new Inner_ApiFrameworkConfig(_configSection.GetConfigSection<Inner_ApiAuthTokenConfig>())
        {
            IsVstsExecution = Configurator.IsAdoExecution,
        };

        context.Set(inner_ApiFrameworkConfig);

        context.Set(_configSection.GetConfigSection<Outer_ApiAuthTokenConfig>());

        context.Set(_configSection.GetConfigSection<ApprenticeCommitmentsJobsAuthTokenConfig>());
    }

    [BeforeScenario(Order = 4)]
    public void SetUpHelpers() => context.Replace(new RetryHelper(context.ScenarioInfo, context.Get<ObjectContext>()));
}