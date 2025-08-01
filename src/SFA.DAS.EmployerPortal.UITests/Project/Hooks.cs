using SFA.DAS.EmployerPortal.UITests.Project.Helpers;
using SFA.DAS.EmployerPortal.UITests.Project.Helpers.SqlDbHelpers;
using SFA.DAS.Framework.Hooks;

namespace SFA.DAS.EmployerPortal.UITests.Project;

[Binding]
public class Hooks(ScenarioContext context) : FrameworkBaseHooks(context)
{
    private readonly DbConfig _dbConfig = context.Get<DbConfig>();

    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();

    [BeforeScenario(Order = 22)]
    public async Task Navigate() => await Navigate(UrlConfig.EmployerApprenticeshipService_BaseUrl);

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

        var employerPortalDatahelpers = new EmployerPortalDataHelper(tags, $"{dataHelper.GatewayUsername}@{emaildomain}", aornDataHelper);

        context.Set(employerPortalDatahelpers);

        context.Set(new LoginCredentialsHelper(_objectContext));

        _objectContext.SetOrganisationName(employerPortalDatahelpers.CompanyTypeOrg);

        context.Set(new EmployerPortalSqlDataHelper(_objectContext, _dbConfig));

        context.Set(new TprSqlDataHelper(_dbConfig, _objectContext, aornDataHelper));

        context.Set(new CommitmentsSqlHelper(_objectContext, _dbConfig));

        context.Set(new EmployerFinanceSqlHelper(_objectContext, _dbConfig));

        context.Set(new TransferMatchingSqlDataHelper(_objectContext, _dbConfig));

        var randomEmail = employerPortalDatahelpers.RandomEmail;

        _objectContext.SetRegisteredEmail(randomEmail);

        if (randomEmail.Contains(mailosaurEmaildomain)) mailosaurUser.AddToEmailList(randomEmail);
    }
}
