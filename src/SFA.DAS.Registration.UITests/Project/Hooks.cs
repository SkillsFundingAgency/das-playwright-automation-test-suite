using SFA.DAS.Framework;
using SFA.DAS.MongoDb.DataGenerator;
using SFA.DAS.MongoDb.DataGenerator.Helpers;
using SFA.DAS.Registration.UITests.Project.Helpers;
using SFA.DAS.Registration.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.UI.FrameworkHelpers;

namespace SFA.DAS.Registration.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context)
{
    private readonly DbConfig _dbConfig = context.Get<DbConfig>();

    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 22)]
    public async Task Navigate()
    {
        var driver = context.Get<Driver>();

        var url = UrlConfig.EmployerApprenticeshipService_BaseUrl;

        context.Get<ObjectContext>().SetDebugInformation(url);

        await driver.Page.GotoAsync(url);
    }

    [BeforeScenario(Order = 23)]
    public void SetUpDataHelpers()
    {
        var tags = context.ScenarioInfo.Tags;

        var dataHelper = new EmployerUserNameDataHelper(tags);

        _objectContext.SetDataHelper(dataHelper);

        var mailosaurUser = context.Get<MailosaurUser>();

        var mailosaurEmaildomain = mailosaurUser.DomainName;

        var emaildomain = tags.Any(x => x.ContainsCompareCaseInsensitive("perftest")) ? "asperfautomation.com" : mailosaurEmaildomain;

        var aornDataHelper = new AornDataHelper();

        var registrationDatahelpers = new RegistrationDataHelper(tags, $"{dataHelper.GatewayUsername}@{emaildomain}", aornDataHelper);

        context.Set(registrationDatahelpers);

        context.Set(new LoginCredentialsHelper(_objectContext));

        _objectContext.SetOrganisationName(registrationDatahelpers.CompanyTypeOrg);

        context.Set(new RegistrationSqlDataHelper(_objectContext, _dbConfig));

        context.Set(new TprSqlDataHelper(_dbConfig, _objectContext, aornDataHelper));

        context.Set(new CommitmentsSqlHelper(_objectContext, _dbConfig));

        context.Set(new EmployerFinanceSqlHelper(_objectContext, _dbConfig));

        context.Set(new TransferMatchingSqlDataHelper(_objectContext, _dbConfig));

        var randomEmail = registrationDatahelpers.RandomEmail;

        _objectContext.SetRegisteredEmail(randomEmail);

        if (randomEmail.Contains(mailosaurEmaildomain)) mailosaurUser.AddToEmailList(randomEmail);
    }
}
