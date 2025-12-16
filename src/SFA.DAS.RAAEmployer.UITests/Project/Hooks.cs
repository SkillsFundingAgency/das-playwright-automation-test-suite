namespace SFA.DAS.RAAEmployer.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    [BeforeScenario(Order = 34)]
    public async Task SetUpHelpers()
    {
        //var apprenticeCourseDataHelper = new ApprenticeCourseDataHelper(new RandomCourseDataHelper(), ApprenticeStatus.WaitingToStart, []);

        //context.Set(apprenticeCourseDataHelper);

        var page = context.Get<Driver>().Page;

        page.SetDefaultNavigationTimeout(10000);

        page.SetDefaultTimeout(15000);

        await Task.CompletedTask;
    }
}
