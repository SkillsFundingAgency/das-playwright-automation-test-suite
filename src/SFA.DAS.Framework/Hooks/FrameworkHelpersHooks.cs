namespace SFA.DAS.Framework.Hooks;

[Binding]
public class FrameworkHelpersHooks(ScenarioContext context)
{
    [BeforeScenario(Order = 2)]
    public void SetUpFrameworkHelpers()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpFrameworkHelpers Order = 2 hook");

        context.Set(new TryCatchExceptionHelper(objectContext));

        context.Set(new RetryHelper(context.ScenarioInfo, objectContext));
    }
}