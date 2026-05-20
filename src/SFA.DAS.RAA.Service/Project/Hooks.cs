
using SFA.DAS.RAA.DataGenerator.Project.Helpers;
using SFA.DAS.RAA.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpDataHelpers()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpDataHelpers Order = 32 hook");

        var vacancyTitleDatahelper = new VacancyTitleDatahelper(isCloneVacancy: false);
        context.Set(vacancyTitleDatahelper);
    }

    [BeforeScenario(Order = 33)]
    public async Task SetUpHelpers()
    {
        try
        {
            var objectContext = context.Get<ObjectContext>();

            objectContext.SetConsoleAndDebugInformation("Entered SetUpHelpers Order = 33 hook");

            var vacancyTitleDatahelper = context.Get<VacancyTitleDatahelper>();

            var fAAConfig = context.GetFAAConfig<FAAUserConfig>();

            context.Set(new RAADataHelper(fAAConfig, vacancyTitleDatahelper));

            context.Set(new AdvertDataHelper());

            var dbConfig = context.Get<DbConfig>();

            objectContext.SetConsoleAndDebugInformation("Setting ProviderCreateVacancySqlDbHelper");
            context.Set(new ProviderCreateVacancySqlDbHelper(objectContext, dbConfig));

            objectContext.SetConsoleAndDebugInformation("Setting RAAProviderPermissionsSqlDbHelper");
            context.Set(new RAAProviderPermissionsSqlDbHelper(objectContext, dbConfig));

            objectContext.SetConsoleAndDebugInformation("Configuring browser timeouts");
            var browserContext = context.Get<Driver>().BrowserContext;

            objectContext.SetDebugInformation("*****Setting DefaultNavigationTimeout to be 40000ms = 40 sec*****");

            browserContext.SetDefaultNavigationTimeout(40000);

            objectContext.SetDebugInformation("*****Setting DefaultTimeout to be 40000ms = 40 sec*****");

            browserContext.SetDefaultTimeout(40000);

            objectContext.SetConsoleAndDebugInformation("Completed SetUpHelpers Order = 33 hook");
        }
        catch (Exception ex)
        {
            var objectContext = context.Get<ObjectContext>();
            objectContext.SetConsoleAndDebugInformation($"ERROR in SetUpHelpers Order = 33 hook: {ex.Message}");
            throw;
        }

        await Task.CompletedTask;
    }
}

