namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "providerrestrictedcourses")]
public class RestrictedCoursesHooks(ScenarioContext context) : AparBaseHooks(context)
{
    private readonly string[] _tags = context.ScenarioInfo.Tags;

    [BeforeScenario(Order = 33)]
    public async Task ClearProviderRestrictedCoursesData()
    {
        if (_tags.Any(x => x == "rpadalup02")) await ClearProviderRestrictedCourses();
    }
    [AfterScenario(Order = 34)]
    public async Task ClearProviderRestrictedCoursesDataAfter()
    {
        if (_tags.Any(x => x == "rpadalup02")) await ClearProviderRestrictedCourses();
    }
}