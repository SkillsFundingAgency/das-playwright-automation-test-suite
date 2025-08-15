
namespace SFA.DAS.AparAdmin.UITests.Project.Hooks;

[Binding, Scope(Tag = "rpgateway")]
public class AparAdminGatewayBeforeScenarioHooks(ScenarioContext context) : AparBaseHooks(context)
{
    [BeforeScenario(Order = 36)]
    public void SetUpGatewayApplicationRoute() => _objectContext.SetApplicationRoute(ApplicationRouteHelper.GetApplicationRoute(context.ScenarioInfo.Title));
}
