using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;

namespace SFA.DAS.ConfigurationBuilder
{
    [Binding]
    public class ObjectContextHooks(ScenarioContext context)
    {
        [BeforeScenario(Order = 0)]
        public void SetObjectContext(ObjectContext objectContext) => context.Set(objectContext);
    }
}