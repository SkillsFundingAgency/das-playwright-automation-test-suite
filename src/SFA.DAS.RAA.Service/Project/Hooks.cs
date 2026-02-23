
using SFA.DAS.RAA.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 33)]
    public async Task SetUpHelpers()
    {
        var vacancyTitleDatahelper = context.Get<VacancyTitleDatahelper>();

        var fAAConfig = context.GetFAAConfig<FAAUserConfig>();

        context.Set(new RAADataHelper(fAAConfig, vacancyTitleDatahelper));

        context.Set(new AdvertDataHelper());

        var browserContext = context.Get<Driver>().BrowserContext;

        var objectContext = context.Get<ObjectContext>();

        objectContext.SetDebugInformation("*****Setting DefaultNavigationTimeout to be 40000ms = 40 sec*****");

        browserContext.SetDefaultNavigationTimeout(40000);

        objectContext.SetDebugInformation("*****Setting DefaultTimeout to be 40000ms = 40 sec*****");

        browserContext.SetDefaultTimeout(40000);

        await Task.CompletedTask;
    }
}
