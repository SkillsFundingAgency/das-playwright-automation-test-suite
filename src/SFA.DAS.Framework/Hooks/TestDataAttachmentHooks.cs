using SFA.DAS.ConfigurationBuilder;
using SFA.DAS.FrameworkHelpers;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework.Hooks;

[Binding]
public class TestDataAttachmentHooks(ScenarioContext context)
{
    [AfterScenario(Order = 99)]
    public void AfterScenario_AttachTestData() => context.Get<TryCatchExceptionHelper>().AfterScenarioException(() => new TestDataAttachment(context).AddTestDataAttachment());
}