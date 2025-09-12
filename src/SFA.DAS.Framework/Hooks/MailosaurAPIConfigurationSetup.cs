using SFA.DAS.Framework.Helpers;

namespace SFA.DAS.Framework.Hooks;

[Binding]
public class MailosaurAPIConfigurationSetup(ScenarioContext context)
{
    private const string MailosaurApiConfig = "MailasourApiConfig";

    private MailosaurApiHelper mailosaurApiHelper;

    private readonly TryCatchExceptionHelper _tryCatch = context.Get<TryCatchExceptionHelper>();

    [BeforeScenario(Order = 5)]
    public void SetUpMailosaurAPIConfiguration()
    {
        var mailosaurApiConfigs = new MultiConfigurationSetupHelper(context).SetMultiConfiguration<MailosaurApiConfig>(MailosaurApiConfig);

        var mailosaurApiConfig = Configurator.IsAdoExecution ? mailosaurApiConfigs.Single(x => x.ServerName == "azure") : mailosaurApiConfigs.Single(x => x.ServerName == "local");

        context.Set(new MailosaurUser(mailosaurApiConfig.ServerName, mailosaurApiConfig.ServerId, mailosaurApiConfig.ApiToken));
    }

    [BeforeScenario(Order = 12)]
    public void SetUpMailosaurApiHelper() => context.Set(mailosaurApiHelper = new MailosaurApiHelper(context));

    [AfterScenario(Order = 29)]
    public async Task DeleteMessages()
    {
        if (context.TestError == null) await _tryCatch.AfterScenarioException(async () => await mailosaurApiHelper.DeleteAdhocInbox());
    }
}
