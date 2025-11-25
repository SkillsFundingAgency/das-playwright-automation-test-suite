using TechTalk.SpecFlow;
using SFA.DAS.FrameworkHelpers;

namespace SFA.DAS.Finance.APITests.Project;

[Binding]
public class FinanceConfigurationSetup(ScenarioContext context)
{
    [BeforeScenario(Order = 0)]
    public void InitializeObjectContext()
    {
        // Ensure an ObjectContext is available in ScenarioContext for tests and helpers
        context.Set(new ObjectContext());
    }
}
