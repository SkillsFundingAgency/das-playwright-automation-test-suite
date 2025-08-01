using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.EarlyConnectForms.UITests.Project;

[Binding]
public class Hooks : FrameworkBaseHooks
{
    private readonly ObjectContext _objectContext;
    private readonly DbConfig _dbConfig;
    private readonly EarlyConnectSqlHelper _sqlHelper;

    public Hooks(ScenarioContext context) : base(context)
    {
        _objectContext = context.Get<ObjectContext>();
        _dbConfig = context.Get<DbConfig>();
        _sqlHelper = new EarlyConnectSqlHelper(_objectContext, _dbConfig);
    }

    [BeforeScenario(Order = 31)]
    public async Task SetUpHelpers()
    {
        var mailosaurUser = context.Get<MailosaurUser>();

        var name = _sqlHelper.GetAnEducationalOrganisation();

        var datahelper = new EarlyConnectDataHelper(mailosaurUser, name);

        context.Set(datahelper);

        var email = datahelper.Email;

        _objectContext.SetDebugInformation($"'{email}' is used");

        mailosaurUser.AddToEmailList(email);

        await Navigate(UrlConfig.EarlyConnect_BaseUrl);
    }

    [AfterScenario(Order = 31)]
    public async Task DeleteStudentData()
    {
        await _sqlHelper.DeleteStudentDataAndAnswersByEmail(context.Get<EarlyConnectDataHelper>().Email);
    }
}