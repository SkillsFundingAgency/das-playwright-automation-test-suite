using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Hooks;

[Binding, Scope(Tag = "@aanaprentice")]
public class AANApprenticeHooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 31)]
    public async Task Navigate_Apprentice() => await Navigate(UrlConfig.AAN_Apprentice_BaseUrl);

    [AfterScenario(Order = 34), Scope(Tag = "@aanapprentice04b")]
    public async Task DeleteLocationFilterEvents()
    {
        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(async () =>
        {
            await context.Get<AANSqlHelper>().DeleteLocationFilterEventsBeginning("Location Filter Apprentice Test");
        });
    }
}

