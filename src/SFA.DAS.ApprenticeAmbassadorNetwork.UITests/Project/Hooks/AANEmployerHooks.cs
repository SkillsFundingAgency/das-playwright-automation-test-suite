namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Hooks;

[Binding, Scope(Tag = "@aanemployer")]
public class AANEmployerHooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 31)]
    public async Task Navigate_Employer() => await Navigate(UrlConfig.AAN_Employer_BaseUrl);

    [AfterScenario(Order = 34), Scope(Tag = "@aanemployer03b")]
    public async Task DeleteLocationFilterEvents()
    {
        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(async () =>
        {
            await context.Get<AANSqlHelper>().DeleteLocationFilterEventsBeginning("Location Filter Employer Test");
        });
    }
}
