namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "restrictedcourses")]
public class ManagingStandardsHooks(ScenarioContext context) : AparBaseHooks(context)
{
    private readonly string[] _tags = context.ScenarioInfo.Tags;

    [BeforeScenario(Order = 33)]
    public async Task ClearLastStartDateData()
    {
        if (_tags.Any(x => x == "aparmrc02")) await ResetLastStartDate();
    }

    [BeforeScenario(Order = 34)]
    public async Task ClearDownProviderDataFromCourse()
    {
        if (_tags.Any(x => x == "aparmrc03")) await ResetTrainingProviderFromCourse();
    }
}
