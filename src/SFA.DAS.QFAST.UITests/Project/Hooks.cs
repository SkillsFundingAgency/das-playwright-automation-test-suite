using SFA.DAS.Framework.Hooks;
using SFA.DAS.QFAST.UITests.Project.Helpers;


namespace SFA.DAS.QFAST.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 22)]
    public async Task Navigate() => await Navigate(UrlConfig.QFAST_BaseUrl);
    [BeforeScenario(Order = 23)]
    public void SetUpDataHelpers()
    {       
        context.Set(new QfastDbSqlHelpers(context.Get<ObjectContext>(), context.Get<DbConfig>()));
        var dataHelper = new QfastDataHelpers();
        context.Set(dataHelper);
    }
    //[AfterScenario(Order = 24)]
    //public async Task CleanUpQfastData()
    //{
    //    if (context.TestError != null) return;
    //    await context.Get<TryCatchExceptionHelper>().AfterScenarioException(async () =>
    //    {
    //        var qfastDbHelper = context.Get<QfastDbSqlHelpers>();
    //        await qfastDbHelper.DeleteApplications();
    //        await qfastDbHelper.DeleteForms();
    //    });
    //}
}