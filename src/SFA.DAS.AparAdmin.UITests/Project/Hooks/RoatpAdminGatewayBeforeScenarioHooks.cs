

namespace SFA.DAS.AparAdmin.UITests.Project.Hooks;

[Binding, Scope(Tag = "rpgateway")]
public class RoatpAdminGatewayBeforeScenarioHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    [BeforeScenario(Order = 36)]
    public void SetUpGatewayApplicationRoute() => _objectContext.SetApplicationRoute(ApplicationRouteHelper.GetApplicationRoute(context.ScenarioInfo.Title));
}
