

using SFA.DAS.RAA.Service.Project.Helpers;

namespace SFA.DAS.RAA.Service.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 33)]
    public void SetUpHelpers()
    {
        var vacancyTitleDatahelper = context.Get<VacancyTitleDatahelper>();

        var fAAConfig = context.GetFAAConfig<FAAUserConfig>();

        context.Set(new RAADataHelper(fAAConfig, vacancyTitleDatahelper));

        context.Set(new AdvertDataHelper());

        var dbConfig = context.Get<DbConfig>();

        var objectContext = context.Get<ObjectContext>();

        context.Set(new ProviderCreateVacancySqlDbHelper(objectContext, dbConfig));

        context.Set(new RAAProviderPermissionsSqlDbHelper(objectContext, dbConfig));
    }
}
