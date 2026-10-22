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

    [BeforeScenario(Order = 34)]
    public async Task AddLastStartDateRestrictProvider()
    {
        if (_tags.Any(x => x == "rpadalup03a")) await AddLastStartDate();
    }

    [BeforeScenario(Order = 35)]
    public async Task UnrestrictRestrictedProviderBefore()
    {
        if (_tags.Any(x => x == "rpadalup04")) await UnrestrictProvider();
    }

    [BeforeScenario(Order = 36)]
    public async Task ClearProviderAllowedCourseData()
    {
        if (_tags.Any(x => x == "rpadalup05")) await ClearProviderAllowedCourse();
    }


    [AfterScenario(Order = 33)]
    public async Task ClearProviderRestrictedCoursesDataAfter()
    {
        if (_tags.Any(x => x == "rpadalup02a")) await ClearProviderRestrictedCourses();
    }

    [AfterScenario(Order = 34)]
    public async Task UnrestrictRestrictedProvider()
    {
        if (_tags.Any(x => x == "rpadalup04")) await UnrestrictProvider();
    }
}