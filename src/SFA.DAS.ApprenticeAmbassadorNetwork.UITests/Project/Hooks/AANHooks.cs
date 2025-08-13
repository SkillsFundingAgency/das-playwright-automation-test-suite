global using SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Helpers;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Hooks;

[Binding]
public class AANHooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    [BeforeScenario(Order = 32)]
    public void SetUpDataHelpers()
    {
        context.Set(new AANSqlHelper(context.Get<ObjectContext>(), context.Get<DbConfig>()));

        context.Set(new AANDataHelpers());

        context.Set(new AanAdminStepsHelper(context));

        context.Set(new ApprenticeStepsHelper(context));

        context.Set(new EmployerStepsHelper(context));

        context.Set(new AanAdminCreateEventDatahelper());
    }
}
