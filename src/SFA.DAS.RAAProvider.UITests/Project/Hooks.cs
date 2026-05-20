using System;
using SFA.DAS.RAA.DataGenerator.Project.Helpers;
using SFA.DAS.RAA.Service.Project.Helpers;

namespace SFA.DAS.RAAProvider.UITests.Project;

[Binding]
public class RAAProviderHooks(ScenarioContext context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpDataHelpers()
    {
        var objectContext = context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered RAAProvider SetUpDataHelpers Order = 32 hook");

        var vacancyTitleDatahelper = new VacancyTitleDatahelper(isCloneVacancy: false);
        context.Set(vacancyTitleDatahelper);
    }

    [BeforeScenario(Order = 33)]
    public void SetUpDatabaseHelpers()
    {
        try
        {
            var objectContext = context.Get<ObjectContext>();

            objectContext.SetConsoleAndDebugInformation("Entered RAAProvider SetUpDatabaseHelpers Order = 33 hook");

            var dbConfig = context.Get<DbConfig>();

            objectContext.SetConsoleAndDebugInformation("Setting ProviderCreateVacancySqlDbHelper");
            context.Set(new ProviderCreateVacancySqlDbHelper(objectContext, dbConfig));

            objectContext.SetConsoleAndDebugInformation("Setting RAAProviderPermissionsSqlDbHelper");
            context.Set(new RAAProviderPermissionsSqlDbHelper(objectContext, dbConfig));

            objectContext.SetConsoleAndDebugInformation("Completed RAAProvider SetUpDatabaseHelpers Order = 33 hook");
        }
        catch (Exception ex)
        {
            var objectContext = context.Get<ObjectContext>();
            objectContext.SetConsoleAndDebugInformation($"ERROR in RAAProvider SetUpDatabaseHelpers Order = 33 hook: {ex.Message}");
            throw;
        }
    }
}
