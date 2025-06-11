using SFA.DAS.FrameworkHelpers;

namespace SFA.DAS.EarlyConnectForms.UITests.Project;

[Binding]
public class Hooks
{
    private readonly ObjectContext _objectContext;
    private readonly DbConfig _dbConfig;
    private readonly ScenarioContext _context;
    private readonly EarlyConnectSqlHelper _sqlHelper;

    public Hooks(ScenarioContext context)
    {
        _context = context;
        _objectContext = context.Get<ObjectContext>();
        _dbConfig = context.Get<DbConfig>();
        _sqlHelper = new EarlyConnectSqlHelper(_objectContext, _dbConfig);
    }

    [BeforeScenario(Order = 31)]
    public async Task SetUpHelpers()
    {
        var mailosaurUser = _context.Get<MailosaurUser>();

        var name = _sqlHelper.GetAnEducationalOrganisation();

        var datahelper = new EarlyConnectDataHelper(mailosaurUser, name);

        _context.Set(datahelper);

        var email = datahelper.Email;

        _objectContext.SetDebugInformation($"'{email}' is used");

        mailosaurUser.AddToEmailList(email);

        var driver = _context.Get<Driver>();

        var url = UrlConfig.EarlyConnect_BaseUrl;

        _context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    [AfterScenario(Order = 31)]
    public async Task DeleteStudentData()
    {
        await _sqlHelper.DeleteStudentDataAndAnswersByEmail(_context.Get<EarlyConnectDataHelper>().Email);
    }
}