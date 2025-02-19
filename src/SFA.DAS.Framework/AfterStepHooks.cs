using SFA.DAS.FrameworkHelpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework;

[Binding]
public class AfterStepHooks(ScenarioContext context)
{
    [AfterStep(Order = 10)]
    public async Task AfterStep()
    {
        string StepOutcome() => context.TestError != null ? "ERROR" : "Done";

        var stepInfo = context.StepContext.StepInfo;

        var objectContext = context.Get<ObjectContext>();

        var message = $"-> {StepOutcome()}: {stepInfo.StepDefinitionType} {stepInfo.Text}";

        objectContext.SetAfterStepInformation(message);

        objectContext.SetDebugInformation(message);

        await Task.CompletedTask;
    }

    //[AfterStep(Order = 11)]
    //public async Task Screenshot()
    //{
    //    if (context.StepContext.StepInfo.Text.StartsWith("the trace is")) return;

    //    var driver = context.Get<Driver>();

    //    await driver.ScreenshotAsync(false);
    //}
}