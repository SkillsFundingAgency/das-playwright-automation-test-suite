namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding]
public class RoatpLaunchApplicationHooks(ScenarioContext context) : RoatpBaseHooks(context)
{
    private readonly string[] _tags = context.ScenarioInfo.Tags;

    [BeforeScenario(Order = 41)]
    public async Task RoatpLaunchApplication()
    {
        if (_tags.Any(x => x == "roatpapply" || x == "roatpapplycreateaccount" || x == "roatpfulle2eviaapply" || x == "roatpapplyinprogressapplication" || x == "roatpapplychangeukprn" || x == "roatpapplytestdataprep")) await GoToUrl(UrlConfig.Apply_BaseUrl);

        if (_tags.Contains("roatpassessoradmin")) await GoToUrl(UrlConfig.RoATPAssessor_BaseUrl);
    }
}
