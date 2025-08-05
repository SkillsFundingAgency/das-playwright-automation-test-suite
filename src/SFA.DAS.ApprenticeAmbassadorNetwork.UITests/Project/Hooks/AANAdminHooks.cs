namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Hooks;

[Binding, Scope(Tag = "@aanadmin")]
public class AANAdminHooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 31)]
    public async Task Navigate_Admin() => await Navigate(UrlConfig.AAN_Admin_BaseUrl);

    [BeforeScenario(Order = 33)]
    public void SetUpDataHelpers()
    {
        if (context.ScenarioInfo.Tags.Contains("aanadmincreateevent"))
        {
            context.Set(new AanAdminCreateEventDatahelper());
        }

        if (context.ScenarioInfo.Tags.Contains("aanadminchangeevent"))
        {
            context.Set(new AanAdminUpdateEventDatahelper());
        }
    }

    [AfterScenario(Order = 30), Scope(Tag = "@aanadmincreateevent")]
    public async Task DeleteAdminCreatedEvent()
    {
        if (context.TestError != null) return;

        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(async () =>
        {
            var eventId = context.Get<ObjectContext>().GetAanAdminEventId();

            if (!string.IsNullOrEmpty(eventId)) await context.Get<AANSqlHelper>().DeleteAdminCreatedEvent(eventId);
        });
    }

    [AfterScenario(Order = 34), Scope(Tag = "@aanadn01b")]
    public async Task DeleteLocationFilterEvents()
    {
        await context.Get<TryCatchExceptionHelper>().AfterScenarioException(async () =>
        {
            await context.Get<AANSqlHelper>().DeleteLocationFilterEventsBeginning("Location Filter Test");
        });
    }
}
