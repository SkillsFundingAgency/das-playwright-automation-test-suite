namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "providerrestrictedcourses")]
public class RestrictedCoursesHooks(ScenarioContext context) : AparBaseHooks(context)
{
    private readonly string[] _tags = context.ScenarioInfo.Tags;

    [BeforeScenario(Order = 33)]
    public async Task ClearProviderRestrictedCoursesData()
    {
        if (_tags.Any(x => x == "rpadalup02a")) await ClearProviderRestrictedCourses();
    }

    [BeforeScenario(Order = 35)]
    public async Task AddLastStartDateRestrictProvider()
    {
        if (_tags.Any(x => x == "rpadalup03a")) await AddLastStartDate();
    }


    [AfterScenario(Order = 34)]
    public async Task ClearProviderRestrictedCoursesDataAfter()
    {
        if (_tags.Any(x => x == "rpadalup02a")) await ClearProviderRestrictedCourses();
    }
}