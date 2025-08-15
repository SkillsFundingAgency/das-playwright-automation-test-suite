namespace SFA.DAS.Apar.UITests.Project.Hooks;

[Binding, Scope(Tag = "oldroatpadmin")]
public class OldAparAdminHooks(ScenarioContext context) : AparBaseHooks(context)
{
    private readonly string[] _tags = context.ScenarioInfo.Tags;

    [BeforeScenario(Order = 32)]
    public new void GetOldRoatpAdminData()
    {
        if (!_tags.Any(x => x == "oldroatpadmindownloadprovider" || x == "rpadoutcome01" || x == "roatpapplye2e" || x == "rpadoutcomeappeals01" || x == "rpadgatewayfailappeals01" || x == "rpadgatewayrejectreapplications01")) base.GetOldRoatpAdminData();
    }

    [BeforeScenario(Order = 33)]
    public async Task ClearDownAdminData()
    {
        if (_tags.Any(x => x == "deletetrainingprovider")) await DeleteTrainingProvider();
    }
}
