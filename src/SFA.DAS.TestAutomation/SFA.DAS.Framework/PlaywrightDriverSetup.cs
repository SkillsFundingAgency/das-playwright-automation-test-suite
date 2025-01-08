using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework;

[Binding]
public class DriverSetup(ScenarioContext context)
{
    private static Driver driver;

    [BeforeTestRun]
    public static Task BeforeAll()
    {
        driver = new Driver();

        return Task.CompletedTask;
    }

    [BeforeScenario(Order = 4)]
    public void SetupPlaywrightDriver()
    {
        context.Set(driver.Page);
    }
}
