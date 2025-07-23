using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework.Hooks;

public abstract class FrameworkBaseHooks(ScenarioContext context)
{
    protected readonly ScenarioContext context = context;

    protected async Task Navigate(string url)
    {
        var driver = context.Get<Driver>();

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }
}
