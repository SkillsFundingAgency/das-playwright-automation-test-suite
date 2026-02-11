namespace SFA.DAS.ProviderLogin.Service.Project;

public abstract class ProviderConfigurationBaseSetup
{
    protected readonly ScenarioContext _context;
    protected readonly ConfigSection _configSection;
    protected readonly string[] _tags;

    protected FrameworkList<DfeProviderUsers> dfeframeworkList;

    protected List<ProviderDetails> dfeProviderDetailsList;

    protected ProviderConfigurationBaseSetup(ScenarioContext context)
    {
        _context = context;
        _tags = context.ScenarioInfo.Tags;
        _configSection = _context.Get<ConfigSection>();
    }

    protected T SetProviderCreds<T>() where T : ProviderConfig => SetProviderCredsHelper.SetProviderCreds(dfeframeworkList, dfeProviderDetailsList, _configSection.GetConfigSection<T>());
}


[Binding]
public class ProviderConfigurationSetup(ScenarioContext context) : ProviderConfigurationBaseSetup(context)
{
    [BeforeScenario(Order = 2)]
    public async Task SetUpProviderConfiguration()
    {
        var objectContext = _context.Get<ObjectContext>();

        objectContext.SetConsoleAndDebugInformation("Entered SetUpProviderConfiguration Order = 2 hook");

        dfeframeworkList = _context.Get<FrameworkList<DfeProviderUsers>>();

        dfeProviderDetailsList = await new EmployerProviderRelationshipsSqlDataHelper(_context.Get<ObjectContext>(), _context.Get<DbConfig>()).GetProviderName(dfeframeworkList.SelectMany(x => x.Listofukprn).ToList());

        _context.Set(dfeProviderDetailsList);

        SetProviderConfig();

        _context.SetProviderPermissionConfig(SetProviderCreds<ProviderPermissionsConfig>());

        _context.SetChangeOfPartyConfig(SetProviderCreds<ChangeOfPartyConfig>());

        _context.SetPortableFlexiJobProviderConfig(SetProviderCreds<PortableFlexiJobProviderConfig>());

        _context.SetPerfTestProviderPermissionsConfig(_configSection.GetConfigSection<PerfTestProviderPermissionsConfig>());

        _context.SetNonEasLoginUser(SetProviderCreds<ProviderAccountOwnerUser>());        

        _context.SetNonEasLoginUser(_configSection.GetConfigSection<ProviderContributorUser>());

        _context.SetNonEasLoginUser(_configSection.GetConfigSection<ProviderContributorWithApprovalUser>());

        _context.SetNonEasLoginUser(_configSection.GetConfigSection<ProviderViewOnlyUser>());

        _context.SetNonEasLoginUser(SetProviderCreds<EmployerTypeProviderAccount>());
    }

    private void SetProviderConfig()
    {
        var providerConfig = SetProviderCreds<ProviderConfig>();

        if (_tags.IsAddRplDetails()) providerConfig = SetProviderCreds<RplProviderConfig>();

        if (_tags.IsTestDataDeleteCohortViaProviderPortal()) _context.Set(SetProviderCreds<DeleteCohortProviderConfig>());

        _context.SetProviderConfig(providerConfig);
    }
}
