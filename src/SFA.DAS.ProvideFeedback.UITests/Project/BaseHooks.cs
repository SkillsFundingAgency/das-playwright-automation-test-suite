using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.ProvideFeedback.UITests.Project;

public abstract class BaseHooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    protected readonly ObjectContext _objectContext = context.Get<ObjectContext>();
    protected readonly ScenarioContext _context = context;
    protected readonly TryCatchExceptionHelper _tryCatch = context.Get<TryCatchExceptionHelper>();
}
